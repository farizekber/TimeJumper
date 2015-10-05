using Assets.Scripts;
using UnityEngine;

public class Iceball : ObstacleBase
{
    private Animator animator;
    private Animator dragonAnimator;
    private SpriteRenderer spriteRenderer;
    bool iceball = false;

    public Iceball() : base(Obstacles.Flying, -1.5f, "Iceball", new SpawnData(
        false, 1.0f, -5.5f,
        true, 2.0f, 3.5f,
        false, 1.0f, 0.5f),

        new SpawnData(
        false, 1.0f, -8.0f,
        true, 6.5f, 0.0f,
        false, 1.0f, 0.5f))
    { }

    public override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        dragonAnimator = GameObject.Find("Dragon").GetComponent<Animator>();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        animator.speed = (Global.Instance.speed < 0 || !dragonAnimator.GetCurrentAnimatorStateInfo(0).IsName("Firing") ? 0 : Global.Instance.speed);
        //Color c = spriteRenderer.material.color;
        //spriteRenderer.material.color = new Color(c.r, c.g, c.b,  ? 1.0f : 0.0f)

        if (!iceball)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("IceballIdle") || animator.GetCurrentAnimatorStateInfo(0).IsName("Iceball"))
            {
                rigid.transform.localPosition = new Vector3(rigid.transform.localPosition.x - 1.0f, rigid.transform.localPosition.y, rigid.transform.localPosition.z);
                iceball = true;
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Warning"))
            rigid.velocity = new Vector2(0, 0);
    }

    public override void Activate()
    {
        base.Activate();
        iceball = false;
        animator.Play("Warning", 0, 0);
    }
}