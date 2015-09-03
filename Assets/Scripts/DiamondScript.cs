using UnityEngine;
using System.Collections;

public class DiamondScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (ButtonCollision.buttonsCanMove)
            GetComponent<Rigidbody2D>().velocity = new Vector2(-Global.speed, 0);
        else
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }
}
