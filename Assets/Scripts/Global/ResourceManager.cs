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
        public float activeVehicleHealth = 0.0f;
        public float vehicleHealthLossPerHit = 0.33f;

        void Start()
        {
            vehicleHealth = GameObject.Find("HealthBar");
            energy[0] = GameObject.Find("EnergyBar1");
            energy[1] = GameObject.Find("EnergyBar2");
            energy[2] = GameObject.Find("EnergyBar3");
        }

        void OnGUI()
        {
            vehicleHealth.GetComponent<Image>().fillAmount = activeVehicleHealth;
            energy[0].GetComponent<Image>().fillAmount = activeEnergy > 0 ? 1 : 0;
            energy[0].GetComponent<Image>().fillAmount = activeEnergy > 1 ? 1 : 0;
            energy[0].GetComponent<Image>().fillAmount = activeEnergy > 2 ? 1 : 0;
        }

        public void IncreaseEnergy()
        {
            //TODO
        }

        public void IncreaseHealth()
        {
            Mathf.Clamp(activeVehicleHealth += vehicleHealthLossPerHit, 0, 1);
        }

        public void ResetActiveHealth()
        {
            activeVehicleHealth = 1.0f;
        }

        //Called when noticing collision
        public void LowerHealth()
        {
            if (activeVehicleHealth > 0)
            {
                Mathf.Clamp(activeVehicleHealth -= vehicleHealthLossPerHit, 0, 1);
            }
            else if (activeEnergy > 0)
            {
                activeEnergy = 0;
            }
            else
            {
                GameOverAnimation.GetInstance().Trigger();
            }
        }
    }
}
