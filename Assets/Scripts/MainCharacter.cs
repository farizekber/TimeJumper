using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System.Linq;

public class MainCharacter : MonoBehaviour {
    
    float previousClickTime = 0;
    public float clickRate = 0.40f;
    public float speedModifier = 1f;

	// Use this for initialization
	void Start ()
    {
        //Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        //rigid.velocity = new Vector2(rigid.velocity.x, 0);
        Input.simulateMouseWithTouches = true;
    }

    void OnCollisionEnter2D(Collision2D col)
    { }

    // Update is called once per frame
    void Update ()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();

        rigid.transform.localPosition = new Vector3(Mathf.Clamp(rigid.transform.localPosition.x, -4.35f, 4.1f), Mathf.Clamp(rigid.transform.localPosition.y, 0.765f, 10f), rigid.transform.localPosition.z);
    }

    void FixedUpdate()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();

        GetComponent<Animator>().speed = (Global.Instance.speed < -0.19f ? -0.19f : Global.Instance.speed / speedModifier);

        if (rigid.transform.localPosition.y > 0.765f - float.Epsilon && rigid.transform.localPosition.y < 0.765f + float.Epsilon)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 0);
            //Debug.LogFormat("Reset x: {0} y: {1}" , rigid.transform.localPosition.x,rigid.transform.localPosition.y);
        }

        if (GameOverAnimation.GetInstance().m_fAnimationInProgress)
        {
            rigid.velocity = new Vector2(0, 0);
        }
        else
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            if ((Input.GetMouseButtonDown(0) || Input.touches.Any(x => x.phase == TouchPhase.Began)) && (Time.time - previousClickTime > (clickRate / Global.Instance.speed)))
            {
                //Debug.LogFormat("Jump x: {0} y: {1}", rigid.transform.localPosition.x, rigid.transform.localPosition.y);
                previousClickTime = Time.time;
                rigid.velocity = new Vector2(0, 0);
                rigid.AddForce(new Vector2(0, 150));
            }
        }
    }
    
}
