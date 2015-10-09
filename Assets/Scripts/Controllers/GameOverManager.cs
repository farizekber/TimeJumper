using Assets.Scripts.Objects;
using Assets.Scripts.Parallaxing;
using global;
using GooglePlayGames;
using UnityEngine;

namespace Assets.Scripts
{
    class GameOverManager
    {
        private static GameOverManager s_instance;

        public bool m_fAnimationInProgress = false;
        private float m_fpInitiatedTime = 0;

        public static GameOverManager GetInstance()
        {
            return s_instance == null ? s_instance = new GameOverManager() : s_instance;
        }

        public static void FinalizeObject()
        {
            s_instance = null;
        }

        public void Update()
        {
            if (!(m_fAnimationInProgress && (m_fpInitiatedTime + 2 < Time.time)))
                return;

            SpawnManager.Instance.CancelInvoke();
            SpawnManager.Instance.enabled = false;

            PerspectiveInitializer.s_Instance.CleanPerspective();
            PerspectiveInitializer.FinalizeObject();
             
            Fader.FinalizeObject();
            Global.FinalizeObject();
                
            m_fpInitiatedTime = 0;
            m_fAnimationInProgress = false;

            GameOverManager.FinalizeObject();
            ApplicationGlobal.GlobalBackButtonEnabled = true;
            Application.LoadLevel("GameOver");
        }

        public void Trigger()
        {
            if (m_fAnimationInProgress)
                return;

            if(Global.Instance.orientation == 0)
                GameObject.Find("Main Character").GetComponent<Animator>().Play("Death", 0, 0);

            GameObject background = GameObject.Find("Background Manager");
            for (int i = 0; i < background.transform.childCount; ++i)
            {
                background.transform.GetChild(i).GetComponent<BackgroundPlane>().SpeedModifier = 0;
            }

            GameObject.Find("SpawnManager").GetComponent<SpawnManager>().platformManager.DisableAll();
            GameObject.Find("SpawnManager").GetComponent<SpawnManager>().CancelInvoke();

            m_fAnimationInProgress = true;

            m_fpInitiatedTime = Time.time;
            Global.endingTime = Time.time;
            Global.Instance.addingDistance = false;

            if (PlayGamesPlatform.Instance.localUser.authenticated)
            {
                Social.ReportScore((int)Global.distance, GooglePlayServices.leaderboard_time_jumper_leaderboard, (bool success2) =>
                {
                    //Left empty on purpose.
                });
            }

            if (Global.distance > PlayerPrefs.GetInt("Highest Distance"))
            {
                PlayerPrefs.SetInt("Highest Distance", (int)Global.distance);
                PlayerPrefs.SetInt("IsNewHighScore", 1);
            }
            else
            {
                PlayerPrefs.SetInt("IsNewHighScore", 0);
            }

            Global.Instance.delay.Start();
        }   
    }
}
