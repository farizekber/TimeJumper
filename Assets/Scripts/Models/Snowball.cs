using Assets.Scripts;
using UnityEngine;

public class Snowball : ObstacleBase
{
    public Snowball() : base(1.5f, "Snowball", new SpawnData(
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
    }
}