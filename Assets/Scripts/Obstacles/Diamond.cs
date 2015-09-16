using Assets.Scripts;

public class Diamond : ObstacleBase
{
    public Diamond() : base(Obstacles.Stationary, 1.0f, "Diamond", new SpawnData(
        false, 1.0f, 10.5f,
        true, 5.21f, 1.29f,
        false, 1.0f, 0.5f))
    { }
}