using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System.Linq;
using UnityEngine.UI;

public class MainCharacter : MonoBehaviour {

    float previousClickTime = 0;
    public float clickRate = 0.40f;
    public float speedModifier = 1f;

    public bool inVehicle = false;
    public int vehicleHealth = 0;

    public int defaultJumps;
    private int jumps;
    private float downTime;
    private Swipe currentSwipe = new Swipe();
    Rigidbody2D rigid;
    Animator animator;

    public void ResetJumps()
    {
        jumps = defaultJumps;
    }

	// Use this for initialization
	void Start ()
    {
        Input.simulateMouseWithTouches = false;
        jumps = defaultJumps;
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Global.Instance == null)
            return;

        if(animator != null)
            animator.speed = (Global.Instance.speed < 0.0f ? 0.0f : Global.Instance.speed / speedModifier);

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

        SpawnManager.Instance.platformManager.UpdateActive(currentSwipe, transform, Input.GetKey(KeyCode.DownArrow));

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
                        rigid.AddForce(new Vector2(0, 150.0f * 1.5f));

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
            else
            {
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

        if (Global.Instance != null)
        {
            if (Global.Instance.orientation == 0)
                rigid.transform.localPosition = new Vector3(Mathf.Clamp(rigid.transform.localPosition.x, -4.35f, 4.1f), Mathf.Clamp(rigid.transform.localPosition.y, 0.765f, 6.45f), rigid.transform.localPosition.z);
            else
                rigid.transform.localPosition = new Vector3(Mathf.Clamp(rigid.transform.localPosition.x, -4.35f, 4.1f), Mathf.Clamp(rigid.transform.localPosition.y, -0.19f, 6.45f), rigid.transform.localPosition.z);
        }
    }
}
