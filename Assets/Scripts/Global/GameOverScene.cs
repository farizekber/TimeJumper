using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System;
using global;

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

        GameObject.Find("Particle System").SetActive(PlayerPrefs.GetInt("IsNewHighScore") == 1);
        GameObject.Find("Particle System (1)").SetActive(PlayerPrefs.GetInt("IsNewHighScore") == 1);
        GameObject.Find("Particle System (2)").SetActive(PlayerPrefs.GetInt("IsNewHighScore") == 1);

        ApplicationGlobal.GlobalBackButtonEnabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {   
        if (Input.GetKey(KeyCode.Escape))
        {
            ApplicationGlobal.GlobalBackButtonEnabled = true;
            Application.LoadLevel("MainMenu");
        }
    }

    public void loadPlay()
    {
        Application.LoadLevel("Loading");
    }

    public void loadMenu()
    {
        Application.LoadLevel("MainMenu");
    }

}
