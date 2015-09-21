using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System;

public class Platform : MonoBehaviour {

    //private Rigidbody2D mainCharacterRigidBody;
    private MainCharacter mainCharacter;
    private SpriteRenderer spriteRenderer;
    private Collider2D m_collider;
    public bool colliderEnabled = true;
    public bool Taken;

    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        m_collider = GetComponent<BoxCollider2D>();
        mainCharacter = GameObject.Find("Main Character").GetComponent<MainCharacter>();
    }
	
	// Update is called once per frame
	void Update () {
        //if (mainCharacterRigidBody.transform.localPosition.y < transform.localPosition.y)
        //{
        //    colliderEnabled = false;
        //}

        m_collider.enabled = colliderEnabled;

        if (!colliderEnabled)
        {
            if(spriteRenderer.color.a + float.Epsilon > 0.5)
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);
        }
        else
        {
            if (spriteRenderer.color.a - float.Epsilon < 1.0)
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1.0f);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Main Character")
        {
            mainCharacter.ResetJumps();
        }
    }

    public void Disable()
    {
        transform.localPosition = new Vector3(-8, 0, 1.5f);
    }
}
