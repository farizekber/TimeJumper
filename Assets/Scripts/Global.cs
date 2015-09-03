using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Global : MonoBehaviour {

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
	}

    public static void UpdateScore()
    {
        ScoreText.text = "Score : " + score;
    }
}
