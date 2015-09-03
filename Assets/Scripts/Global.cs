using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour {

    public static float speed = 1f;
    
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            speed--;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            speed++;
	}
}
