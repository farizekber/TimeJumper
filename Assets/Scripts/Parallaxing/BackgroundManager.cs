using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Parallaxing
{
    public class BackgroundManager : MonoBehaviour
    {
        public void UpdateShader()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                BackgroundPlane backgroundPlane = transform.GetChild(i).gameObject.GetComponent<BackgroundPlane>();

                if (backgroundPlane.pieces[0] == null)
                    continue;
                
                backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().material.SetFloat("_RedMultiplier", backgroundPlane.RedMultiplier);
                backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().material.SetFloat("_GreenMultiplier", backgroundPlane.GreenMultiplier);
                backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().material.SetFloat("_BlueMultiplier", backgroundPlane.BlueMultiplier);
                backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().material.SetFloat("_RedInversed", backgroundPlane.RedInversed ? 1.0f : 0.0f);
                backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().material.SetFloat("_GreenInversed", backgroundPlane.GreenInversed ? 1.0f : 0.0f);
                backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().material.SetFloat("_BlueInversed", backgroundPlane.BlueInversed ? 1.0f : 0.0f);

                backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().material.SetFloat("_RedMultiplier", backgroundPlane.RedMultiplier);
                backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().material.SetFloat("_GreenMultiplier", backgroundPlane.GreenMultiplier);
                backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().material.SetFloat("_BlueMultiplier", backgroundPlane.BlueMultiplier);
                backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().material.SetFloat("_RedInversed", backgroundPlane.RedInversed ? 1.0f : 0.0f);
                backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().material.SetFloat("_GreenInversed", backgroundPlane.GreenInversed ? 1.0f : 0.0f);
                backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().material.SetFloat("_BlueInversed", backgroundPlane.BlueInversed ? 1.0f : 0.0f);
            }
        }

        public void InitHorizontal()
        {
            GameObject background = GameObject.Find("Background Manager");
            for (int i = 0; i < background.transform.childCount; ++i)
            {
                background.transform.GetChild(i).gameObject.SetActive(background.transform.GetChild(i).gameObject.name.Length < 8);
            }
        }

        public void InitVertical()
        {
            GameObject background = GameObject.Find("Background Manager");
            for (int i = 0; i < background.transform.childCount; ++i)
            {
                background.transform.GetChild(i).gameObject.SetActive(background.transform.GetChild(i).gameObject.name.Length >= 8);
            }
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
                    if (backgroundPlane.pieces[0].transform.localPosition.x < -backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().bounds.size.x / transform.localScale.x)
                        backgroundPlane.pieces[0].transform.localPosition = new Vector3(backgroundPlane.pieces[0].transform.localPosition.x + backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().bounds.size.x / transform.localScale.x * 2.0f, backgroundPlane.pieces[0].transform.localPosition.y, backgroundPlane.pieces[0].transform.localPosition.z);

                    if (backgroundPlane.pieces[1].transform.localPosition.x < -backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().bounds.size.x / transform.localScale.x)
                        backgroundPlane.pieces[1].transform.localPosition = new Vector3(backgroundPlane.pieces[1].transform.localPosition.x + backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().bounds.size.x / transform.localScale.x * 2.0f, backgroundPlane.pieces[1].transform.localPosition.y, backgroundPlane.pieces[1].transform.localPosition.z);
                }
                else
                {
                    if (backgroundPlane.pieces[0].transform.localPosition.x > backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().bounds.size.x / transform.localScale.x)
                        backgroundPlane.pieces[0].transform.localPosition = new Vector3(backgroundPlane.pieces[0].transform.localPosition.x - backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().bounds.size.x / transform.localScale.x * 2.0f, backgroundPlane.pieces[0].transform.localPosition.y, backgroundPlane.pieces[0].transform.localPosition.z);

                    if (backgroundPlane.pieces[1].transform.localPosition.x > backgroundPlane.pieces[1].GetComponent<SpriteRenderer>().bounds.size.x / transform.localScale.x)
                        backgroundPlane.pieces[1].transform.localPosition = new Vector3(backgroundPlane.pieces[1].transform.localPosition.x - backgroundPlane.pieces[0].GetComponent<SpriteRenderer>().bounds.size.x / transform.localScale.x * 2.0f, backgroundPlane.pieces[1].transform.localPosition.y, backgroundPlane.pieces[1].transform.localPosition.z);
                }
            }
        }

        void Init()
        {
            Camera.main.transform.localScale = new Vector3(Camera.main.transform.localScale.x * Camera.main.aspect, Camera.main.transform.localScale.y, Camera.main.transform.localScale.z);
            UpdateShader();
        }
    }
}
