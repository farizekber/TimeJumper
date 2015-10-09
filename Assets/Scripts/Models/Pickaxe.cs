using Assets.Scripts;
using UnityEngine;

public class Pickaxe : ObstacleBase
{
    private Animator animator;

    public Pickaxe() : base(1.5f, "Pickaxe", new SpawnData(
        false, 1.0f, 10.5f,
        true, 3.07f, 4.0f,
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
    }
}