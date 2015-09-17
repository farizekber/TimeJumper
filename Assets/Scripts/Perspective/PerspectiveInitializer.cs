using Assets.Scripts.Perspective;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class PerspectiveInitializer : MonoBehaviour
    {
        public static PerspectiveInitializer s_Instance;

        private Theme currentTheme;// = Theme.Default();
        private Perspectives currentPerspective;

        private Theme newTheme;
        private Perspectives newPerspective;

        private GameObject dragon;

        public static void Finalize()
        {
            s_Instance = null;
        }

        public void Start()
        {
            s_Instance = this;
            Invoke("SwitchPerspective", 10.0f + (UnityEngine.Random.value * 1.0f));
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

            //SpawnManager.Instance.CancelInvoke();
            SpawnManager.Instance.RemoveAll();
            //SpawnManager.Instance.Init();

            ////Remove all spawnables from screen.
            //((GameObject[])GameObject.FindObjectsOfType(typeof(GameObject))).Where((GameObject gameObject) =>
            //{
            //    foreach (ObstacleBase spawnable in SpawnManager.Instance.spawnables)
            //    {
            //        if (gameObject.name == spawnable.name + "(Clone)")
            //        {
            //            return true;
            //        }
            //    }
            //    return false;
            //}).ToList().ForEach((GameObject gameObject) => GameObject.Destroy(gameObject));

            ////Remove all spawnables from screen.
            //((GameObject[])GameObject.FindObjectsOfType(typeof(GameObject))).Where((GameObject gameObject) =>
            //{
            //    foreach (ObstacleBase spawnable in SpawnManager.Instance.collectables)
            //    {
            //        if (gameObject.name == spawnable.name + "(Clone)")
            //        {
            //            return true;
            //        }
            //    }
            //    return false;
            //}).ToList().ForEach((GameObject gameObject) => GameObject.Destroy(gameObject));
        }

        public void LoadVerticalPerspective()
        {
            Global.Instance.orientation = 1;

            GameObject.Find("Main Character").transform.localRotation = Quaternion.Euler(GameObject.Find("Main Character").transform.localRotation.x, GameObject.Find("Main Character").transform.localRotation.y, -90);
            //GameObject.Destroy(GameObject.Find("Platform"));
            GameObject.Find("Main Character").GetComponent<Rigidbody2D>().gravityScale = 0;

            for (int i = 0; i < GameObject.Find("Background").transform.childCount; ++i)
            {
                if (GameObject.Find("Background").transform.GetChild(i).gameObject.name != "Directional light")
                    GameObject.Find("Background").transform.GetChild(i).gameObject.SetActive(GameObject.Find("Background").transform.GetChild(i).gameObject.name.Length > 8);
            }

            GameObject.Find("Main Character").GetComponent<BoxCollider2D>().size = new Vector2(0.8542318f, 1.699413f) * 2;
            GameObject.Find("Main Character").GetComponent<Rigidbody2D>().transform.localPosition = new Vector3(4.5f, GameObject.Find("Main Character").GetComponent<Rigidbody2D>().transform.localPosition.y, GameObject.Find("Main Character").GetComponent<Rigidbody2D>().transform.localPosition.z);
            GameObject.Find("Main Character").GetComponent<SpriteRenderer>().sprite = newTheme.m_mainCharacter;
            GameObject.Find("Main Character").GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(newTheme.m_mainCharacterAnimationString);

            dragon = GameObject.Find("Dragon");
            dragon.SetActive(false);

            SpawnManager.Instance.spawnables.AddRange(newTheme.m_spawnables);
            SpawnManager.Instance.collectables.AddRange(newTheme.m_collectables);
            SpawnManager.Instance.Init();
        }

        public void LoadHorizontalPerspective()
        {
            Global.Instance.orientation = 0;

            GameObject.Find("Main Character").transform.localRotation = Quaternion.Euler(GameObject.Find("Main Character").transform.localRotation.x, GameObject.Find("Main Character").transform.localRotation.y, 0);
            //GameObject.Destroy(GameObject.Find("Platform"));
            GameObject.Find("Main Character").GetComponent<Rigidbody2D>().gravityScale = 0.35f;

            for (int i = 0; i < GameObject.Find("Background").transform.childCount; ++i)
            {
                if (GameObject.Find("Background").transform.GetChild(i).gameObject.name != "Directional light")
                    GameObject.Find("Background").transform.GetChild(i).gameObject.SetActive(GameObject.Find("Background").transform.GetChild(i).gameObject.name.Length < 8);
            }

            GameObject.Find("Main Character").GetComponent<BoxCollider2D>().size = new Vector2(0.8542318f, 1.699413f);
            GameObject.Find("Main Character").GetComponent<Rigidbody2D>().transform.localPosition = new Vector3(-3.459f, 2.216f, GameObject.Find("Main Character").GetComponent<Rigidbody2D>().transform.localPosition.z);
            GameObject.Find("Main Character").GetComponent<SpriteRenderer>().sprite = Resources.Load("Images/character-v2") as Sprite;
            GameObject.Find("Main Character").GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/character_0");

            if(dragon == null) GameObject.Find("Dragon").SetActive(true);
            else dragon.SetActive(true);

            //GameObject.Find("Platform").active = true;

            SpawnManager.Instance.spawnables.AddRange(newTheme.m_spawnables);
            SpawnManager.Instance.collectables.AddRange(newTheme.m_collectables);
            SpawnManager.Instance.Init();
        }

        public void SwitchPerspective()
        {
            if (Global.Instance.orientation == 0)
            {
                newTheme = Theme.LoadTestLevel();
                newTheme.m_perspectiveTransitions[0].m_trigger();
            }
            else
            {
                PerspectiveInitializer.s_Instance.CleanPerspective();

                GameObject gobject = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/" + "Boulder"), new Vector3(0, 5, 0.5f), new Quaternion(0, 0, 0, 0));
                gobject.transform.localPosition += Global.Instance.GlobalObject.transform.localPosition + Global.Instance.ForegroundObject.transform.localPosition;
                gobject.transform.parent = Global.Instance.ForegroundObject.transform;

                Fader.s_Instance.InvokeMethod("Enable", 1.25f);
                PerspectiveInitializer.s_Instance.InvokeMethod("LoadHorizontalPerspective", 1.75f);
                Fader.s_Instance.InvokeMethod("Disable", 2.25f);
            }

            Invoke("SwitchPerspective", 20.0f + (UnityEngine.Random.value * 1.0f));
        }
    }
}