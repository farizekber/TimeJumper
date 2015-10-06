using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System;
using global;

public class GameOverScene : MonoBehaviour {

    public AudioClip highscore;
    public AudioClip button;
    private AudioSource audSource;

    // Use this for initialization
    void Start ()
    {
        audSource = GameObject.Find("ApplicationGlobal").GetComponent<AudioSource>();

        TimeSpan t = TimeSpan.FromSeconds(Global.endingTime - Global.startTime);
        string answer = string.Format("{0:D2}:{1:D2}:{2:D2}",
                        t.Hours,
                        t.Minutes,
                        t.Seconds);

        GetComponentsInChildren<Text>().Where(s => s.name == "Time2").First().text = answer;
        GetComponentsInChildren<Text>().Where(s => s.name == "Distance2").First().text = (int)Global.distance + "m";
        GetComponentsInChildren<Text>().Where(s => s.name == "HighestDistance2").First().text = PlayerPrefs.GetInt("Highest Distance") + "m";

        int HighScoreBool = PlayerPrefs.GetInt("IsNewHighScore");

        if (HighScoreBool == 1)
        {
            GameObject.Find("Particle System").GetComponent<ParticleSystem>().Play();
            GameObject.Find("Particle System (1)").GetComponent<ParticleSystem>().Play();
            GameObject.Find("Particle System (2)").GetComponent<ParticleSystem>().Play();
            GameObject highscoreObject = GameObject.Find("HighscoreText");
            highscoreObject.GetComponent<SpriteRenderer>().transform.localPosition = new Vector3(highscoreObject.GetComponent<SpriteRenderer>().transform.localPosition.x, highscoreObject.GetComponent<SpriteRenderer>().transform.localPosition.y, -1f);
            Invoke("playHighscoreSound", 1);
        }

        ApplicationGlobal.GlobalBackButtonEnabled = false;
    }
	
    void playHighscoreSound()
    {
        audSource.PlayOneShot(highscore, 0.03f);
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
        audSource.PlayOneShot(button);
        Application.LoadLevel("Loading");
    }

    public void loadMenu()
    {
        audSource.PlayOneShot(button);
        Application.LoadLevel("MainMenu");
    }

}
