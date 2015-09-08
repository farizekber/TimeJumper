using UnityEngine;

namespace Assets.Scripts
{
    public abstract class ObstacleBase : MonoBehaviour
    {
        private Obstacles m_obstacleKind;
        private float m_fpSpeedModifier;

        public ObstacleBase(Obstacles obstacleKind, float fpSpeedModifier)
        {
            m_obstacleKind = obstacleKind;
            m_fpSpeedModifier = fpSpeedModifier;
        }

        // Use this for initialization
        void Start() { }

        // Update is called once per frame
        void Update()
        {
            if (!GameOverAnimation.GetInstance().m_fAnimationInProgress)
                GetComponent<Rigidbody2D>().velocity = new Vector2(-m_fpSpeedModifier * Global.speed, 0);
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
