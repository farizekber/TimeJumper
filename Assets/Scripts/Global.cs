using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Diagnostics;

public class Global : MonoBehaviour {

    public GameObject spawningItemButton;
    public GameObject spawningItemDiamond;
    public float spawnRate = 5;
    float nextSpawn = 0;
    float lastSpawnTimeObstacle = 0;
    float lastSpawnTimeDiamond = 0;

    public static Stopwatch delay = new Stopwatch();
    public static float speed = 1f;
    public static int score = 0;
    public static Text ScoreText; 

	// Use this for initialization
	void Start () {
        ScoreText = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            speed--;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            speed++;

        //if (lastSpawnTime + (spawnRate / speed) >= Time.time)

        if (!ButtonCollision.buttonsCanMove)
            return;

        if(Time.time - lastSpawnTimeObstacle - (delay.ElapsedMilliseconds / 1000f) > ((spawnRate + nextSpawn)/speed))
        {
            nextSpawn = 10 * Random.value;
            lastSpawnTimeObstacle = Time.time;
            /*GameObject gobject = (GameObject)*/Instantiate(spawningItemButton,
                new Vector3(
                6.5f,
                -3.1f,
                0.5f),
                new Quaternion(0, 0, 0, 0));
        }

        if (Time.time - lastSpawnTimeDiamond - (delay.ElapsedMilliseconds / 1000f) > (spawnRate / speed))
        {
            lastSpawnTimeDiamond = Time.time;
            /*GameObject gobject = (GameObject)*/
            Instantiate(spawningItemDiamond,
                new Vector3(
                6.5f,
                Random.value * 6.0f - 1.5f,
                0.5f),
                new Quaternion(0, 0, 0, 0));

        }
    }

    public static void UpdateScore()
    {
        ScoreText.text = "Score : " + score;
    }
}
