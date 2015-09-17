using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Perspective
{
    class Theme
    {
        public List<ObstacleBase> m_spawnables, m_collectables;
        public List<PerspectiveTransition> m_perspectiveTransitions;
        public Texture m_background;
        public Sprite m_mainCharacter;
        public string m_mainCharacterAnimationString;
        public Sprite m_chaser;
        public string m_chaserAnimationString;

        public Theme(List<ObstacleBase> spawnables, List<ObstacleBase> collectables, List<PerspectiveTransition> perspectiveTransitions, Texture background, Sprite mainCharacter, string mainCharacterAnimationString, Sprite chaser, string chaserAnimationString)
        {
            m_spawnables = spawnables;
            m_collectables = collectables;
            m_perspectiveTransitions = perspectiveTransitions;
            m_background = background;
            m_mainCharacter = mainCharacter;
            m_mainCharacterAnimationString = mainCharacterAnimationString;
            m_chaser = chaser;
            m_chaserAnimationString = chaserAnimationString;
        }

        //        public static Theme Default()
        //        {
        //            return new Theme(new List<ObstacleBase>() { (Resources.Load("Prefabs/" + "Bat") as GameObject).GetComponent<Bat>(),
        //                                                        (Resources.Load("Prefabs/" + "Minecart") as GameObject).GetComponent<Minecart>(),
        //                                                        (Resources.Load("Prefabs/" + "Diamond") as GameObject).GetComponent<Diamond>() },
        //                             new List<PerspectiveTransition>() { new PerspectiveTransition(Perspectives.Horizontal, Perspectives.Vertical, delegate {
        //                                 PerspectiveInitializer.s_Instance.CleanPerspective();

        ////                                 Debug.Log(Resources.Load("Prefabs/" + "Boulder"));

        //                                 GameObject gobject = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/" + "Boulder"), new Vector3(0, 5, 0.5f), new Quaternion(0,0,0,0));
        //                                 gobject.transform.localPosition += Global.Instance.GlobalObject.transform.localPosition + Global.Instance.ForegroundObject.transform.localPosition;
        //                                 gobject.transform.parent = Global.Instance.ForegroundObject.transform;

        //                                 Fader.s_Instance.InvokeMethod("Enable", 1.25f);
        //                                 PerspectiveInitializer.s_Instance.InvokeMethod("LoadPerspective", 1.75f);
        //                                 Fader.s_Instance.InvokeMethod("Disable", 2.25f);
        //                             }) },
        //                             Resources.Load("Images/back") as Texture,
        //                             Resources.Load("Images/dragon") as Sprite,
        //                             "Animations/Dragon",
        //                             Resources.Load("Images/scottpilgrim_multiple") as Sprite,
        //                             "Animations/Main Character");
        //        }

        public static Theme LoadTestLevel()
        {
            return new Theme(new List<ObstacleBase>() { (Resources.Load("Prefabs/" + "Crate") as GameObject).GetComponent<Crate>(),
                                                        (Resources.Load("Prefabs/" + "Minecart") as GameObject).GetComponent<Minecart>(),
                                                        (Resources.Load("Prefabs/" + "Stone") as GameObject).GetComponent<Stone>(),
                                                        (Resources.Load("Prefabs/" + "Pickaxe") as GameObject).GetComponent<Pickaxe>(),
                                                        (Resources.Load("Prefabs/" + "TNT") as GameObject).GetComponent<TNT>() },

                             new List<ObstacleBase>() { (Resources.Load("Prefabs/" + "Diamond") as GameObject).GetComponent<Diamond>(),
                                                        (Resources.Load("Prefabs/" + "MineCarVehicle") as GameObject).GetComponent<MineCarVehicle>() },

                             new List<PerspectiveTransition>() { new PerspectiveTransition(Perspectives.Horizontal, Perspectives.Vertical, delegate {
                                 PerspectiveInitializer.s_Instance.CleanPerspective();

                                 GameObject gobject = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/" + "Boulder"), new Vector3(0, 5, 0.5f), new Quaternion(0,0,0,0));
                                 gobject.transform.localPosition += Global.Instance.GlobalObject.transform.localPosition + Global.Instance.ForegroundObject.transform.localPosition;
                                 gobject.transform.parent = Global.Instance.ForegroundObject.transform;

                                 Fader.s_Instance.InvokeMethod("Enable", 1.25f);
                                 PerspectiveInitializer.s_Instance.InvokeMethod("LoadVerticalPerspective", 1.75f);
                                 Fader.s_Instance.InvokeMethod("Disable", 2.25f);
                             }) },
                             Resources.Load("Images/back") as Texture, 
                             Resources.Load("Images/character-falling-v1") as Sprite, 
                             "Animations/Main Character 2",
                             Resources.Load("Images/dragon") as Sprite,
                             "Animations/Dragon");
        }
    }
}
