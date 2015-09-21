using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class Fader : MonoBehaviour
    {
        public static Fader s_Instance;
        private SpriteRenderer m_spriteRenderer;
        public bool m_fEnabled;

        public static void FinalizeObject()
        {
            s_Instance.enabled = false;
            s_Instance = null;
        }

        public void Start()
        {
            s_Instance = this;
            m_spriteRenderer = GameObject.Find("Fader").GetComponent<SpriteRenderer>();
            m_spriteRenderer.color = new Color(0, 0, 0, m_fEnabled ? 1f : 0f);
            Disable();
        }

        public void Update()
        {
            m_spriteRenderer.color = new Color(0, 0, 0, Mathf.Clamp01(m_fEnabled ? m_spriteRenderer.color.a + 0.05f : m_spriteRenderer.color.a - 0.05f));
        }

        public void InvokeMethod(string methodName, float delay)
        {
            Invoke(methodName, delay);
        }
        
        public void Enable()
        {
            m_fEnabled = true;
        }

        public void Disable()
        {
            m_fEnabled = false;
        }
    }
}
