using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class MainCharacter : MonoBehaviour {
    
    public static bool canFly = true;
    float previousClickTime = 0;
    public float clickRate = 0.10f;
    int jumpsSinceGround = 0;


	// Use this for initialization
	void Start () {
	
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Diamond(Clone)")
        {
            Destroy(col.gameObject);
            Global.Instance.score += 1;
            Global.Instance.UpdateScore();
        }
    }

    // Update is called once per frame
    void Update ()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();

        GetComponent<Animator>().speed = (Global.Instance.speed < 0 ? 0 : Global.Instance.speed/4f);
        
        if (rigid.transform.localPosition.y == 0 && jumpsSinceGround > 0)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 0);
            jumpsSinceGround = 0;
            //Debug.LogFormat("Reset x: {0} y: {1}" , rigid.transform.localPosition.x,rigid.transform.localPosition.y);
        }

        if (GameOverAnimation.GetInstance().m_fAnimationInProgress)
        {
            rigid.velocity = new Vector2(0, 0);
        }
        else
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            if ((Input.touchCount > 0 || Input.GetMouseButtonDown(0)) && Time.time - previousClickTime > (clickRate / Global.Instance.speed) && jumpsSinceGround < 2)
            {
                //Debug.LogFormat("Jump x: {0} y: {1}", rigid.transform.localPosition.x, rigid.transform.localPosition.y);
                previousClickTime = Time.time;
                rigid.velocity = new Vector2(0, 0);
                rigid.AddForce(new Vector2(0, 200));
                jumpsSinceGround += 1;
            }
        }

        rigid.transform.localPosition = new Vector3(Mathf.Clamp(rigid.transform.localPosition.x, -4.35f, 4.1f), Mathf.Clamp(rigid.transform.localPosition.y, 0, 10f), rigid.transform.localPosition.z);

    }

    
}
