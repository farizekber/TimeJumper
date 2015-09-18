using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class PlatformManager
    {
        //3 prefabs
        //Have 4 of 1 and 3, 20 of 2
        //All use platform script

        class PlatformCombination
        {
            public Platform begin;
            public List<Platform> middle = new List<Platform>();
            public Platform end;
            public GameObject combinedGameObject = new GameObject("Combined Platform");

            public PlatformCombination(Platform begin, List<Platform> middle, Platform end)
            {
                this.begin = begin;
                this.middle = middle;
                this.end = end;
            }

            public PlatformCombination() { }
        }

        List<PlatformCombination> spawnedPlatforms = new List<PlatformCombination>();
        List<Platform> beginComponents = new List<Platform>();
        List<Platform> middleComponents = new List<Platform>();
        List<Platform> endComponents = new List<Platform>();
        public bool previousHigh = false;

        public void Update()
        {
            foreach (PlatformCombination platformCombination in spawnedPlatforms)
            {
                platformCombination.combinedGameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-2 * (Global.Instance.speed / 3.0f), 0);
            }
        }

        public void UpdateActive(Swipe currentSwipe, Transform mainCharacterTransform, bool downKeyDown)
        {
            foreach (PlatformCombination platformCombination in spawnedPlatforms)
            {
                platformCombination.begin.colliderEnabled = mainCharacterTransform.localPosition.y > (platformCombination.combinedGameObject.transform.localPosition.y + 0.55f);
                platformCombination.end.colliderEnabled = mainCharacterTransform.localPosition.y > (platformCombination.combinedGameObject.transform.localPosition.y + 0.55f);

                foreach (Platform platform in platformCombination.middle)
                {
                    platform.colliderEnabled = mainCharacterTransform.localPosition.y > (platformCombination.combinedGameObject.transform.localPosition.y + 0.55f);
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

        //Initial Spawn
        public void Spawn()
        {
            for (int i = 0; i < 4; i++)
            {
                beginComponents.Add((GameObject.Instantiate(Resources.Load("Prefabs/" + "PlatformBegin"), new Vector3(-8, 0, 0.5f), new Quaternion(0, 0, 0, 0)) as GameObject).GetComponent<Platform>());
            }
            for (int i = 0; i < 20; i++)
            {
                middleComponents.Add((GameObject.Instantiate(Resources.Load("Prefabs/" + "PlatformMiddle"), new Vector3(-8, 0, 0.5f), new Quaternion(0, 0, 0, 0)) as GameObject).GetComponent<Platform>());
            }
            for (int i = 0; i < 4; i++)
            {
                endComponents.Add((GameObject.Instantiate(Resources.Load("Prefabs/" + "PlatformEnd"), new Vector3(-8, 0, 0.5f), new Quaternion(0, 0, 0, 0)) as GameObject).GetComponent<Platform>());
            }
        }

        //Activate a piece with the given length (Will always be extended with a begin and end component
        public void Activate(int length, bool low) {

            PlatformCombination platformCombination = new PlatformCombination();

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
            
            platformCombination.combinedGameObject.AddComponent<Rigidbody2D>();
            platformCombination.combinedGameObject.GetComponent<Rigidbody2D>().mass = 5;
            platformCombination.combinedGameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            platformCombination.combinedGameObject.GetComponent<Rigidbody2D>().gravityScale = 0;

            platformCombination.combinedGameObject.GetComponent<Rigidbody2D>().transform.localPosition = new Vector3(-8, 0, 0.5f);
            //platformCombination.combinedGameObject.GetComponent<Rigidbody2D>().transform.localPosition += Global.Instance.GlobalObject.transform.localPosition + Global.Instance.ForegroundObject.transform.localPosition;
            platformCombination.combinedGameObject.GetComponent<Rigidbody2D>().transform.SetParent(Global.Instance.ForegroundObject.transform);

            if (platformCombination.begin == null || platformCombination.end == null)
            {
                if (platformCombination.begin != null)
                    platformCombination.begin.Taken = false;
                if (platformCombination.end != null)
                    platformCombination.end.Taken = false;
                foreach (Platform item in platformCombination.middle)
                {
                    if (item != null)
                    {
                        item.Taken = false;
                    }
                }
                return;
            }

            foreach (Platform platform in platformCombination.middle)
            {
                if (platform == null)
                {
                    if (platformCombination.begin != null)
                        platformCombination.begin.Taken = false;
                    if (platformCombination.end != null)
                        platformCombination.end.Taken = false;
                    foreach (Platform item in platformCombination.middle)
                    {
                        if (item != null)
                        {
                            item.Taken = false;
                        }
                    }
                    return;
                }
            }

            platformCombination.begin.transform.SetParent(platformCombination.combinedGameObject.transform);
            platformCombination.end.transform.SetParent(platformCombination.combinedGameObject.transform);
            foreach (Platform platform in platformCombination.middle)
            {
                platform.transform.SetParent(platformCombination.combinedGameObject.transform);
            }
            
            platformCombination.combinedGameObject.GetComponent<Rigidbody2D>().transform.localPosition = new Vector3(13, low ? 2.5f : 3.5f, 0.5f);
            float pieceSize = 0.6f;
            float xOffset = 0;

            foreach (Platform item in platformCombination.middle)
            {
                xOffset += pieceSize;
                item.transform.localPosition = new Vector3(platformCombination.begin.transform.localPosition.x - xOffset, platformCombination.begin.transform.localPosition.y, platformCombination.begin.transform.localPosition.z);
            }

            platformCombination.begin.transform.localPosition = new Vector3(platformCombination.begin.transform.localPosition.x - xOffset - pieceSize, platformCombination.begin.transform.localPosition.y, platformCombination.begin.transform.localPosition.z);

            spawnedPlatforms.Add(platformCombination);
        }

        //Move all platforms back to their original place
        public void DisableAll() {
            foreach (PlatformCombination platformCombination in spawnedPlatforms)
            {
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
        
        //Cleans this up, expects all gameobjects to have been destroyed before this.
        public void Finalize()
        {
            foreach (PlatformCombination platformCombination in spawnedPlatforms)
            {
                GameObject.Destroy(platformCombination.combinedGameObject);
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
            beginComponents.Clear();
            middleComponents.Clear();
            endComponents.Clear();
        }
    }
}
