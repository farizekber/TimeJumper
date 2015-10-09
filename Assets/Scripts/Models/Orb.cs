using Assets.Scripts;
using UnityEngine;

public class Orb : ObstacleBase
{
    public bool isTeleport = false;
    private Animator animator;
    private float lastGUIUpdate = 0;

    public Orb() : base(1.0f, "Orb", new SpawnData(
        false, 1.0f, 10.5f,
        true, 5.21f, 1.29f,
        false, 1.0f, 0.5f),

        new SpawnData(
        false, 1.0f, -8.0f,
        true, 6.5f, 0.0f,
        false, 1.0f, 0.5f))
    { }

    public override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        float currentTime = Time.time;

        if (!(currentTime > lastGUIUpdate + 0.2f))
        {
            return;
        }

        lastGUIUpdate = currentTime;

        animator.speed = (Global.Instance.speed < 0 ? 0 : Global.Instance.speed);

        if (GameObject.Find("Resource Manager").GetComponent<ResourceManager>().activeEnergy >= 3)
        {
            isTeleport = true;
            transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
            GetComponent<CircleCollider2D>().radius = 2f;
            animator.SetBool("IsTeleport", true);
        }
        else
        {
            isTeleport = false;
            transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            GetComponent<CircleCollider2D>().radius = 0.64f;
            animator.SetBool("IsTeleport", false);
        }
    }

    public override void Activate()
    {
        base.Activate();

        if (GameObject.Find("Resource Manager").GetComponent<ResourceManager>().activeEnergy >= 3)
        {
            isTeleport = true;
            transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
            GetComponent<CircleCollider2D>().radius = 2f;
            animator.SetBool("IsTeleport", true);
        }
        else
        {
            isTeleport = false;
            transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            GetComponent<CircleCollider2D>().radius = 0.64f;
            animator.SetBool("IsTeleport", false);
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (!m_fActive)
            return;

        if (other.gameObject.name == "Main Character")
        {
            Disable();

            if (!isTeleport)
            {
                GameObject.Find("Resource Manager").GetComponent<ResourceManager>().IncreaseEnergy();
            }
            else
            {
                PerspectiveInitializer perspectiveInitializer = PerspectiveInitializer.s_Instance;
                perspectiveInitializer.themeState = perspectiveInitializer.themeState == PerspectiveInitializer.ThemeState.Mine ? PerspectiveInitializer.ThemeState.Ice : PerspectiveInitializer.ThemeState.Mine;

                GameObject.Find("Main Camera").GetComponent<Twirler>().Trigger();
                PerspectiveInitializer.s_Instance.CleanPerspective();

                if (Global.Instance.orientation == 0)
                {
                    PerspectiveInitializer.s_Instance.InvokeMethod("LoadHorizontalPerspective", 2f);
                }
                else
                {
                    PerspectiveInitializer.s_Instance.InvokeMethod("LoadVerticalPerspective", 2f);
                }

                GameObject.Find("Resource Manager").GetComponent<ResourceManager>().ProcessTeleportUsage();
                Global.Instance.IncreaseDistance(500);
            }
        }
    }
}