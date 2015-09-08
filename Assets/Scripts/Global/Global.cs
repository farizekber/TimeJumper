using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using Assets.Scripts;
using System.Collections.Generic;

public class Global : MonoBehaviour {
    
    public List<ObstacleBase> spawnables = new List<ObstacleBase>();
    public float spawnRate = 5;

    public static Stopwatch delay = new Stopwatch();
    public static float speed = 1f;
    public static int score = 0;
    public static Text ScoreText;
    public static GameObject ForegroundObject;
    public static GameObject GlobalObject;

	// Use this for initialization
	void Start () {
        GlobalObject = gameObject;
        ScoreText = GetComponentInChildren<Text>();
        ForegroundObject = GameObject.Find("Foreground");
        InvokeSpawns();
    }

    void InvokeSpawns()
    {
        if (GameOverAnimation.GetInstance().m_fAnimationInProgress)
            return;

        foreach (ObstacleBase obstacle in spawnables)
        {
            obstacle.Spawn();
        }

        Invoke("InvokeSpawns", (spawnRate / speed) * Random.value + 3);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            speed--;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            speed++;

        GameOverAnimation.GetInstance().Update();
    }

    public static void UpdateScore()
    {
        ScoreText.text = "Score : " + score;
    }
}
