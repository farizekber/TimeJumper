using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class MainCharacter : MonoBehaviour {
    
    public static bool canFly = true;
    float previousClickTime = 0;
    public float clickRate = 0.10f;


	// Use this for initialization
	void Start () {
	
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Diamond(Clone)")
        {
            Destroy(col.gameObject);
            Global.score += 1;
            Global.UpdateScore();
        }
    }

    // Update is called once per frame
    void Update ()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();

        GetComponent<Animator>().speed = (Global.speed < 0 ? 0 : Global.speed/4f);
        
        if (rigid.transform.localPosition.y <= 0)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 0);
        }

        if (GameOverAnimation.GetInstance().m_fAnimationInProgress)
        {
            rigid.velocity = new Vector2(0, 0);
        }
        else
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            if ((Input.touchCount > 0 || Input.GetMouseButtonDown(0)) && Time.time - previousClickTime > (clickRate / Global.speed))
            {
                previousClickTime = Time.time;
                if (canFly || rigid.transform.localPosition.y <= 0)
                {
                    rigid.AddForce(new Vector2(0, 150));
                }
            }
        }

        rigid.transform.localPosition = new Vector3(Mathf.Clamp(rigid.transform.localPosition.x, -4.35f, 4.1f), Mathf.Clamp(rigid.transform.localPosition.y, 0, 10f), rigid.transform.localPosition.z);

    }

    
}
