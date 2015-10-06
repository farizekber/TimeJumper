using UnityEngine;
using System.Collections;
using System;

public class Dragon : MonoBehaviour
{
    public float x;// = animationEvent.intParameter / 1000.0f;
    public float intensity;// = animationEvent.floatParameter;
    public bool pointLightingOn = false;
    private Light m_light;
    private SpawnManager spawnManager;
    private Color pointLightColor = new Color(188, 85, 41, 255);
    private float lastGUIUpdate = 0;

    private RuntimeAnimatorController dragon;
    private RuntimeAnimatorController dragonIce;
    private RuntimeAnimatorController dragon2;
    private RuntimeAnimatorController dragon2Ice;

    // Use this for initialization
    void Start() {
        m_light = GameObject.Find("Directional light").GetComponent<Light>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        dragon = Resources.Load<RuntimeAnimatorController>("Animations/Dragon");
        dragonIce = Resources.Load<RuntimeAnimatorController>("Animations/DragonIce");
        dragon2 = Resources.Load<RuntimeAnimatorController>("Animations/Dragon2");
        dragon2Ice = Resources.Load<RuntimeAnimatorController>("Animations/Dragon2Ice");
    }

    // Update is called once per frame
    void Update()
    {
        if (Global.Instance == null)
            return;

        float currentTime = Time.time;

        if (!(currentTime > lastGUIUpdate + 0.2f))
        {
            return;
        }

        lastGUIUpdate = currentTime;

        if (Global.Instance.orientation == 0)
        {
            GetComponent<Animator>().SetBool("FireballIncomming", spawnManager.IsAnyFireballActive());
        }

        if (pointLightingOn)
        {
            m_light.intensity = intensity * 0.1f;
            m_light.type = LightType.Point;
            m_light.color = pointLightColor;
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

    public void LoadHorizontal(Assets.Scripts.PerspectiveInitializer.ThemeState themeState)
    {
        transform.localPosition = new Vector3(-5.4f, 2.76f, 0.5f);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        transform.localScale = new Vector3(0.75f, 0.75f, 1);

        if (themeState == Assets.Scripts.PerspectiveInitializer.ThemeState.Mine)
        {
            GetComponent<Animator>().runtimeAnimatorController = dragon;
            pointLightColor = new Color(188, 85, 41, 255);
        }
        else
        {
            GetComponent<Animator>().runtimeAnimatorController = dragonIce;
            pointLightColor = new Color(41, 85, 188, 255);
        }
    }

    public void LoadVertical(Assets.Scripts.PerspectiveInitializer.ThemeState themeState)
    {
        transform.localPosition = new Vector3(5.50f, 3.12f, 0.5f);
        transform.localRotation = Quaternion.Euler(0, 0, 270);
        transform.localScale = new Vector3(1, 1, 1);

        if (themeState == Assets.Scripts.PerspectiveInitializer.ThemeState.Mine)
        {
            GetComponent<Animator>().runtimeAnimatorController = dragon2;
            pointLightColor = new Color(188, 85, 41, 255);
        }
        else
        {
            GetComponent<Animator>().runtimeAnimatorController = dragon2Ice;
            pointLightColor = new Color(41, 85, 188, 255);
        }
    }
}
