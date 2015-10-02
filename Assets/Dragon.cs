using UnityEngine;
using System.Collections;

public class Dragon : MonoBehaviour
{
    public float x;// = animationEvent.intParameter / 1000.0f;
    public float intensity;// = animationEvent.floatParameter;
    public bool pointLightingOn = false;
    private Light m_light;
    private SpawnManager spawnManager;

    // Use this for initialization
    void Start () {
        m_light = GameObject.Find("Directional light").GetComponent<Light>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Global.Instance.orientation == 0)
        {
            GetComponent<Animator>().SetBool("FireballIncomming", spawnManager.IsAnyFireballActive());
        }

        if (pointLightingOn)
        {
            m_light.intensity = intensity * 0.1f;
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
}
