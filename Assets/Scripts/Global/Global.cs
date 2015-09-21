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
    //public float obstacleSpawnRate = 5f;
    //public float collectableSpawnRate = 3f;
    public static int score = 0;

    public Text ScoreText;
    public Text TimeText;
    public Text DistanceText;

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

    // Use this for initialization
    private void Start()
    {
        Instance = this;

        HealthBar = GameObject.Find("HealthBar");
        HealthBar.GetComponent<Image>().enabled = false;
        HealthBarBackground = GameObject.Find("HealthBarBackground");
        HealthBarBackground.GetComponent<Image>().enabled = false;

        score = 0;
        distance = 0;
        startTime = Time.time;
        lastTime = startTime;
        endingTime = 0;

        GlobalObject = gameObject;
        ScoreText = GetComponentsInChildren<Text>().Where(s => s.name == "ScoreText").First();
        TimeText = GetComponentsInChildren<Text>().Where(s => s.name == "TimeText").First();
        DistanceText = GetComponentsInChildren<Text>().Where(s => s.name == "DistanceText").First();
        Instance.ScoreText.text = "Score: 0";
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
        DistanceText.text = "Distance : " + (int)distance + "m";
    }

    public void UpdateScore()
    {
        ScoreText.text = "Score : " + score;
    }
}