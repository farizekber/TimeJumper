using Assets.Scripts;

public class Stone : ObstacleBase
{
    public Stone() : base(Obstacles.Riding, 1f, "Stone", new SpawnData(
        false, 1.0f, 10.5f,
        false, 1.0f, 1.45f,
        false, 1.0f, 0.5f))
    { }
}