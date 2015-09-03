using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class Global : MonoBehaviour {

    public GameObject spawningItem;
    public float spawnRate = 5;
    float lastSpawnTime = 0;

    public static Stopwatch delay = new Stopwatch();
    public static float speed = 1f;
    
	// Use this for initialization
	void Start () {
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

        if(Time.time - lastSpawnTime - (delay.ElapsedMilliseconds / 1000f) > (spawnRate/speed))
        {
            lastSpawnTime = Time.time;
            GameObject gobject = (GameObject)Instantiate(spawningItem,
                new Vector3(
                6.5f,
                Random.value * 6.0f - 3,
                0.5f),
                new Quaternion(0, 0, 0, 0));
        }
    }
}
