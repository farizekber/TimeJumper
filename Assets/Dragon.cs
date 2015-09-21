using UnityEngine;
using System.Collections;

public class Dragon : MonoBehaviour
{
    public float x;// = animationEvent.intParameter / 1000.0f;
    public float intensity;// = animationEvent.floatParameter;
    public bool pointLightingOn = false;
    private Light m_light;

    // Use this for initialization
    void Start () {
        m_light = GameObject.Find("Directional light").GetComponent<Light>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (pointLightingOn)
        {
            m_light.intensity = intensity * 0.1f;//0
            m_light.type = LightType.Point;
            m_light.color = new Color(188, 85, 41, 255);
            m_light.transform.localPosition = new Vector3(x, 0.008f, 5);
        }
        else
        {
            m_light.intensity = 1.7f;
            m_light.type = LightType.Directional;
            m_light.color = new Color(1, 1, 1);
        }
    }

    public void ToggleLighting(int lightingOn)
    {
        if (lightingOn == 0)
            this.pointLightingOn = false;
        else
            this.pointLightingOn = true;
    }

//    public void UpdateLighting()
//    {
////0.34f
//        //switch (phase)
//        //{
//        //    case 0:
//        //        GameObject.Find("Directional light").GetComponent<Light>().intensity = 0;
//        //        GameObject.Find("Directional light").GetComponent<Light>().type = LightType.Point;
//        //        GameObject.Find("Directional light").GetComponent<Light>().color = new Color(188, 85, 41, 255);
//        //        GameObject.Find("Directional light").GetComponent<Light>().transform.localPosition = new Vector3(0.34f, 0.008f, -1);
//        //        break;

//        //    case 1:
//        //        GameObject.Find("Directional light").GetComponent<Light>().intensity = 0.04f / 4.0f;
//        //        GameObject.Find("Directional light").GetComponent<Light>().type = LightType.Point;
//        //        GameObject.Find("Directional light").GetComponent<Light>().color = new Color(188, 85, 41, 255);
//        //        GameObject.Find("Directional light").GetComponent<Light>().transform.localPosition = new Vector3(0.34f, 0.008f, -1);
//        //        break;

//        //    case 2:
//        //        GameObject.Find("Directional light").GetComponent<Light>().intensity = 0.06f / 4.0f;
//        //        GameObject.Find("Directional light").GetComponent<Light>().type = LightType.Point;
//        //        GameObject.Find("Directional light").GetComponent<Light>().color = new Color(188, 85, 41, 255);
//        //        GameObject.Find("Directional light").GetComponent<Light>().transform.localPosition = new Vector3(0.328f, 0.008f, -1);
//        //        break;

//        //    case 3:
//        //        GameObject.Find("Directional light").GetComponent<Light>().intensity = 0.1f / 4.0f;
//        //        GameObject.Find("Directional light").GetComponent<Light>().type = LightType.Point;
//        //        GameObject.Find("Directional light").GetComponent<Light>().color = new Color(188, 85, 41, 255);
//        //        GameObject.Find("Directional light").GetComponent<Light>().transform.localPosition = new Vector3(0.291f, 0.008f, -1);
//        //        break;

//        //    case 4:
//        //        GameObject.Find("Directional light").GetComponent<Light>().intensity = 0.15f / 4.0f;
//        //        GameObject.Find("Directional light").GetComponent<Light>().type = LightType.Point;
//        //        GameObject.Find("Directional light").GetComponent<Light>().color = new Color(188, 85, 41, 255);
//        //        GameObject.Find("Directional light").GetComponent<Light>().transform.localPosition = new Vector3(0.291f, 0.008f, -1);
//        //        break;

//        //    case 5:
//        //        GameObject.Find("Directional light").GetComponent<Light>().intensity = 0.15f / 4.0f;
//        //        GameObject.Find("Directional light").GetComponent<Light>().type = LightType.Point;
//        //        GameObject.Find("Directional light").GetComponent<Light>().color = new Color(188, 85, 41, 255);
//        //        GameObject.Find("Directional light").GetComponent<Light>().transform.localPosition = new Vector3(0.284f, 0.008f, -1);
//        //        break;

//        //    case 6:
//        //        GameObject.Find("Directional light").GetComponent<Light>().intensity = 0.08f / 4.0f;
//        //        GameObject.Find("Directional light").GetComponent<Light>().type = LightType.Point;
//        //        GameObject.Find("Directional light").GetComponent<Light>().color = new Color(188, 85, 41, 255);
//        //        GameObject.Find("Directional light").GetComponent<Light>().transform.localPosition = new Vector3(0.27f, 0.008f, -1);
//        //        break;

//        //    case 7:
//        //        GameObject.Find("Directional light").GetComponent<Light>().intensity = 0.04f / 4.0f;
//        //        GameObject.Find("Directional light").GetComponent<Light>().type = LightType.Point;
//        //        GameObject.Find("Directional light").GetComponent<Light>().color = new Color(188, 85, 41, 255);
//        //        GameObject.Find("Directional light").GetComponent<Light>().transform.localPosition = new Vector3(0.27f, 0.008f, -1);
//        //        break;
//        //}
//    }
}
