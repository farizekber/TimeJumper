using UnityEngine;
using System.Collections;

public class BatScript : MonoBehaviour
{
    public Sprite sprite;
    public int twirlTimeInSeconds;
    public bool ButtonPushed = false;
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
	void Update ()
    {
        if (ButtonCollision.buttonsCanMove)
            GetComponent<Rigidbody2D>().velocity = new Vector2(-1.5f * Global.speed, 0);
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
                ButtonCollision.buttonsCanMove = true;
                Destroy(this.gameObject);
                Application.LoadLevel("GameOver");
            }
        }

        if (transform.localPosition.x < -10)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Main Character")
        {
            initiatedTime = Time.time;
            ButtonPushed = true;
            MainCharacter.canMove = false;
            ButtonCollision.buttonsCanMove = false;
            Global.delay.Start();
        }
    }

    void OnTriggerStay2D(Collider2D other) { }

    void OnTriggerExit2D(Collider2D other) { }
}
