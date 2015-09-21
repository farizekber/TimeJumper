using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public abstract class ObstacleBase : MonoBehaviour
    {
        public struct SpawnData
        {
            public bool m_fRandomX, m_fRandomY, m_fRandomZ;
            public float m_fpXModifier, m_fpYModifier, m_fpZModifier, m_fpXOffset, m_fpYOffset, m_fpZOffset;

            public SpawnData(bool fRandomX, float fpXModifier, float fpXOffset, bool fRandomY, float fpYModifier, float fpYOffset, bool fRandomZ, float fpZModifier, float fpZOffset)
            {
                m_fRandomX = fRandomX;
                m_fpXModifier = fpXModifier;
                m_fpXOffset = fpXOffset;

                m_fRandomY = fRandomY;
                m_fpYModifier = fpYModifier;
                m_fpYOffset = fpYOffset;

                m_fRandomZ = fRandomZ;
                m_fpZModifier = fpZModifier;
                m_fpZOffset = fpZOffset;
            }
        }

        private Obstacles m_obstacleKind;
        private float m_fpSpeedModifier;
        public string m_szPrefab;
        public SpawnData m_horizontalSpawnData;
        public SpawnData m_verticalSpawnData;
        public bool active = false;

        private Collider2D m_collider;
        private Rigidbody2D rigid;
        private MainCharacter mainCharacter;

        public ObstacleBase(Obstacles obstacleKind, float fpSpeedModifier, string szPrefab, SpawnData horizontalSpawnData, SpawnData verticalSpawnData)
        {
            m_obstacleKind = obstacleKind;
            m_fpSpeedModifier = fpSpeedModifier;
            m_szPrefab = szPrefab;
            m_horizontalSpawnData = horizontalSpawnData;
            m_verticalSpawnData = verticalSpawnData;
        }

        public GameObject Spawn()
        {
            GameObject gobject;

            if (Global.Instance.orientation == 0)
            {
                gobject = (GameObject)Instantiate(Resources.Load("Prefabs/" + m_szPrefab),
                    new Vector3(
                    -8.0f,
                    ((m_horizontalSpawnData.m_fRandomY ? Random.value : 0) * m_horizontalSpawnData.m_fpYModifier) + m_horizontalSpawnData.m_fpYOffset,
                    ((m_horizontalSpawnData.m_fRandomZ ? Random.value : 0) * m_horizontalSpawnData.m_fpZModifier) + m_horizontalSpawnData.m_fpZOffset),
                    Quaternion.Euler(0, 0, 0));
            }
            else
            {
                gobject = (GameObject)Instantiate(Resources.Load("Prefabs/" + m_szPrefab),
                    new Vector3(
                    -8.0f,
                    ((m_verticalSpawnData.m_fRandomY ? Random.value : 0) * m_verticalSpawnData.m_fpYModifier) + m_verticalSpawnData.m_fpYOffset,
                    ((m_verticalSpawnData.m_fRandomZ ? Random.value : 0) * m_verticalSpawnData.m_fpZModifier) + m_verticalSpawnData.m_fpZOffset),
                    Quaternion.Euler(0, 0, -90));
            }

            //gobject.transform.localPosition += Global.Instance.GlobalObject.transform.localPosition + Global.Instance.ForegroundObject.transform.localPosition;
            gobject.GetComponent<Collider2D>().enabled = false;
            gobject.transform.parent = Global.Instance.ForegroundObject.transform;

            return gobject;
        }

        public void Activate()
        {
            m_collider.enabled = true;
            transform.parent = null;

            if (Global.Instance.orientation == 0)
            {
                transform.localPosition = new Vector3(
                    ((m_horizontalSpawnData.m_fRandomX ? Random.value : 0) * m_horizontalSpawnData.m_fpXModifier) + m_horizontalSpawnData.m_fpXOffset,
                    ((m_horizontalSpawnData.m_fRandomY ? Random.value : 0) * m_horizontalSpawnData.m_fpYModifier) + m_horizontalSpawnData.m_fpYOffset,
                    ((m_horizontalSpawnData.m_fRandomZ ? Random.value : 0) * m_horizontalSpawnData.m_fpZModifier) + m_horizontalSpawnData.m_fpZOffset);
            }
            else
            {
                transform.localPosition = new Vector3(
                    ((m_verticalSpawnData.m_fRandomX ? Random.value : 0) * m_verticalSpawnData.m_fpXModifier) + m_verticalSpawnData.m_fpXOffset,
                    ((m_verticalSpawnData.m_fRandomY ? Random.value : 0) * m_verticalSpawnData.m_fpYModifier) + m_verticalSpawnData.m_fpYOffset,
                    ((m_verticalSpawnData.m_fRandomZ ? Random.value : 0) * m_verticalSpawnData.m_fpZModifier) + m_verticalSpawnData.m_fpZOffset);
            }

            transform.localPosition += Global.Instance.GlobalObject.transform.localPosition + Global.Instance.ForegroundObject.transform.localPosition;
            transform.parent = Global.Instance.ForegroundObject.transform;

            active = true;
        }

        // Use this for initialization
        public void Start() {
            m_collider = GetComponent<Collider2D>();
            rigid = GetComponent<Rigidbody2D>();
            mainCharacter = GameObject.Find("Main Character").GetComponent<MainCharacter>();
        }

        // Update is called once per frame
        public void FixedUpdate()
        {
            if (!active)
            {
                return;
            }

            if (!GameOverAnimation.GetInstance().m_fAnimationInProgress)
            {
                if (Global.Instance.orientation == 0)
                    rigid.velocity = new Vector2(-m_fpSpeedModifier * Global.Instance.speed, 0);
                else
                    rigid.velocity = new Vector2(m_fpSpeedModifier * Global.Instance.speed, 0);
            }
            else
                rigid.velocity = new Vector2(0, 0);

            if ((Global.Instance.orientation == 0 && transform.localPosition.x < -8) || (Global.Instance.orientation == 1 && transform.localPosition.x > 15))
            {
                Disable();
                //Destroy(this.gameObject);
            }
        }

        public void Disable()
        {
            m_collider.enabled = false;
            active = false;
            rigid.velocity = new Vector2(0, 0);
            rigid.transform.localPosition = new Vector3(-8, 0, 0);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (!active)
            {
                return;
            }

            ObstacleBase otherObstacleBase = other.gameObject.GetComponent<ObstacleBase>();

            if (otherObstacleBase != null)
            {

                if (other.name == "Diamond(Clone)")
                {
                    if (otherObstacleBase.m_fpSpeedModifier == this.m_fpSpeedModifier)
                    {
                        otherObstacleBase.Disable();
                    }
                }
            }


            if (other.gameObject.name == "Main Character")
            {
                if (name == "Diamond(Clone)")
                {
                    //Destroy(gameObject);
                    Disable();
                    Global.Instance.PlayPickupSound();
                    Global.score += 1;
                    Global.Instance.UpdateScore();
                }
                else if (name == "MineCarVehicle(Clone)")
                {
                    if (!mainCharacter.inVehicle)
                    {
                        Global.Instance.speed += 2f;
                    }

                    Global.Instance.HealthBar.GetComponent<Image>().enabled = true;
                    Global.Instance.HealthBar.GetComponent<Image>().fillAmount = 1;
                    Global.Instance.HealthBarBackground.GetComponent<Image>().enabled = true;
                    Global.Instance.HealthBarBackground.GetComponent<Image>().fillAmount = 1;
                    mainCharacter.inVehicle = true;
                    other.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/InMineCart");
                    other.gameObject.GetComponent<Animator>().enabled = false;
                    mainCharacter.vehicleHealth = 100;

                    Disable();
                }
                else
                {
                    if (!mainCharacter.inVehicle)
                    {
                        GameOverAnimation.GetInstance().Trigger();
                    }
                    else
                    {
                        mainCharacter.vehicleHealth -= 20;
                        Global.Instance.HealthBar.GetComponent<Image>().fillAmount -= 0.2f;
                        Global.Instance.HealthBarBackground.GetComponent<Image>().fillAmount = 1;
                        Disable();

                        if (mainCharacter.vehicleHealth <= 0)
                        {
                            Disable();
                            mainCharacter.inVehicle = false;
                            Global.Instance.HealthBarBackground.GetComponent<Image>().enabled = false;
                            Global.Instance.HealthBar.GetComponent<Image>().enabled = false;
                            other.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/character-v2");
                            other.gameObject.GetComponent<Animator>().enabled = true;
                            Global.Instance.speed -= 2f;
                        }
                    }
                }
            }
        }
    }
}