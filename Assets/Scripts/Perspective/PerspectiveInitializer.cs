using Assets.Scripts.Perspective;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace Assets.Scripts
{
    class PerspectiveInitializer : MonoBehaviour
    {
        public static PerspectiveInitializer s_Instance;

        private Theme currentTheme;// = Theme.Default();
        private Perspectives currentPerspective;

        private Theme newTheme;
        private Perspectives newPerspective;

        public static void Finalize()
        {
            s_Instance = null;
        }

        public void Start()
        {
            s_Instance = this;
            //Invoke("SwitchPerspective", 10);
        }

        public void InvokeMethod(string methodName, float delay)
        {
            Invoke(methodName, delay);
        }

        public void CleanPerspective()
        {
            //Cancel all invokes except for the ones that are used in changing perspective.
            foreach (MonoBehaviour behaviour in GameObject.FindObjectsOfType(typeof(MonoBehaviour)))
            {
                if (behaviour.name == "Fader" || behaviour.name == "PerspectiveInitializer" || behaviour.name == "Theme")
                {
                    continue;
                }

                behaviour.CancelInvoke();
            }

            //Remove all spawnables from screen.
            ((GameObject[])GameObject.FindObjectsOfType(typeof(GameObject))).Where((GameObject gameObject) =>
            {
                foreach (ObstacleBase spawnable in Global.Instance.spawnables)
                {
                    if (gameObject.name == spawnable.name + "(Clone)")
                    {
                        return true;
                    }
                }
                return false;
            }).ToList().ForEach((GameObject gameObject) => GameObject.Destroy(gameObject));
        }

        public void LoadPerspective()
        {
            Global.Instance.spawnables.Clear();
            Global.Instance.spawnables.AddRange(newTheme.m_spawnables);

            GameObject.Find("Main Character").GetComponent<SpriteRenderer>().sprite = newTheme.m_mainCharacter;
            GameObject.Find("Main Character").GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(newTheme.m_mainCharacterAnimationString);

            GameObject.Find("Dragon").GetComponent<SpriteRenderer>().sprite = newTheme.m_chaser;
            GameObject.Find("Dragon").GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(newTheme.m_chaserAnimationString);

            GameObject.Find("Background").GetComponent<MeshRenderer>().material.mainTexture = newTheme.m_background;
            //test
            Background.Instance.horizontal = false;

            Global.Instance.InvokeSpawns();
        }

        public void SwitchPerspective()
        {
            newTheme = Theme.LoadTestLevel();
            newTheme.m_perspectiveTransitions[0].m_trigger();
        }
    }
}