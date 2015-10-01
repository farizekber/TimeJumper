using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class MainMenuScene : MonoBehaviour
{
    bool loggedIn = false;
    // Use this for initialization
    void Start()
    {
        PlayGamesPlatform.Activate();
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);

        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            loggedIn = true;
            coloredButtons();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void loadPlay()
    {
        Application.LoadLevel("Loading");
    }

    public void loadLeaderboards()
    {
        Social.ShowLeaderboardUI();
    }

    public void loadAchievements()
    {
        Social.ShowAchievementsUI();
    }
    
    public void loadAuthentication()
    {
        if (!loggedIn)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    //Social.ReportScore(10, GooglePlayServices.leaderboard_time_jumper_leaderboard, (bool success2) =>
                    //{

                    //});
                    coloredButtons();
                    loggedIn = true;
                }
                else
                {
                    grayButtons();
                    loggedIn = false;
                }
            });
        }
        else
        {
            PlayGamesPlatform.Instance.SignOut();
            grayButtons();
            loggedIn = false;
        }

    }

    void grayButtons()
    {
        GameObject.Find("AuthenticationCover").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("LeaderboardCover").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("AchievementsCover").GetComponent<SpriteRenderer>().enabled = false;
    }

    void coloredButtons()
    {
        GameObject.Find("AuthenticationCover").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("LeaderboardCover").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("AchievementsCover").GetComponent<SpriteRenderer>().enabled = true;
    }
}
