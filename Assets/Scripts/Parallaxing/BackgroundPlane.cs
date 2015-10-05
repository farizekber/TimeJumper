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
        
        public float RedMultiplier;
        public float GreenMultiplier;
        public float BlueMultiplier;
        public bool RedInversed;
        public bool GreenInversed;
        public bool BlueInversed;

        public bool Replace;

        void Start()
        {
            pieces[0] = Instantiate(Resources.Load("Prefabs/BackgroundPiece"), new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
            pieces[1] = Instantiate(Resources.Load("Prefabs/BackgroundPiece"), new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;

            pieces[0].GetComponent<SpriteRenderer>().sprite = background;
            pieces[1].GetComponent<SpriteRenderer>().sprite = background;

            pieces[0].GetComponent<SpriteRenderer>().material = Resources.Load("Materials/diffuseSprite") as Material;
            pieces[1].GetComponent<SpriteRenderer>().material = Resources.Load("Materials/diffuseSprite") as Material;

            pieces[0].GetComponent<SpriteRenderer>().material.SetFloat("_Replace", Replace ? 1.0f : 0.0f);
            pieces[1].GetComponent<SpriteRenderer>().material.SetFloat("_Replace", Replace ? 1.0f : 0.0f);
            
            pieces[0].transform.parent = transform;
            pieces[1].transform.parent = transform;

            pieces[0].transform.localScale = new Vector3(pieces[0].transform.localScale.x * 1.33f, pieces[0].transform.localScale.y, pieces[0].transform.localScale.z);
            pieces[1].transform.localScale = new Vector3(pieces[1].transform.localScale.x * 1.33f, pieces[1].transform.localScale.y, pieces[1].transform.localScale.z);

            pieces[0].transform.localPosition = new Vector3(0f, 0, -0.0001f);
            pieces[1].transform.localPosition = new Vector3(pieces[0].GetComponent<SpriteRenderer>().bounds.size.x / transform.parent.localScale.x, 0, 0);
        }

        void Update()
        {
        }
    }
}
