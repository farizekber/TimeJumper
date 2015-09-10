using UnityEngine;
using System.Collections;

public class Boulder : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -3);
	}
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<Rigidbody2D>().transform.localPosition.y <= 0)
        {
            Destroy(gameObject);
        }
	}
}
