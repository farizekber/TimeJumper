using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class Platform : MonoBehaviour {

    public bool colliderEnabled = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (GameObject.Find("Main Character").GetComponent<Rigidbody2D>().transform.localPosition.y < transform.localPosition.y)
        {
            colliderEnabled = false;
        }

        GetComponent<BoxCollider2D>().enabled = colliderEnabled;

        if (!colliderEnabled)
        {
            GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 0.5f);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 1.0f);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Main Character")
        {
            col.gameObject.GetComponent<MainCharacter>().ResetJumps();
        }
    }
}
