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

    public float speed = 1f;
    public float spawnRate = 5f;
    public int score = 0;

    private Text ScoreText;
    private Text TimeText;
    private Text DistanceText;

    private float distance = 0;
    private float lastTime = 0;

    // Use this for initialization
    private void Start()
    {
        Instance = this;
        GlobalObject = gameObject;
        ScoreText = GetComponentsInChildren<Text>().Where(s => s.name == "ScoreText").First();
        TimeText = GetComponentsInChildren<Text>().Where(s => s.name == "TimeText").First();
        DistanceText = GetComponentsInChildren<Text>().Where(s => s.name == "DistanceText").First();
        //DistanceText.transform.localPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, new Vector2(13, 7));
        ForegroundObject = GameObject.Find("Foreground");
        InvokeSpawns();
    }

    public void InvokeSpawns()
    {
        if (GameOverAnimation.GetInstance().m_fAnimationInProgress)
            return;

        foreach (ObstacleBase obstacle in spawnables)
        {
            obstacle.Spawn();
        }

        Invoke("InvokeSpawns", (spawnRate * Random.value + 3) / speed);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            speed--;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            speed++;

        GameOverAnimation.GetInstance().Update();

        TimeText.text = "Time : " + (int)Time.time;
        distance += ((Time.time - lastTime) * speed) * 10;
        lastTime = Time.time;
        DistanceText.text = "Distance : " + (int)distance + "m";
    }

    public void UpdateScore()
    {
        ScoreText.text = "Score : " + score;
    }
}