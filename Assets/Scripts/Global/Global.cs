using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using Assets.Scripts;
using System.Collections.Generic;
using System.Linq;

public class Global : MonoBehaviour
{
    public static Global Instance;
    public GameObject ForegroundObject;
    public GameObject GlobalObject;
    public Stopwatch delay = new Stopwatch();
    public List<ObstacleBase> spawnables = new List<ObstacleBase>();
    public List<ObstacleBase> collectables = new List<ObstacleBase>();

    public float speed = 1f;
    float lastSpeedIncrease = 0;
    public float obstacleSpawnRate = 5f;
    public float collectableSpawnRate = 3f;
    public static int score = 0;

    private Text ScoreText;
    private Text TimeText;
    private Text DistanceText;

    public static float distance = 0;
    private float lastTime = 0;

    public static float startTime;
    public static float endingTime;

    public bool addingDistance = true;

    public static void Finalize()
    {
        Instance.enabled = false;
        Instance = null;
    }

    // Use this for initialization
    private void Start()
    {
        Instance = this;

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
        //DistanceText.transform.localPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, new Vector2(13, 7));
        ForegroundObject = GameObject.Find("Foreground");
        InvokeObstacleSpawns();
        InvokeCollectableSpawns();
    }

    public void InvokeCollectableSpawns()
    {
        if (GameOverAnimation.GetInstance().m_fAnimationInProgress)
            return;

        //float randomNumber = Random.value * 100;
        //float amountSpawn = 100 / spawnables.Count;
        //spawnables[(int)(randomNumber / amountSpawn)].Spawn();

        collectables.ForEach((ObstacleBase o) => o.Spawn());

        if (Random.value > 0.75)
            collectables.ForEach((ObstacleBase o) => o.Spawn());

        if (Random.value > 0.75)
            collectables.ForEach((ObstacleBase o) => o.Spawn());
        /*
        foreach (ObstacleBase obstacle in spawnables)
        {
            obstacle.Spawn();
        }*/

        Invoke("InvokeCollectableSpawns", (collectableSpawnRate * Random.value + 3) / speed);
    }

    public void InvokeObstacleSpawns()
    {
        if (GameOverAnimation.GetInstance().m_fAnimationInProgress)
            return;

        float randomNumber = Random.value * 100;
        float amountSpawn = 100 / spawnables.Count;
        spawnables[(int)(randomNumber / amountSpawn)].Spawn();
        //collectables.ForEach((ObstacleBase o) => o.Spawn());
        /*
        foreach (ObstacleBase obstacle in spawnables)
        {
            obstacle.Spawn();
        }*/

        Invoke("InvokeObstacleSpawns", (obstacleSpawnRate * Random.value + 3) / speed);
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
            
        if(endingTime < 1)
            TimeText.text = "Time : " + (int)(currentTime - startTime);
        else
            TimeText.text = "Time : " + (int)(endingTime);

        if (addingDistance)
            distance += ((currentTime - lastTime) * speed) * 10;

        lastTime = currentTime;
        DistanceText.text = "Distance : " + (int)distance + "m";
    }

    public void UpdateScore()
    {
        ScoreText.text = "Score : " + score;
    }
}