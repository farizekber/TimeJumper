using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Parallaxing
{
    public class BackgroundPlane : MonoBehaviour
    {
        public Sprite background;
        public GameObject[] pieces = new GameObject[2];
        public float SpeedModifier;
        public bool Horizontal;
        public bool Original = true;
        public bool originalScaleValuesChanged = false;
        public Vector3 originalScale0;
        public Vector3 originalScale1;

        public void SetTransform()
        {
            //if (name == "bg4")
            //{
            //    pieces[0].transform.localScale = new Vector3(1, 0.4f, 1);
            //    pieces[1].transform.localScale = new Vector3(1, 0.4f, 1);
            //}


            //if (name == "bg4")
            //{
            //    pieces[0].transform.localPosition = new Vector3(0f, -0.02f, -0.0001f);
            //    pieces[1].transform.localPosition = new Vector3(pieces[0].GetComponent<SpriteRenderer>().bounds.size.x / transform.parent.localScale.x, -0.02f, 0f);
            //}
            //else
            //{;
            //}

            if (name == "bg1" && !Original)
            {
                //pieces[0].transform.localScale = new Vector3(pieces[0].transform.localScale.x* 1.33f * (Original? 1.0f : 0.66f), pieces[0].transform.localScale.y* 1.4f, pieces[0].transform.localScale.z);
                //pieces[1].transform.localScale = new Vector3(pieces[1].transform.localScale.x* 1.33f * (Original? 1.0f : 0.66f), pieces[1].transform.localScale.y* 1.4f, pieces[1].transform.localScale.z);
                pieces[0].transform.localScale = new Vector3(originalScale0.x * 1.33f * (Original ? 1.0f : 0.66f), originalScale0.y * 1.4f, originalScale0.z);
                pieces[1].transform.localScale = new Vector3(originalScale1.x * 1.33f * (Original ? 1.0f : 0.66f), originalScale1.y * 1.4f, originalScale1.z);

                pieces[0].transform.localPosition = new Vector3(0f, 0, -0.0001f);
                pieces[1].transform.localPosition = new Vector3(pieces[0].GetComponent<SpriteRenderer>().bounds.size.x / transform.parent.localScale.x, 0, 0);
            }
            else
            {
                //pieces[0].transform.localScale = new Vector3(pieces[0].transform.localScale.x * 1.33f * (Original? 1.0f : 0.66f), pieces[0].transform.localScale.y, pieces[0].transform.localScale.z);
                //pieces[1].transform.localScale = new Vector3(pieces[1].transform.localScale.x * 1.33f * (Original? 1.0f : 0.66f), pieces[1].transform.localScale.y, pieces[1].transform.localScale.z);
                pieces[0].transform.localScale = new Vector3(originalScale0.x * 1.33f * (Original ? 1.0f : 0.66f), originalScale0.y, originalScale0.z);
                pieces[1].transform.localScale = new Vector3(originalScale1.x * 1.33f * (Original ? 1.0f : 0.66f), originalScale1.y, originalScale1.z);

                pieces[0].transform.localPosition = new Vector3(0f, 0, -0.0001f);
                pieces[1].transform.localPosition = new Vector3(pieces[0].GetComponent<SpriteRenderer>().bounds.size.x / transform.parent.localScale.x, 0, 0);
            }
        }

        void Start()
        {
            pieces[0] = Instantiate(Resources.Load("Prefabs/BackgroundPiece"), new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
            pieces[1] = Instantiate(Resources.Load("Prefabs/BackgroundPiece"), new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;

            pieces[0].GetComponent<SpriteRenderer>().sprite = background;
            pieces[1].GetComponent<SpriteRenderer>().sprite = background;
            
            pieces[0].GetComponent<SpriteRenderer>().material = Resources.Load("Materials/defaultSpriteDiffuse") as Material;
            pieces[1].GetComponent<SpriteRenderer>().material = Resources.Load("Materials/defaultSpriteDiffuse") as Material;

            pieces[0].transform.parent = transform;
            pieces[1].transform.parent = transform;

            if (!originalScaleValuesChanged)
            {
                originalScaleValuesChanged = true;
                originalScale0 = pieces[0].transform.localScale;
                originalScale1 = pieces[1].transform.localScale;
            }
            SetTransform();

            if (Global.Instance.orientation == 0)
            {
                gameObject.SetActive(name.Length < 8);
            }
            else
            {
                gameObject.SetActive(name.Length >= 8);
            }
        }
    }
}
