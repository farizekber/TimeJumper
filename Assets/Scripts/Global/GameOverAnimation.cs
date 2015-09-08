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

        public GameOverAnimation()
        {
            initializeTwirl();
        }

        public void Update()
        {
            if (!m_fAnimationInProgress)
                return;

            (m_twirl as UnityStandardAssets.ImageEffects.Twirl).angle++;

            if (m_fpInitiatedTime + m_fpTwirlTimeInSeconds < Time.time && (((m_twirl as UnityStandardAssets.ImageEffects.Twirl).angle % 360) == 0))
            {
                m_fpInitiatedTime = 0;
                m_fAnimationInProgress = false;
                Global.Instance.delay.Stop();
                Application.LoadLevel("GameOver");
            }
        }

        public void Trigger()
        {
            m_fpInitiatedTime = Time.time;
            m_fAnimationInProgress = true;
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
