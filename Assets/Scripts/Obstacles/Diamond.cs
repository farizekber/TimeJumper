using Assets.Scripts;

public class Diamond : ObstacleBase
{
    public Diamond() : base(Obstacles.Stationary, 1.0f, "Diamond", new SpawnData(
        false, 1.0f, 10.5f,
        true, 6.5f, 0.0f,
        false, 1.0f, 0.5f))
    { }
}