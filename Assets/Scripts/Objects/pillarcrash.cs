using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class pillarcrash : MonoBehaviour {

    private Rigidbody2D rigid;
    private Vector3 startLocation = new Vector3(3f, 11f, 1f);
    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody2D>();
        rigid.transform.localPosition += Global.Instance.GlobalObject.transform.localPosition + Global.Instance.ForegroundObject.transform.localPosition;
        rigid.transform.parent = Global.Instance.ForegroundObject.transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (rigid.transform.localPosition.y <= 3.58f)
        {

            GameObject crash;
            if (GameObject.Find("PerspectiveInitializer").GetComponent<PerspectiveInitializer>().themeState == PerspectiveInitializer.ThemeState.Mine)
                crash = GameObject.Find("Crash(Clone)");
            else
                crash = GameObject.Find("ice-crash(Clone)");

            crash.GetComponent<Crash>().Trigger();

            /*
            if (crash.transform.localPosition.x <= 3)
            {
                
            }
            */

            if (rigid.transform.localPosition.y <= -2.58f)
            {
                rigid.velocity = new Vector2(0, 0);
                rigid.transform.localPosition = startLocation;
            }
        }
	}

    public void Trigger()
    {
        rigid.velocity = new Vector2(0, -3.25f);
    }
}
