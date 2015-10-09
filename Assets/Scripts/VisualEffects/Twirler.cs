using UnityEngine;
using System.Collections;

public class Twirler : MonoBehaviour
{
    public readonly float m_fpTwirlTimeInSeconds = 5;
    private Component m_twirl = null;
    public bool m_fInProgress = false;

    // Use this for initialization
    void Start () {
        initializeTwirl();
    }

    public void Trigger()
    {
        m_fInProgress = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_fInProgress)
            return;

        (m_twirl as UnityStandardAssets.ImageEffects.Twirl).angle += 3;

        if (((m_twirl as UnityStandardAssets.ImageEffects.Twirl).angle % 360) == 0)
        {
            m_fInProgress = false;
        }
    }

    private void initializeTwirl()
    {
        foreach (Component component in GameObject.Find("Main Camera").GetComponents<Component>())
        {
            if (component is UnityStandardAssets.ImageEffects.Twirl)
            {
                m_twirl = component;
            }
        }
    }
}
