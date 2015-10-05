using Assets.Scripts;
using UnityEngine;

public class Orb : ObstacleBase
{
    public bool isTeleport = false;
    private Animator animator;

    public Orb() : base(Obstacles.Stationary, 1.0f, "Orb", new SpawnData(
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

        animator.speed = (Global.Instance.speed < 0 ? 0 : Global.Instance.speed);

        if (GameObject.Find("Resource Manager").GetComponent<ResourceManager>().activeEnergy >= 3)
        {
            isTeleport = true;
            animator.SetBool("IsTeleport", true);
        }
        else
        {
            isTeleport = false;
            animator.SetBool("IsTeleport", false);
        }
    }

    public override void Activate()
    {
        base.Activate();
        if (GameObject.Find("Resource Manager").GetComponent<ResourceManager>().activeEnergy >= 3)
        {
            isTeleport = true;
            animator.SetBool("IsTeleport", true);
        }
        else
        {
            isTeleport = false;
            animator.SetBool("IsTeleport", false);
        }
    }

    //TODO: Override collision
}