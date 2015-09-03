using UnityEngine;
using System.Collections;
using System.Reflection;

public class ButtonCollision : MonoBehaviour {

    public Sprite sprite;
    public Sprite spritePushed;
    public int twirlTimeInSeconds;
    public static bool ButtonPushed = false;
    float initiatedTime = 0;
    Component twirl = null;

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
        if (ButtonPushed)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            (twirl as UnityStandardAssets.ImageEffects.Twirl).angle++;

            if (initiatedTime + twirlTimeInSeconds < Time.time && (((twirl as UnityStandardAssets.ImageEffects.Twirl).angle % 360) == 0))
            {
                ButtonPushed = false;
                initiatedTime = 0;
                GetComponent<SpriteRenderer>().sprite = sprite;
            }
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-Global.speed, 0);
        }

        if (transform.localPosition.x < -10)
        {
            Destroy(this);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        initiatedTime = Time.time;
        ButtonPushed = true;
        GetComponent<SpriteRenderer>().sprite = spritePushed;
    }

    void OnTriggerStay2D(Collider2D other) { }

    void OnTriggerExit2D(Collider2D other) { }
}
