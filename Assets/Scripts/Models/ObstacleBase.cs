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
        private float m_fpSpeedModifier;
        public string m_szPrefab;
        public SpawnData m_horizontalSpawnData;
        public SpawnData m_verticalSpawnData;
        public bool m_fActive = false;

        protected Collider2D m_collider;
        protected Rigidbody2D m_rigid;
        protected MainCharacter m_mainCharacter;

        public ObstacleBase(float fpSpeedModifier, string szPrefab, SpawnData horizontalSpawnData, SpawnData verticalSpawnData)
        {
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
            gobject.GetComponent<ObstacleBase>().Start();
            gobject.GetComponent<Collider2D>().enabled = false;
            gobject.transform.parent = Global.Instance.ForegroundObject.transform;

            return gobject;
        }

        public virtual void Activate()
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

            m_fActive = true;
        }

        // Use this for initialization
        public virtual void Start() {
            m_collider = GetComponent<Collider2D>();
            m_rigid = GetComponent<Rigidbody2D>();
            m_mainCharacter = GameObject.Find("Main Character").GetComponent<MainCharacter>();
        }

        // Update is called once per frame
        public virtual void FixedUpdate()
        {
            if (!m_fActive)
            {
                return;
            }

            if (!GameOverManager.GetInstance().m_fAnimationInProgress)
            {
                if (Global.Instance.orientation == 0)
                    m_rigid.velocity = new Vector2(-m_fpSpeedModifier * Global.Instance.speed, 0);
                else
                    m_rigid.velocity = new Vector2(m_fpSpeedModifier * Global.Instance.speed, 0);
            }
            else
            {
                m_rigid.velocity = new Vector2(0, 0);
            }

            if (transform.localPosition.x < -12 || transform.localPosition.x > 15)
            {
                Disable();
            }
        }

        public void Disable()
        {
            m_collider.enabled = false;
            m_fActive = false;
            m_rigid.velocity = new Vector2(0, 0);
            m_rigid.transform.localPosition = new Vector3(-12, 0, 0);
        }

        public virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (!m_fActive)
            {
                return;
            }
            
            if ((gameObject.name == "MineCarVehicle(Clone)" || other.gameObject.name == "MineCarVehicle(Clone)") && other.gameObject.name != "Main Character")
            {
                if (gameObject.GetComponent<ObstacleBase>() == null || other.gameObject.GetComponent<ObstacleBase>() == null)
                    return;
                
                if (gameObject.GetComponent<ObstacleBase>().m_fpSpeedModifier >= other.gameObject.GetComponent<ObstacleBase>().m_fpSpeedModifier - 0.01 && gameObject.GetComponent<ObstacleBase>().m_fpSpeedModifier <= other.gameObject.GetComponent<ObstacleBase>().m_fpSpeedModifier + 0.01)
                {
                    if (gameObject.name == "MineCarVehicle(Clone)")
                    {
                        gameObject.GetComponent<Rigidbody2D>().transform.localPosition = new Vector3(gameObject.GetComponent<Rigidbody2D>().transform.localPosition.x + 2.0f, gameObject.GetComponent<Rigidbody2D>().transform.localPosition.y, gameObject.GetComponent<Rigidbody2D>().transform.localPosition.z);
                    }
                }
            }
            else if (other.gameObject.name == "Main Character")
            {
                Disable();

                if (name == "MineCarVehicle(Clone)")
                {
                    GameObject.Find("Resource Manager").GetComponent<ResourceManager>().IncreaseHealth();
                }
                else
                {
                    GameObject.Find("Resource Manager").GetComponent<ResourceManager>().LowerHealth();
                }
            }
        }
    }
}