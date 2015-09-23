using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System;

public class GameOverScene : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        TimeSpan t = TimeSpan.FromSeconds(Global.endingTime - Global.startTime);
        string answer = string.Format("{0:D2}:{1:D2}:{2:D2}",
                        t.Hours,
                        t.Minutes,
                        t.Seconds);

        GetComponentsInChildren<Text>().Where(s => s.name == "Time2").First().text = answer;
        GetComponentsInChildren<Text>().Where(s => s.name == "Distance2").First().text = (int)Global.distance + "m";
        GetComponentsInChildren<Text>().Where(s => s.name == "HighestDistance2").First().text = PlayerPrefs.GetInt("Highest Distance") + "m";
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            Vector3 inputLocation = Vector3.zero;

            if (Input.GetMouseButtonDown(0))
            {
                inputLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                // Get movement of the finger since last frame
                inputLocation = Input.GetTouch(0).deltaPosition;
            }

            if (GetComponentInChildren<BoxCollider2D>().bounds.Intersects(new Bounds(inputLocation, new Vector3(20, 20, 100))))
            {
                Application.LoadLevel("Loading");
            }
        }
    }
}
