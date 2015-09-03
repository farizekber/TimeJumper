using UnityEngine;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;

public class ButtonCollision : MonoBehaviour {

    public Sprite sprite;
    public Sprite spritePushed;
    public int twirlTimeInSeconds;
    public bool ButtonPushed = false;
    float initiatedTime = 0;
    Component twirl = null;
    public static List<GameObject> buttons = new List<GameObject>();
    public static bool buttonsCanMove = true;

	// Use this for initialization
	void Start ()
    {
        foreach (Component component in GameObject.Find("Main Camera").GetComponents<Component>())
        {
            if (component is UnityStandardAssets.ImageEffects.Twirl)
            {
                twirl = component;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (buttonsCanMove)
            GetComponent<Rigidbody2D>().velocity = new Vector2(-Global.speed, 0);
        else
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        if (ButtonPushed)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            (twirl as UnityStandardAssets.ImageEffects.Twirl).angle++;

            if (initiatedTime + twirlTimeInSeconds < Time.time && (((twirl as UnityStandardAssets.ImageEffects.Twirl).angle % 360) == 0))
            {
                ButtonPushed = false;
                MainCharacter.canMove = true;
                initiatedTime = 0;
                Global.delay.Stop();
                buttonsCanMove = true;
                Destroy(this.gameObject);
            }
        }

        if (transform.localPosition.x < -10)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        initiatedTime = Time.time;
        ButtonPushed = true;
        MainCharacter.canMove = false;
        GetComponent<SpriteRenderer>().sprite = spritePushed;
        buttonsCanMove = false;
        Global.delay.Start();
    }

    void OnTriggerStay2D(Collider2D other) { }

    void OnTriggerExit2D(Collider2D other) { }
}
