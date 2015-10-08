using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingScene : MonoBehaviour {

    //0 = shader rendering on
    //1 = shader rendering off

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.GetInt("UseShader") == 1)
            GetComponent<Toggle>().isOn = true;
        else
            GetComponent<Toggle>().isOn = false;
    }
	
	// Update is called once per frame
	void Update () {
        //GameObject.Find("Text").GetComponent<Text>().text = "" + PlayerPrefs.GetInt("UseShader");
    }

    public void Toggle()
    {
        int i = 1 - PlayerPrefs.GetInt("UseShader");
        PlayerPrefs.SetInt("UseShader", i);
    }

    public void Back()
    {
        Application.LoadLevel("MainMenu");
    }
}
