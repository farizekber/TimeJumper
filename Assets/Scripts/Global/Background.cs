using UnityEngine;

public class Background : MonoBehaviour {

    float tickCount = 0;
    public static bool horizontal = true;

    // Use this for initialization
    void Start()
    {
        Camera.main.transform.localScale = new Vector3(Camera.main.transform.localScale.x * Camera.main.aspect, Camera.main.transform.localScale.y, Camera.main.transform.localScale.z);
    }

    // Update is called once per frame
    void Update () {
        GetComponent<MeshRenderer>().material.SetFloat("_TickCount", (tickCount += (Time.fixedDeltaTime * 0.075f * Global.Instance.speed)) - Global.Instance.delay.ElapsedMilliseconds / 1000.0f);
        GetComponent<MeshRenderer>().material.SetFloat("_Horizontal", horizontal ? 10 : -10);
    }
}
