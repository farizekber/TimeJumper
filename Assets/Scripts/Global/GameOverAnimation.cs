using UnityEngine;

namespace Assets.Scripts
{
    class GameOverAnimation
    {
        private static GameOverAnimation s_instance;
        public readonly float m_fpTwirlTimeInSeconds = 5;

        private Component m_twirl = null;
        public bool m_fAnimationInProgress = false;
        private float m_fpInitiatedTime = 0;

        public static GameOverAnimation GetInstance()
        {
            return s_instance == null ? s_instance = new GameOverAnimation() : s_instance;
        }

        public static void FinalizeObject()
        {
            s_instance = null;
        }

        public GameOverAnimation()
        {
            initializeTwirl();
        }

        public void Update()
        {
            if (!m_fAnimationInProgress)
                return;

            (m_twirl as UnityStandardAssets.ImageEffects.Twirl).angle+=3;

            if (/*m_fpInitiatedTime + m_fpTwirlTimeInSeconds < Time.time &&*/ (((m_twirl as UnityStandardAssets.ImageEffects.Twirl).angle % 360) == 0))
            {
                SpawnManager.Instance.CancelInvoke();
                SpawnManager.Instance.enabled = false;

                //foreach (Background item in Resources.FindObjectsOfTypeAll(typeof(Background)))
                //{
                //    item.FinalizeObject();
                //}
                
                PerspectiveInitializer.s_Instance.CleanPerspective();
                PerspectiveInitializer.FinalizeObject();
                
                //Background.Finalize();
                Fader.FinalizeObject();
                Global.FinalizeObject();
                
                m_fpInitiatedTime = 0;
                m_fAnimationInProgress = false;
                //Global.Instance.delay.Stop();
                GameOverAnimation.FinalizeObject();

                Application.LoadLevel("GameOver");
            }
        }

        public void Trigger()
        {
            if (m_fAnimationInProgress)
                return;

            m_fAnimationInProgress = true;

            m_fpInitiatedTime = Time.time;
            Global.endingTime = Time.time;
            //Global.Instance.PlayDeathSound();
            Global.Instance.addingDistance = false;

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

        private void initializeTwirl()
        {
            foreach (Component component in GameObject.Find("Main Camera").GetComponents<Component>())
            {
                if (component is UnityStandardAssets.ImageEffects.Twirl)
                {
                    m_twirl = component;
                }
            }
        }
    }

}
