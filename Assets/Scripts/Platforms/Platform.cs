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
        m_collider.enabled = colliderEnabled;

        Color c = spriteRenderer.color;
        if (!colliderEnabled)
        {
            if(c.a + float.Epsilon > 0.5)
                spriteRenderer.color = new Color(c.r, c.g, c.b, 0.5f);
        }
        else
        {
            if (c.a - float.Epsilon < 1.0)
                spriteRenderer.color = new Color(c.r, c.g, c.b, 1.0f);
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
