using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using Assets.Scripts;
using System.Collections.Generic;
using System.Linq;

public class Global : MonoBehaviour
{
    public static Global Instance;

    public int orientation = 0;

    public GameObject ForegroundObject;
    public GameObject GlobalObject;
    public Stopwatch delay = new Stopwatch();
    //public List<ObstacleBase> spawnables = new List<ObstacleBase>();
    //public List<ObstacleBase> collectables = new List<ObstacleBase>();

    public float speed = 1f;
    float lastSpeedIncrease = 0;

    public Text TimeText;
    public Text DistanceText;
    private bool paused = false;

    public static float distance = 0;
    private float lastTime = 0;

    public static float startTime;
    public static float endingTime;

    public bool addingDistance = true;

    private AudioSource audioSource;
    private AudioSource foregroundAudioSource;

    public GameObject HealthBar;
    public GameObject HealthBarBackground;

    //Performance related.
    System.TimeSpan t;
    char[] str = new char[15];

    public void LoadHorizontal(PerspectiveInitializer.ThemeState themeState)
    {
        orientation = 0;

        TimeText.transform.localRotation = Quaternion.Euler(0, 0, 0);
        TimeText.transform.localPosition = new Vector3(-239, 180, TimeText.transform.localPosition.z);

        DistanceText.transform.localRotation = Quaternion.Euler(0, 0, 0);
        DistanceText.transform.localPosition = new Vector3(-100, 180, DistanceText.transform.localPosition.z);

        GameObject muteButton = GameObject.Find("MuteButton");
        GameObject pauseButton = GameObject.Find("PauseButton");
        float oldY = muteButton.transform.localPosition.y;
        muteButton.transform.localPosition = new Vector3(muteButton.transform.localPosition.x, oldY * -1, muteButton.transform.localPosition.z);
        pauseButton.transform.localPosition = new Vector3(muteButton.transform.localPosition.x - 40, oldY * -1, pauseButton.transform.localPosition.z);
        muteButton.transform.rotation = Quaternion.Euler(0, 0, 0);
        pauseButton.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void LoadVertical(PerspectiveInitializer.ThemeState themeState)
    {
        orientation = 1;

        TimeText.transform.localRotation = Quaternion.Euler(0, 0, -90);
        TimeText.transform.localPosition = new Vector3(-300, 110, TimeText.transform.localPosition.z);

        DistanceText.transform.localRotation = Quaternion.Euler(0, 0, -90);
        DistanceText.transform.localPosition = new Vector3(-300, -40, DistanceText.transform.localPosition.z);

        GameObject muteButton = GameObject.Find("MuteButton");
        GameObject pauseButton = GameObject.Find("PauseButton");
        muteButton.transform.localPosition = new Vector3(muteButton.transform.localPosition.x, muteButton.transform.localPosition.y * -1, muteButton.transform.localPosition.z);
        pauseButton.transform.localPosition = new Vector3(muteButton.transform.localPosition.x, (pauseButton.transform.localPosition.y * -1) + 40, pauseButton.transform.localPosition.z);
        muteButton.transform.rotation = Quaternion.Euler(0, 0, 270);
        pauseButton.transform.rotation = Quaternion.Euler(0, 0, 270);
    }

    public void PlayPickupSound()
    {
        audioSource.Play();
    }

    public void PlayDeathSound()
    {
        foregroundAudioSource.Play();
    }

    public static void FinalizeObject()
    {
        Instance.enabled = false;
        Instance = null;
    }

    void Awake()
    {
        Instance = this;
        AudioListener.volume = 1 - PlayerPrefs.GetInt("MuteState");
        GameObject.Find("Main Camera").GetComponent<AudioSource>().enabled = true;
    }

    // Use this for initialization
    private void Start()
    {
        HealthBar = GameObject.Find("HealthBar");
        HealthBar.GetComponent<Image>().enabled = false;
        HealthBarBackground = GameObject.Find("HealthBarBackground");
        HealthBarBackground.GetComponent<Image>().enabled = false;
        
        distance = 0;
        startTime = Time.time;
        lastTime = startTime;
        endingTime = 0;

        GlobalObject = gameObject;
        TimeText = GetComponentsInChildren<Text>().Where(s => s.name == "TimeText").First();
        DistanceText = GetComponentsInChildren<Text>().Where(s => s.name == "DistanceText").First();
        Instance.TimeText.text = "Time : 00:00:00";
        Instance.DistanceText.text = "Distance: 0m";

        ForegroundObject = GameObject.Find("Foreground");
        foregroundAudioSource = ForegroundObject.GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();

        SpawnManager.Instance.Init();

        Screen.autorotateToPortrait = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToPortraitUpsideDown = false;
        
        if (AudioListener.volume == 0)
        {
            GameObject.Find("MuteButton").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/System Icons/mute-button-v2");
        }
        else
        {
            GameObject.Find("MuteButton").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/System Icons/audio-button-v2");
        }

        str[0] = 'T';
        str[1] = 'i';
        str[2] = 'm';
        str[3] = 'e';
        str[4] = ' ';
        str[5] = ':';
        str[6] = ' ';
        str[7] = '0';
        str[8] = '0';
        str[9] = ':';
        str[10] = '0';
        str[11] = '0';
        str[12] = ':';
        str[13] = '0';
        str[14] = '0';
    }

    public void PauseButton()
    {
        if (!paused)
        {
            paused = true;
            Time.timeScale = 0f;
            AudioListener.pause = true;
            GameObject.Find("PauseButton").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/System Icons/play-button-v2");
        }
        else
        {
            paused = false;
            Time.timeScale = 1f;
            AudioListener.pause = false;
            GameObject.Find("PauseButton").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/System Icons/pause-button-v2");
        }
    }

    public void AudioButton()
    {
        if (AudioListener.volume == 0)
        {
            AudioListener.volume = 1;
            GameObject.Find("MuteButton").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/System Icons/audio-button-v2");
            PlayerPrefs.SetInt("MuteState", 0);
        }
        else
        {
            AudioListener.volume = 0;
            GameObject.Find("MuteButton").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/System Icons/mute-button-v2");
            PlayerPrefs.SetInt("MuteState", 1);
        }
    }

    //Choose an alternative from the commented piece of code, because of performance issues.
    void updateTimeString(float currentTime)
    {
        if (endingTime < 1)
        {
            t = System.TimeSpan.FromSeconds(currentTime - startTime);
        }
        else
        {
            t = System.TimeSpan.FromSeconds(endingTime - startTime);
        }

        if (t.Hours < 10)
        {
            str[7] = '0';
            str[8] = ((char)(t.Hours + 48));
        }
        else
        {
            str[7] = (char)(t.Hours / 10 + 48);
            str[8] = (char)(t.Hours % 10 + 48);
        }

        if (t.Minutes < 10)
        {
            str[10] = '0';
            str[11] = ((char)(t.Minutes + 48));
        }
        else
        {
            str[10] = (char)(t.Minutes / 10 + 48);
            str[11] = (char)(t.Minutes % 10 + 48);
        }

        if (t.Seconds < 10)
        {
            str[13] = '0';
            str[14] = ((char)(t.Seconds + 48));
        }
        else
        {
            str[13] = (char)(t.Seconds / 10 + 48);
            str[14] = (char)(t.Seconds % 10 + 48);
        }
        
        TimeText.text = new string(str, 0, str.Count());
        
        /*= string.Format("Time : {0:D2}:{1:D2}:{2:D2}",
                    t.Hours,
                    t.Minutes,
                    t.Seconds);*/
    }

    void OnGUI()
    {
        float currentTime = Time.time;

        if (!(currentTime < lastTime + 0.5f))
            return;

        updateTimeString(currentTime);

        if ((int)distance > PlayerPrefs.GetInt("Highest Distance"))
        {
            DistanceText.color = new Color(255.0f / 255.0f, 208.0f / 255.0f, 66.0f / 255.0f);
        }
        DistanceText.text = "Distance : " + (int)distance + "m";
    }
    
    // Update is called once per frame
    private void Update()
    {
        // PlayerPrefs.SetInt("Highest Distance", 0);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            speed--;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            speed++;

        GameOverAnimation.GetInstance().Update();

        float currentTime = Time.time;
        if ((int)currentTime % 5 == 0 && (currentTime - lastSpeedIncrease) > 1.5f)
        {
            speed += 0.075f;
            lastSpeedIncrease = currentTime;
        }

        if (addingDistance)
            distance += ((currentTime - lastTime) * speed) * 3;

        lastTime = currentTime;
    }
}