using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class ResourceManager : MonoBehaviour
    {
        GameObject vehicleHealth;
        GameObject[] energy = new GameObject[3];

        public bool inVehicle = false;
        public int activeEnergy;
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
            if (inVehicle)
            {
                Global.Instance.HealthBar.GetComponent<Image>().enabled = true;
                Global.Instance.HealthBarBackground.GetComponent<Image>().enabled = true;
                GameObject.Find("Main Character").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/InMineCart");
                GameObject.Find("Main Character").GetComponent<Animator>().enabled = false;
            }
            else
            {
                if (Global.Instance != null)
                {
                    Global.Instance.HealthBar.GetComponent<Image>().enabled = false;
                    Global.Instance.HealthBarBackground.GetComponent<Image>().enabled = false;
                    GameObject.Find("Main Character").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/character-v2");
                    GameObject.Find("Main Character").GetComponent<Animator>().enabled = true;
                }
            }

            vehicleHealth.GetComponent<Image>().fillAmount = activeVehicleHealth;
            energy[0].GetComponent<Image>().fillAmount = activeEnergy >= 1 ? 1 : 0;
            energy[1].GetComponent<Image>().fillAmount = activeEnergy >= 2 ? 1 : 0;
            energy[2].GetComponent<Image>().fillAmount = activeEnergy >= 3 ? 1 : 0;
        }

        public void IncreaseEnergy()
        {
            Mathf.Clamp(activeEnergy++, 0, 3);
            GameObject.Find("Divine-Shield").GetComponent<Renderer>().enabled = true;
        }

        public void IncreaseHealth()
        {
            if (!inVehicle)
            {
                resetActiveHealth();    
                inVehicle = true;
                Global.Instance.speed += 2;
            }
            else
            {
                Mathf.Clamp(activeVehicleHealth += vehicleHealthLossPerHit, 0, 1);
            }
        }

        public void RemoveVehicle()
        {
            if (inVehicle)
            {
                inVehicle = false;
                Global.Instance.speed -= 2;
            }
        }

        void resetActiveHealth()
        {
            activeVehicleHealth = 1.0f;
        }

        //Called when noticing collision
        public void LowerHealth()
        {
            if (activeVehicleHealth > 0.01 && inVehicle)
            {
                Mathf.Clamp(activeVehicleHealth -= vehicleHealthLossPerHit, 0, 1);

                if (activeVehicleHealth < 0.01)
                {
                    RemoveVehicle();
                }
            }
            else if (activeEnergy > 0)
            {
                activeEnergy = 0;
                GameObject.Find("Divine-Shield").GetComponent<Renderer>().enabled = false;
            }
            else
            {
                GameOverAnimation.GetInstance().Trigger();
            }
        }
    }
}
