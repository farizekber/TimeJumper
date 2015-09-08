using UnityEngine;

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
        public SpawnData m_spawnData;

        public ObstacleBase(Obstacles obstacleKind, float fpSpeedModifier, string szPrefab, SpawnData spawnData)
        {
            m_obstacleKind = obstacleKind;
            m_fpSpeedModifier = fpSpeedModifier;
            m_szPrefab = szPrefab;
            m_spawnData = spawnData;
        }

        public void Spawn()
        {
            while (GameOverAnimation.GetInstance().m_fAnimationInProgress)
            {
                Invoke("Spawn", 0.1f);
                return;
            }

            GameObject gobject = (GameObject)Instantiate(Resources.Load("Prefabs/" + m_szPrefab),
                new Vector3(
                ((m_spawnData.m_fRandomX ? Random.value : 0) * m_spawnData.m_fpXModifier) + m_spawnData.m_fpXOffset,
                ((m_spawnData.m_fRandomY ? Random.value : 0) * m_spawnData.m_fpYModifier) + m_spawnData.m_fpYOffset,
                ((m_spawnData.m_fRandomZ ? Random.value : 0) * m_spawnData.m_fpZModifier) + m_spawnData.m_fpZOffset),
                new Quaternion(0, 0, 0, 0));

            gobject.transform.localPosition += Global.Instance.GlobalObject.transform.localPosition + Global.Instance.ForegroundObject.transform.localPosition;
            gobject.transform.parent = Global.Instance.ForegroundObject.transform;
        }

        // Use this for initialization
        void Start() { }

        // Update is called once per frame
        void Update()
        {
            if (!GameOverAnimation.GetInstance().m_fAnimationInProgress)
                GetComponent<Rigidbody2D>().velocity = new Vector2(-m_fpSpeedModifier * Global.Instance.speed, 0);
            else
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

            if (transform.localPosition.x < -10)
            {
                Destroy(this.gameObject);
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name == "Main Character")
            {
                GameOverAnimation.GetInstance().Trigger();
            }
        }

        void OnTriggerStay2D(Collider2D other) { }

        void OnTriggerExit2D(Collider2D other) { }
    }
}