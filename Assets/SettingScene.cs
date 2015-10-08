using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingScene : MonoBehaviour {

    //1 = shader rendering on
    //0 = shader rendering off

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.GetInt("UseShader") == 1)
            GetComponent<Toggle>().isOn = true;
        else
            GetComponent<Toggle>().isOn = false;

        GetComponent<Toggle>().onValueChanged.AddListener(delegate { Toggle(); });
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void Toggle()
    {
        PlayerPrefs.SetInt("UseShader", 1 - PlayerPrefs.GetInt("UseShader"));
    }

    public void Back()
    {
        Application.LoadLevel("MainMenu");
    }
}
