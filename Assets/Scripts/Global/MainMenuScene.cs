using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using System.Threading;

public class MainMenuScene : MonoBehaviour
{
    bool loggedIn = false;
    public AudioClip button;
    private AudioSource audSource;

    // Use this for initialization
    void Start()
    {
        audSource = GameObject.Find("ApplicationGlobal").GetComponent<AudioSource>();
        grayButtons();
        PlayGamesPlatform.Activate();
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);

        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            loggedIn = true;
            coloredButtons();
        }
        AudioListener.volume -= PlayerPrefs.GetInt("MuteState");

        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
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

    // Update is called once per frame
    void Update()
    {

    }

    void loadPlay()
    {
        audSource.PlayOneShot(button);
        Application.LoadLevel("Loading");
    }

    public void loadLeaderboards()
    {
        audSource.PlayOneShot(button);
        Social.ShowLeaderboardUI();
    }

    public void loadAchievements()
    {
        audSource.PlayOneShot(button);
        Social.ShowAchievementsUI();
    }
    
    public void loadAuthentication()
    {
        audSource.PlayOneShot(button);
        if (!loggedIn)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
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

    public void SwitchToSettingScreen()
    {
        audSource.PlayOneShot(button);
        Application.LoadLevel("Settings");
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
