using UnityEngine;
using System.Collections;

public class pillarcrash : MonoBehaviour {

    private Rigidbody2D rigid;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = new Vector2(0, -3);
	}
	
	// Update is called once per frame
	void Update () {
        if (rigid.transform.localPosition.y <= 0)
        {
            Destroy(gameObject);
        }
	}
}
