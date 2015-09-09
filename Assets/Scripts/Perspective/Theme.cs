using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Perspective
{
    class Theme
    {
        public List<ObstacleBase> m_spawnables = new List<ObstacleBase>();
        public Texture m_background;
        public Sprite m_mainCharacter;
        public string m_mainCharacterAnimationString;
        public Sprite m_chaser;
        public string m_chaserAnimationString;

        public Theme(List<ObstacleBase> spawnables, Texture background, Sprite mainCharacter, string mainCharacterAnimationString, Sprite chaser, string chaserAnimationString)
        {
            m_spawnables = spawnables;
            m_background = background;
            m_mainCharacter = mainCharacter;
            m_mainCharacterAnimationString = mainCharacterAnimationString;
            m_chaser = chaser;
            m_chaserAnimationString = chaserAnimationString;
        }

        public static Theme LoadTestLevel()
        {
            return new Theme(new List<ObstacleBase>() { (Resources.Load("Prefabs/" + "Bat") as GameObject).GetComponent<Bat>(),
                                                        (Resources.Load("Prefabs/" + "Minecart") as GameObject).GetComponent<Minecart>(),
                                                        (Resources.Load("Prefabs/" + "Diamond") as GameObject).GetComponent<Diamond>() }, 
                             Resources.Load("Images/back") as Texture, 
                             Resources.Load("Images/dragon") as Sprite, 
                             "Animations/Dragon",
                             Resources.Load("Images/scottpilgrim_multiple") as Sprite,
                             "Animations/Main Character");
        }
    }
}
