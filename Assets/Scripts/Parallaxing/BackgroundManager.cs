using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Parallaxing
{
    public class BackgroundManager : MonoBehaviour
    {
        public Sprite mineFloor;
        public Sprite iceFloor;

        public void InitHorizontal(PerspectiveInitializer.ThemeState themeState)
        {
            GameObject background = GameObject.Find("Background Manager");
            for (int i = 0; i < background.transform.childCount; ++i)
            {
                background.transform.GetChild(i).gameObject.SetActive(background.transform.GetChild(i).gameObject.name.Length < 8);
            }
            AdjustTheme(themeState);
        }

        public void InitVertical(PerspectiveInitializer.ThemeState themeState)
        {
            GameObject background = GameObject.Find("Background Manager");
            for (int i = 0; i < background.transform.childCount; ++i)
            {
                background.transform.GetChild(i).gameObject.SetActive(background.transform.GetChild(i).gameObject.name.Length >= 8);
            }
            AdjustTheme(themeState);
        }

        void FixedUpdate()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                BackgroundPlane backgroundPlane = transform.GetChild(i).gameObject.GetComponent<BackgroundPlane>();

                if (backgroundPlane.pieces[0] == null)
                    continue;

                if (!backgroundPlane.enabled)
                    continue;

                backgroundPlane.pieces[0].transform.localPosition = new Vector3((backgroundPlane.pieces[0].transform.localPosition.x - (1.0f * Time.fixedDeltaTime * 0.075f * Global.Instance.speed) * backgroundPlane.SpeedModifier), backgroundPlane.pieces[0].transform.localPosition.y, backgroundPlane.pieces[0].transform.localPosition.z);
                backgroundPlane.pieces[1].transform.localPosition = new Vector3((backgroundPlane.pieces[1].transform.localPosition.x - (1.0f * Time.fixedDeltaTime * 0.075f * Global.Instance.speed) * backgroundPlane.SpeedModifier), backgroundPlane.pieces[1].transform.localPosition.y, backgroundPlane.pieces[1].transform.localPosition.z);

                if (backgroundPlane.Horizontal)
                {
                    if (backgroundPlane.pieces[0].transform.localPosition.x < -backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().bounds.size.x / transform.localScale.x /*- (backgroundPlane.Original ? 0.0f : 0.01f)*/)
                        backgroundPlane.pieces[0].transform.localPosition = new Vector3(backgroundPlane.pieces[0].transform.localPosition.x + backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().bounds.size.x / transform.localScale.x * 2.0f /*- (backgroundPlane.Original ? 0.0f : 0.02f)*/, backgroundPlane.pieces[0].transform.localPosition.y, backgroundPlane.pieces[0].transform.localPosition.z);

                    if (backgroundPlane.pieces[1].transform.localPosition.x < -backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().bounds.size.x / transform.localScale.x /*- (backgroundPlane.Original ? 0.0f : 0.01f)*/)
                        backgroundPlane.pieces[1].transform.localPosition = new Vector3(backgroundPlane.pieces[1].transform.localPosition.x + backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().bounds.size.x / transform.localScale.x * 2.0f /*- (backgroundPlane.Original ? 0.0f : 0.02f)*/, backgroundPlane.pieces[1].transform.localPosition.y, backgroundPlane.pieces[1].transform.localPosition.z);
                }
                else
                {
                    if (backgroundPlane.pieces[0].transform.localPosition.x > backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().bounds.size.x / transform.localScale.x /*- (backgroundPlane.Original ? 0.0f : 0.01f)*/)
                        backgroundPlane.pieces[0].transform.localPosition = new Vector3(backgroundPlane.pieces[0].transform.localPosition.x - backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().bounds.size.x / transform.localScale.x * 2.0f /*- (backgroundPlane.Original ? 0.0f : 0.02f)*/, backgroundPlane.pieces[0].transform.localPosition.y, backgroundPlane.pieces[0].transform.localPosition.z);

                    if (backgroundPlane.pieces[1].transform.localPosition.x > backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().bounds.size.x / transform.localScale.x /*- (backgroundPlane.Original ? 0.0f : 0.01f)*/)
                        backgroundPlane.pieces[1].transform.localPosition = new Vector3(backgroundPlane.pieces[1].transform.localPosition.x - backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().bounds.size.x / transform.localScale.x * 2.0f /*- (backgroundPlane.Original ? 0.0f : 0.02f)*/, backgroundPlane.pieces[1].transform.localPosition.y, backgroundPlane.pieces[1].transform.localPosition.z);
                }
            }
        }

        void Init()
        {
            Camera.main.transform.localScale = new Vector3(Camera.main.transform.localScale.x * Camera.main.aspect, Camera.main.transform.localScale.y, Camera.main.transform.localScale.z);
        }

        public void AdjustTheme(PerspectiveInitializer.ThemeState themeState)
        {
            if (themeState == PerspectiveInitializer.ThemeState.Mine)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    BackgroundPlane backgroundPlane = transform.GetChild(i).gameObject.GetComponent<BackgroundPlane>();

                    if (backgroundPlane.pieces[0] == null)
                        continue;

                    if (backgroundPlane.name == "bg1")
                    {
                        backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.originalBackgroundImages[0];
                        backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.originalBackgroundImages[0];
                        backgroundPlane.Original = true;
                        backgroundPlane.SetTransform();
                    }
                    else if (backgroundPlane.name == "bg2")
                    {
                        backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.originalBackgroundImages[1];
                        backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.originalBackgroundImages[1];
                        backgroundPlane.Original = true;
                        backgroundPlane.SetTransform();
                    }
                    else if (backgroundPlane.name == "bg3")
                    {
                        backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.originalBackgroundImages[2];
                        backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.originalBackgroundImages[2];
                        backgroundPlane.Original = true;
                        backgroundPlane.SetTransform();
                    }
                    else if (backgroundPlane.name == "bg4")
                    {
                        backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.originalBackgroundImages[3];
                        backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.originalBackgroundImages[3];
                        backgroundPlane.Original = true;
                        backgroundPlane.SetTransform();
                    }
                    else if (backgroundPlane.name == "bg5")
                    {
                        backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.originalBackgroundImages[4];
                        backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.originalBackgroundImages[4];
                        backgroundPlane.Original = true;
                        backgroundPlane.SetTransform();
                    }
                    else if (backgroundPlane.name == "bg6")
                    {
                        backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().sprite = mineFloor;
                        backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().sprite = mineFloor;
                        break;
                    }
                    else if (backgroundPlane.name == "bg1-Vertical")
                    {
                        backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.originalBackgroundImages[5];
                        backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.originalBackgroundImages[5];
                        backgroundPlane.Original = true;
                        backgroundPlane.SetTransform();
                    }
                    else if (backgroundPlane.name == "bg2-Vertical")
                    {
                        backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.originalBackgroundImages[6];
                        backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.originalBackgroundImages[6];
                        backgroundPlane.Original = true;
                        backgroundPlane.SetTransform();
                    }
                    else if (backgroundPlane.name == "bg3-Vertical")
                    {
                        backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.originalBackgroundImages[7];
                        backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.originalBackgroundImages[7];
                        backgroundPlane.Original = true;
                        backgroundPlane.SetTransform();
                    }
                    else if (backgroundPlane.name == "bg4-Vertical")
                    {
                        backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.originalBackgroundImages[8];
                        backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.originalBackgroundImages[8];
                        backgroundPlane.Original = true;
                        backgroundPlane.SetTransform();
                    }
                    else if (backgroundPlane.name == "bg5-Vertical")
                    {
                        backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.originalBackgroundImages[9];
                        backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.originalBackgroundImages[9];
                        backgroundPlane.Original = true;
                        backgroundPlane.SetTransform();
                    }
                }
            }
            else
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    BackgroundPlane backgroundPlane = transform.GetChild(i).gameObject.GetComponent<BackgroundPlane>();

                    if (backgroundPlane.pieces[0] == null)
                        continue;

                    if (PlayerPrefs.GetInt("UseShader") == 1)
                    {
                        if (backgroundPlane.name == "bg1")
                        {
                            backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.enhancedBackgroundImages[0];
                            backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.enhancedBackgroundImages[0];
                            backgroundPlane.Original = false;
                            backgroundPlane.SetTransform();
                        }
                        else if (backgroundPlane.name == "bg2")
                        {
                            backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.enhancedBackgroundImages[1];
                            backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.enhancedBackgroundImages[1];
                            backgroundPlane.Original = false;
                            backgroundPlane.SetTransform();
                        }
                        else if (backgroundPlane.name == "bg3")
                        {
                            backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.enhancedBackgroundImages[2];
                            backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.enhancedBackgroundImages[2];
                            backgroundPlane.Original = false;
                            backgroundPlane.SetTransform();
                        }
                        else if (backgroundPlane.name == "bg4")
                        {
                            backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.enhancedBackgroundImages[3];
                            backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.enhancedBackgroundImages[3];
                            backgroundPlane.Original = false;
                            backgroundPlane.SetTransform();
                        }
                        else if (backgroundPlane.name == "bg5")
                        {
                            backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.enhancedBackgroundImages[4];
                            backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.enhancedBackgroundImages[4];
                            backgroundPlane.Original = false;
                            backgroundPlane.SetTransform();
                        }
                        else if (backgroundPlane.name == "bg6")
                        {
                            backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().sprite = iceFloor;
                            backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().sprite = iceFloor;
                            break;
                        }
                        else if (backgroundPlane.name == "bg1-Vertical")
                        {
                            backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.enhancedBackgroundImages[5];
                            backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.enhancedBackgroundImages[5];
                            backgroundPlane.Original = false;
                            backgroundPlane.SetTransform();
                        }
                        else if (backgroundPlane.name == "bg2-Vertical")
                        {
                            backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.enhancedBackgroundImages[6];
                            backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.enhancedBackgroundImages[6];
                            backgroundPlane.Original = false;
                            backgroundPlane.SetTransform();
                        }
                        else if (backgroundPlane.name == "bg3-Vertical")
                        {
                            backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.enhancedBackgroundImages[7];
                            backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.enhancedBackgroundImages[7];
                            backgroundPlane.Original = false;
                            backgroundPlane.SetTransform();
                        }
                        else if (backgroundPlane.name == "bg4-Vertical")
                        {
                            backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.enhancedBackgroundImages[8];
                            backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.enhancedBackgroundImages[8];
                            backgroundPlane.Original = false;
                            backgroundPlane.SetTransform();
                        }
                        else if (backgroundPlane.name == "bg5-Vertical")
                        {
                            backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.enhancedBackgroundImages[9];
                            backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.enhancedBackgroundImages[9];
                            backgroundPlane.Original = false;
                            backgroundPlane.SetTransform();
                        }
                    }
                    else
                    {
                        if (backgroundPlane.name == "bg1")
                        {
                            backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.backupBackgroundImages[0];
                            backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.backupBackgroundImages[0];
                            backgroundPlane.Original = true;
                            backgroundPlane.SetTransform();
                        }
                        else if (backgroundPlane.name == "bg2")
                        {
                            backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.backupBackgroundImages[1];
                            backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.backupBackgroundImages[1];
                            backgroundPlane.Original = true;
                            backgroundPlane.SetTransform();
                        }
                        else if (backgroundPlane.name == "bg3")
                        {
                            backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.backupBackgroundImages[2];
                            backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.backupBackgroundImages[2];
                            backgroundPlane.Original = true;
                            backgroundPlane.SetTransform();
                        }
                        else if (backgroundPlane.name == "bg4")
                        {
                            backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.backupBackgroundImages[3];
                            backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.backupBackgroundImages[3];
                            backgroundPlane.Original = true;
                            backgroundPlane.SetTransform();
                        }
                        else if (backgroundPlane.name == "bg5")
                        {
                            backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.backupBackgroundImages[4];
                            backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.backupBackgroundImages[4];
                            backgroundPlane.Original = true;
                            backgroundPlane.SetTransform();
                        }
                        else if (backgroundPlane.name == "bg6")
                        {
                            backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().sprite = iceFloor;
                            backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().sprite = iceFloor;
                            break;
                        }
                        else if (backgroundPlane.name == "bg1-Vertical")
                        {
                            backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.backupBackgroundImages[5];
                            backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.backupBackgroundImages[5];
                            backgroundPlane.Original = true;
                            backgroundPlane.SetTransform();
                        }
                        else if (backgroundPlane.name == "bg2-Vertical")
                        {
                            backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.backupBackgroundImages[6];
                            backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.backupBackgroundImages[6];
                            backgroundPlane.Original = true;
                            backgroundPlane.SetTransform();
                        }
                        else if (backgroundPlane.name == "bg3-Vertical")
                        {
                            backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.backupBackgroundImages[7];
                            backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.backupBackgroundImages[7];
                            backgroundPlane.Original = true;
                            backgroundPlane.SetTransform();
                        }
                        else if (backgroundPlane.name == "bg4-Vertical")
                        {
                            backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.backupBackgroundImages[8];
                            backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.backupBackgroundImages[8];
                            backgroundPlane.Original = true;
                            backgroundPlane.SetTransform();
                        }
                        else if (backgroundPlane.name == "bg5-Vertical")
                        {
                            backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.backupBackgroundImages[9];
                            backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().sprite = RenderTextureManager.instance.backupBackgroundImages[9];
                            backgroundPlane.Original = true;
                            backgroundPlane.SetTransform();
                        }
                    }
                }
            }
        }
    }
}
