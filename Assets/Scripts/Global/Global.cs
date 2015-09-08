using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using Assets.Scripts;

public class Global : MonoBehaviour {

    public GameObject spawningItemMinecart;
    public GameObject spawningItemDiamond;
    public GameObject spawningItemBat;
    public float spawnRate = 5;

    public static Stopwatch delay = new Stopwatch();
    public static float speed = 1f;
    public static int score = 0;
    public static Text ScoreText;
    private GameObject Foreground;

	// Use this for initialization
	void Start () {
        ScoreText = GetComponentInChildren<Text>();
        Foreground = GameObject.Find("Foreground");
        InvokeSpawns();
    }

    void InvokeSpawns()
    {
        if (GameOverAnimation.GetInstance().m_fAnimationInProgress)
            return;

        SpawnBat();
        SpawnButton();
        SpawnCrystal();
        Invoke("InvokeSpawns", (spawnRate / speed) * Random.value + 3);
    }

    void SpawnCrystal()
    {
        while (GameOverAnimation.GetInstance().m_fAnimationInProgress)
        {
            Invoke("SpawnCrystal", 0.1f);
            return;
        }

        GameObject gobject = (GameObject)Instantiate(spawningItemDiamond,
            new Vector3(
            16.5f,
            Random.value * 10f - 1.0f,
            0.5f),
            new Quaternion(0, 0, 0, 0));
        gobject.transform.localPosition += transform.localPosition + Foreground.transform.localPosition;
        gobject.transform.parent = Foreground.transform;
    }

    void SpawnButton()
    {
        while (GameOverAnimation.GetInstance().m_fAnimationInProgress)
        {
            Invoke("SpawnButton", 0.1f);
            return;
        }

        GameObject gobject = (GameObject)Instantiate(spawningItemMinecart,
            new Vector3(
            16.5f,
            0f,
            0.5f),
            new Quaternion(0, 0, 0, 0));
        gobject.transform.localPosition += transform.localPosition + Foreground.transform.localPosition;
        gobject.transform.parent = Foreground.transform;
    }

    void SpawnBat()
    {
        while (GameOverAnimation.GetInstance().m_fAnimationInProgress)
        {
            Invoke("SpawnButton", 0.1f);
            return;
        }

        GameObject gobject = (GameObject)Instantiate(spawningItemBat,
            new Vector3(
            16.5f,
            Random.value * 10f - 1.0f,
            0.5f),
            new Quaternion(0, 0, 0, 0));
        gobject.transform.localPosition += transform.localPosition + Foreground.transform.localPosition;
        gobject.transform.parent = Foreground.transform;
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
