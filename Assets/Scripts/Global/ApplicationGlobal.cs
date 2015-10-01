using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using System.Collections;
using System.Threading;

namespace global
{
    public class ApplicationGlobal : MonoBehaviour
    {
        public static bool isLoggedIn = false;

        // Use this for initialization
        void Start()
        {
            DontDestroyOnLoad(this);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
                Application.Quit();
        }
    }
}