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
            //PlayGamesPlatform.Activate();

            //TEST
            //PlayGamesPlatform.Instance.SignOut();

            //while (PlayGamesPlatform.Instance.localUser.authenticated)
            //    Thread.Sleep(10);

            //PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            //PlayGamesPlatform.InitializeInstance(config);
            //Social.localUser.Authenticate((bool success) =>
            //{
            //    if (success)
            //    {
            //        Social.ReportScore(10, GooglePlayServices.leaderboard_time_jumper_leaderboard, (bool success2) =>
            //        {
            //            Social.ShowLeaderboardUI();
            //        });
            //    }
            //});
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
                Application.Quit();
        }
    }
}