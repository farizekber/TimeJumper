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
        public void Start()
        {
            //Invoke("SwitchPerspective", 10);
        }

        public void SwitchPerspective()
        {
            Theme newTheme = Theme.LoadTestLevel();

            foreach (MonoBehaviour behaviour in GameObject.FindObjectsOfType(typeof(MonoBehaviour)))
            {
                behaviour.CancelInvoke();
            }
            
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

            //Quick theme test
            Global.Instance.spawnables.Clear();
            Global.Instance.spawnables.AddRange(newTheme.m_spawnables);
            
            GameObject.Find("Main Character").GetComponent<SpriteRenderer>().sprite = newTheme.m_mainCharacter;
            GameObject.Find("Main Character").GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(newTheme.m_mainCharacterAnimationString);

            GameObject.Find("Dragon").GetComponent<SpriteRenderer>().sprite = newTheme.m_chaser;
            GameObject.Find("Dragon").GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(newTheme.m_chaserAnimationString);

            GameObject.Find("Background").GetComponent<MeshRenderer>().material.mainTexture = newTheme.m_background;
            Global.Instance.InvokeSpawns();
        }
    }
}