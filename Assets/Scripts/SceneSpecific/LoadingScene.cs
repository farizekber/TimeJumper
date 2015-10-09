using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        //GameObject gobject = GameObject.Find("LoadingBar");
        //GameObject gobject1 = GameObject.Find("Text");
        //gobject.GetComponent<Image>().fillAmount = 0;

        /*AsyncOperation async = */yield return Application.LoadLevelAsync("Scene1");

        //while (!async.isDone)
        //{
        //    gobject1.GetComponent<Text>().text = (int)(async.progress * 100.0f) + "%";
        //    gobject.GetComponent<Image>().fillAmount = async.progress;
        //    yield return null;
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }
}
