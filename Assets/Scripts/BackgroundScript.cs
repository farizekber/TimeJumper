using UnityEngine;
using System.Collections;

public class BackgroundScript : MonoBehaviour {

    float tickCount = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<MeshRenderer>().material.SetFloat("_TickCount", tickCount += (Time.fixedDeltaTime * 0.075f * Global.speed));
	}
}
