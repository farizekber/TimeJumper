using Assets.Scripts;

public class Stone : ObstacleBase
{
    public Stone() : base(Obstacles.Riding, 1f, "Stone", new SpawnData(
        false, 1.0f, 10.5f,
        false, 1.0f, 0.04f,
        false, 1.0f, 0.5f),

        new SpawnData(
        false, 1.0f, -8.0f,
        true, 6.5f, 0.0f,
        false, 1.0f, 0.5f))
    { }
}