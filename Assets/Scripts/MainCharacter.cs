using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System.Linq;
using UnityEngine.UI;

public class MainCharacter : MonoBehaviour {

    private class Swipe
    {
        public float xDirectionStart = 0, yDirectionStart = 0;
        public float xDirectionEnd = 0, yDirectionEnd = 0;
        public float xDirectionDelta = 0, yDirectionDelta = 0;
        public bool Tap = false;
        public bool Enabled = false;

        public Swipe() { }
        public void Reset()
        {
            xDirectionStart = 0; yDirectionStart = 0;
            xDirectionEnd = 0; yDirectionEnd = 0;
            xDirectionDelta = 0; yDirectionDelta = 0;
            Tap = false;
            Enabled = false;
        }
    }

    float previousClickTime = 0;
    public float clickRate = 0.40f;
    public float speedModifier = 1f;

    public bool inVehicle = false;
    public int vehicleHealth = 0;

    public int defaultJumps;
    private int jumps;
    private float downTime;
    private Swipe currentSwipe = new Swipe();

    public void ResetJumps()
    {
        jumps = defaultJumps;
    }

	// Use this for initialization
	void Start ()
    {
        Input.simulateMouseWithTouches = false;
        jumps = defaultJumps;
    }

    void OnCollisionEnter2D(Collision2D col)
    { }

    // Update is called once per frame
    void Update ()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();

        if (Global.Instance != null)
        {
            if (Global.Instance.orientation == 0)
                rigid.transform.localPosition = new Vector3(Mathf.Clamp(rigid.transform.localPosition.x, -4.35f, 4.1f), Mathf.Clamp(rigid.transform.localPosition.y, 0.765f, 10f), rigid.transform.localPosition.z);
            else
                rigid.transform.localPosition = new Vector3(Mathf.Clamp(rigid.transform.localPosition.x, -4.35f, 4.1f), Mathf.Clamp(rigid.transform.localPosition.y, -0.19f, 10f), rigid.transform.localPosition.z);
        }
    }

    void FixedUpdate()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();

        GetComponent<Animator>().speed = (Global.Instance.speed < 0.0f ? 0.0f : Global.Instance.speed / speedModifier);

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                currentSwipe.Enabled = true;

                Vector3 test = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));


                currentSwipe.xDirectionStart = test.x;
                currentSwipe.yDirectionStart = test.y;
                currentSwipe.xDirectionEnd = test.x;
                currentSwipe.yDirectionEnd = test.y;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector3 test = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));

                currentSwipe.xDirectionEnd = test.x;
                currentSwipe.yDirectionEnd = test.y;

                currentSwipe.xDirectionDelta = currentSwipe.xDirectionEnd - currentSwipe.xDirectionStart;
                currentSwipe.yDirectionDelta = currentSwipe.yDirectionEnd - currentSwipe.yDirectionStart;

                if (currentSwipe.xDirectionDelta < 0.25f && currentSwipe.xDirectionDelta > -0.25f && currentSwipe.yDirectionDelta < 0.25f && currentSwipe.yDirectionDelta > -0.25f)
                {
                    currentSwipe.xDirectionDelta = 0;
                    currentSwipe.yDirectionDelta = 0;
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Vector3 test = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));

                currentSwipe.xDirectionEnd = test.x;
                currentSwipe.yDirectionEnd = test.y;

                currentSwipe.xDirectionDelta = currentSwipe.xDirectionEnd - currentSwipe.xDirectionStart;
                currentSwipe.yDirectionDelta = currentSwipe.yDirectionEnd - currentSwipe.yDirectionStart;

                if (currentSwipe.xDirectionDelta < 0.25f && currentSwipe.xDirectionDelta > -0.25f && currentSwipe.yDirectionDelta < 0.25f && currentSwipe.yDirectionDelta > -0.25f)
                {
                    currentSwipe.Tap = true;
                    currentSwipe.xDirectionDelta = 0;
                    currentSwipe.yDirectionDelta = 0;
                }
            }
        }

        if (Input.touches.Length == 0)
        {
            currentSwipe.Reset();
        }

        if (Global.Instance.orientation == 0)
        {
            if (rigid.transform.localPosition.y >= 0.765f - float.Epsilon && rigid.transform.localPosition.y <= 0.765f + float.Epsilon)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, 0);
                jumps = defaultJumps;
            }

            if (GameOverAnimation.GetInstance().m_fAnimationInProgress)
            {
                rigid.velocity = new Vector2(0, 0);
            }
            else
            {
                rigid.velocity = new Vector2(0, rigid.velocity.y);
                if ((Input.GetMouseButtonDown(0) || (currentSwipe.Enabled && currentSwipe.Tap)) && (Time.time > previousClickTime + (clickRate / Global.Instance.speed)))
                {
                    if (jumps > 0 || jumps == -1)
                    {
                        previousClickTime = Time.time;
                        rigid.velocity = new Vector2(0, 0);
                        rigid.AddForce(new Vector2(0, 250));

                        if (jumps >= 0)
                            jumps--;
                    }
                }
            }
        }
        else
        {
            if (GameOverAnimation.GetInstance().m_fAnimationInProgress)
            {
                rigid.velocity = new Vector2(0, 0);
            }

            if (currentSwipe.Enabled || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                if (currentSwipe.Enabled)
                {
                    //if (rigid.velocity.y > 0 && currentSwipe.yDirectionEnd < 3.1f || rigid.velocity.y < 0 && currentSwipe.yDirectionEnd > 3.1f) rigid.velocity = new Vector2(0, 0);
                    rigid.velocity = new Vector2(0, 0);
                    if (currentSwipe.yDirectionEnd < 3.1f)
                    {
                        rigid.AddForce(new Vector2(0, -100f));
                    }
                    else
                    {
                        rigid.AddForce(new Vector2(0, 100f));
                    }
                }
                else
                {
                    rigid.velocity = new Vector2(0, 0);
                    rigid.AddForce(new Vector2(0, (Input.GetMouseButtonDown(0) ? 1 : -1) * 100.0f));
                }
            }
        }
    }
}
