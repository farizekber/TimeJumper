using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class ResourceManager
    {
        GameObject vehicleHealth;
        GameObject[] energy = new GameObject[3];

        public int activeEnergy = 1;
        public float activeHealth = 0.0f;
        public float vehicleHealthLossPerHit = 0.33f;

        void Start()
        {
            vehicleHealth = GameObject.Find("HealthBar");
            energy[0] = GameObject.Find("Energy1");
            energy[1] = GameObject.Find("Energy2");
            energy[2] = GameObject.Find("Energy3");
        }

        void onGui()
        {
            vehicleHealth.GetComponent<Image>().fillAmount = activeHealth;
            //energy

        }

        void resetActiveHealth()
        {
            activeHealth = 1.0f;
        }

        //Called when noticing collision
        void lowerHealth()
        {
            if (activeHealth > 0)//vehicleHealth.GetComponent<Image>().fillAmount > 0 - float.Epsilon)
            {
                Mathf.Clamp(activeHealth -= vehicleHealthLossPerHit, 0, 1);
                //vehicleHealth.GetComponent<Image>().fillAmount -= vehicleHealthLossPerHit;
            }
            else if (activeEnergy > 0)
            {
                activeEnergy = 0;
            }
            else
            {
                //Trigger Death
            }
        }
    }
}
