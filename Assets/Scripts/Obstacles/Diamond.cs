using Assets.Scripts;

public class Diamond : ObstacleBase
{
    public Diamond() : base(Obstacles.Stationary, 1.0f, "Diamond", new SpawnData(
        false, 1.0f, 16.5f,
        true, 8.5f, -1.0f,
        false, 1.0f, 0.5f))
    { }
}