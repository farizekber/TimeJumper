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
    }

    public void PauseButton()
    {
        if (!paused)
        {
            paused = true;
            Time.timeScale = 0f;
            AudioListener.pause = true;
            GameObject.Find("PauseButton").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/System Icons/play-button");
        }
        else
        {
            paused = false;
            Time.timeScale = 1f;
            AudioListener.pause = false;
            GameObject.Find("PauseButton").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/System Icons/pause-button");
        }
    }

    public void AudioButton()
    {

        if (AudioListener.volume == 0)
        {
            AudioListener.volume = 1;
            GameObject.Find("MuteButton").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/System Icons/audio-button");
        }
        else
        {
            AudioListener.volume = 0;
            GameObject.Find("MuteButton").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/System Icons/mute-button");
        }
    }

    // Update is called once per frame
    private void Update()
    {
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

        if (endingTime < 1)
        {
            System.TimeSpan t = System.TimeSpan.FromSeconds(currentTime - startTime);

            string answer = string.Format("{0:D2}:{1:D2}:{2:D2}",
                            t.Hours,
                            t.Minutes,
                            t.Seconds);
            TimeText.text = "Time : " + answer;
        }
        else
        {
            System.TimeSpan t = System.TimeSpan.FromSeconds(endingTime - startTime);

            string answer = string.Format("{0:D2}:{1:D2}:{2:D2}",
                            t.Hours,
                            t.Minutes,
                            t.Seconds);
            TimeText.text = "Time : " + answer;
        }

        if (addingDistance)
            distance += ((currentTime - lastTime) * speed) * 3;

        lastTime = currentTime;
        float prevHighest = PlayerPrefs.GetInt("Highest Distance");
        if ((int)distance > prevHighest)
        {
            DistanceText.color = new Color(255.0f / 255.0f, 208.0f / 255.0f, 66.0f / 255.0f);
            DistanceText.fontSize = 16;
        }
        DistanceText.text = "Distance : " + (int)distance + "m";
    }
}