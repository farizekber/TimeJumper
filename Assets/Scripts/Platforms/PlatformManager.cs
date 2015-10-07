using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    //bug 1 combination disappears at the start, no clue why.
    public class PlatformManager
    {
        class PlatformCombination
        {
            public Platform begin;
            public List<Platform> middle = new List<Platform>();
            public Platform end;
            public CombinationObject combinedGameObject;

            public PlatformCombination(Platform begin, List<Platform> middle, Platform end)
            {
                this.begin = begin;
                this.middle = middle;
                this.end = end;
            }

            public PlatformCombination() { }
        }

        class CombinationObject
        {
            public GameObject combinationObject;
            public bool Taken = false;

            public Rigidbody2D rigid;

            public CombinationObject(GameObject combinationObject)
            {
                this.combinationObject = combinationObject;
                rigid = combinationObject.GetComponent<Rigidbody2D>();
            }

            public void Disable()
            {
                rigid.transform.SetParent(null);
                rigid.transform.localPosition = new Vector3(-8, 0, 0.5f);
                rigid.transform.localPosition += Global.Instance.GlobalObject.transform.localPosition + Global.Instance.ForegroundObject.transform.localPosition;
                rigid.transform.SetParent(Global.Instance.ForegroundObject.transform);
            }
        }

        List<PlatformCombination> spawnedPlatforms = new List<PlatformCombination>();
        List<CombinationObject> combiningObjects = new List<CombinationObject>();
        List<Platform> beginComponents = new List<Platform>();
        List<Platform> middleComponents = new List<Platform>();
        List<Platform> endComponents = new List<Platform>();
        public bool previousHigh = false;
        private Rigidbody2D mainCharacterRigidBody;

        public void Start()
        {
            mainCharacterRigidBody = GameObject.Find("Main Character").GetComponent<Rigidbody2D>();
        }

        //Initial Spawn
        public void Spawn()
        {
            for (int i = 0; i < 4; i++)
            {
                combiningObjects.Add(new CombinationObject((GameObject.Instantiate(Resources.Load("Prefabs/" + "Combination"), new Vector3(0, 0, 0.5f), new Quaternion(0, 0, 0, 0)) as GameObject)));
                combiningObjects[i].rigid.transform.SetParent(null);
                combiningObjects[i].rigid.transform.localPosition = Global.Instance.GlobalObject.transform.localPosition + Global.Instance.ForegroundObject.transform.localPosition;
                combiningObjects[i].rigid.transform.SetParent(Global.Instance.ForegroundObject.transform);
            }
            for (int i = 0; i < 4; i++)
            {
                beginComponents.Add((GameObject.Instantiate(Resources.Load("Prefabs/" + "PlatformBegin"), new Vector3(0, 0, 0.5f), new Quaternion(0, 0, 0, 0)) as GameObject).GetComponent<Platform>());
            }
            for (int i = 0; i < 20; i++)
            {
                middleComponents.Add((GameObject.Instantiate(Resources.Load("Prefabs/" + "PlatformMiddle"), new Vector3(0, 0, 0.5f), new Quaternion(0, 0, 0, 0)) as GameObject).GetComponent<Platform>());
            }
            for (int i = 0; i < 4; i++)
            {
                endComponents.Add((GameObject.Instantiate(Resources.Load("Prefabs/" + "PlatformEnd"), new Vector3(0, 0, 0.5f), new Quaternion(0, 0, 0, 0)) as GameObject).GetComponent<Platform>());
            }
        }

        //Updates position and disables combinations
        public void Update()
        {
            List<PlatformCombination> platformCombinationsToRemove = new List<PlatformCombination>();

            foreach (PlatformCombination platformCombination in spawnedPlatforms)
            {
                Rigidbody2D rigid = platformCombination.combinedGameObject.rigid;
                rigid.velocity = new Vector2(-2 * (Global.Instance.speed / 3.0f), 0);
                
                if (mainCharacterRigidBody.transform.localPosition.y < rigid.transform.localPosition.y)
                {
                    platformCombination.begin.colliderEnabled = false;
                    platformCombination.end.colliderEnabled = false;

                    foreach (Platform plat in platformCombination.middle)
                    {
                        plat.colliderEnabled = false;
                    }
                }

                if (rigid.transform.localPosition.x < -10.5f)
                {
                    platformCombinationsToRemove.Add(platformCombination);
                }
            }
            
            foreach (PlatformCombination platformCombination in platformCombinationsToRemove)
            {
                DisablePlatformCombination(platformCombination);
                spawnedPlatforms.Remove(platformCombination);
            }
        }

        //Cleans up a combination
        private void DisablePlatformCombination(PlatformCombination platformCombination)
        {
            foreach (Platform platform in platformCombination.middle)
            {
                platform.transform.SetParent(null);
                platform.Disable();
                platform.Taken = false;
            }
            platformCombination.begin.transform.SetParent(null);
            platformCombination.begin.Disable();
            platformCombination.begin.Taken = false;
            platformCombination.end.transform.SetParent(null);
            platformCombination.end.Disable();
            platformCombination.end.Taken = false;
            platformCombination.combinedGameObject.Disable();
            platformCombination.combinedGameObject.Taken = false;
        }

        //The update taking input into account.
        public void UpdateActive(Swipe currentSwipe, Transform mainCharacterTransform, bool downKeyDown)
        {
            foreach (PlatformCombination platformCombination in spawnedPlatforms)
            {
                bool enable = mainCharacterTransform.localPosition.y > (platformCombination.combinedGameObject.rigid.transform.localPosition.y + 0.60f);
                platformCombination.begin.colliderEnabled = enable;
                platformCombination.end.colliderEnabled = enable;

                foreach (Platform platform in platformCombination.middle)
                {
                    platform.colliderEnabled = enable;
                }

                if (currentSwipe.yDirectionDelta < -0.1 || downKeyDown)
                {
                    platformCombination.begin.colliderEnabled = false;
                    platformCombination.end.colliderEnabled = false;

                    foreach (Platform platform in platformCombination.middle)
                    {
                        platform.colliderEnabled = false;
                    }
                }
            }
        }

        //Activate a piece with the given length (Will always be extended with a begin and end component
        public void Activate(int length, bool low) {

            PlatformCombination platformCombination = new PlatformCombination();

            foreach (CombinationObject combination in combiningObjects)
            {
                if (!combination.Taken)
                {
                    platformCombination.combinedGameObject = combination;
                    combination.Taken = true;
                    break;
                }
            }

            //Check if a platform can be made at this time.
            if (platformCombination.combinedGameObject == null)
                return;

            foreach (Platform platform in beginComponents)
            {
                if (!platform.Taken)
                {
                    platformCombination.begin = platform;
                    platform.Taken = true;
                    break;
                }
            }

            for (int i = 0; i < length; i++)
            {
                foreach (Platform platform in middleComponents)
                {
                    if (!platform.Taken)
                    {
                        platformCombination.middle.Add(platform);
                        platform.Taken = true;
                        break;
                    }
                }
            }

            foreach (Platform platform in endComponents)
            {
                if (!platform.Taken)
                {
                    platformCombination.end = platform;
                    platform.Taken = true;
                    break;
                }
            }

            platformCombination.begin.transform.SetParent(platformCombination.combinedGameObject.rigid.transform);
            platformCombination.end.transform.SetParent(platformCombination.combinedGameObject.rigid.transform);
            foreach (Platform platform in platformCombination.middle)
            {
                platform.transform.SetParent(platformCombination.combinedGameObject.rigid.transform);
            }
            
            platformCombination.combinedGameObject.rigid.transform.localPosition = new Vector3(16, low ? 2.0f : 3.5f, 0.5f);
            float pieceSize = 0.64f;
            float xOffset = 0;

            platformCombination.end.transform.localPosition = new Vector3(platformCombination.end.transform.localPosition.x, 0, platformCombination.end.transform.localPosition.z);

            foreach (Platform item in platformCombination.middle)
            {
                xOffset += pieceSize;
                item.transform.localPosition = new Vector3(platformCombination.begin.transform.localPosition.x - xOffset, 0, platformCombination.begin.transform.localPosition.z);
            }

            platformCombination.begin.transform.localPosition = new Vector3(platformCombination.begin.transform.localPosition.x - xOffset - pieceSize, 0, platformCombination.begin.transform.localPosition.z);

            spawnedPlatforms.Add(platformCombination);
        }

        public void AdjustTheme(PerspectiveInitializer.ThemeState themeState, ref Sprite mineBegin, ref Sprite mineMiddle, ref Sprite mineEnd, ref Sprite iceBegin, ref Sprite iceMiddle, ref Sprite iceEnd)
        {
            if (themeState == PerspectiveInitializer.ThemeState.Mine)
            {
                foreach (Platform item in beginComponents)
                {
                    item.GetComponent<SpriteRenderer>().sprite = mineBegin;
                }
                foreach (Platform item in middleComponents)
                {
                    item.GetComponent<SpriteRenderer>().sprite = mineMiddle;
                }
                foreach (Platform item in endComponents)
                {
                    item.GetComponent<SpriteRenderer>().sprite = mineEnd;
                }
            }
            else
            {
                foreach (Platform item in beginComponents)
                {
                    item.GetComponent<SpriteRenderer>().sprite = iceBegin;
                }
                foreach (Platform item in middleComponents)
                {
                    item.GetComponent<SpriteRenderer>().sprite = iceMiddle;
                }
                foreach (Platform item in endComponents)
                {
                    item.GetComponent<SpriteRenderer>().sprite = iceEnd;
                }
            }
        }

        //Move all used platforms back to their original place
        public void DisableAll()
        {
            foreach (PlatformCombination platformCombination in spawnedPlatforms)
            {
                platformCombination.combinedGameObject.Disable();
                platformCombination.combinedGameObject.Taken = false;
                platformCombination.begin.Disable();
                platformCombination.begin.Taken = false;
                platformCombination.end.Disable();
                platformCombination.end.Taken = false;
                foreach (Platform platform in platformCombination.middle)
                {
                    platform.Taken = false;
                    platform.Disable();
                }
            }
        }

        //Removes everything that has to do with platforms
        public void FinalizeObject()
        {
            foreach (PlatformCombination platformCombination in spawnedPlatforms)
            {
                GameObject.Destroy(platformCombination.combinedGameObject.combinationObject);
            }
            foreach (CombinationObject combination in combiningObjects)
            {
                GameObject.Destroy(combination.combinationObject);
            }
            foreach (Platform platform in beginComponents)
            {
                GameObject.Destroy(platform);
            }
            foreach (Platform platform in middleComponents)
            {
                GameObject.Destroy(platform);
            }
            foreach (Platform platform in endComponents)
            {
                GameObject.Destroy(platform);
            }

            spawnedPlatforms.Clear();
            combiningObjects.Clear();
            beginComponents.Clear();
            middleComponents.Clear();
            endComponents.Clear();
        }
    }
}
