using Assets.Scripts;

public class Minecart : ObstacleBase
{
    public Minecart() : base(Obstacles.Riding, 1.25f, "Minecart", new SpawnData(
        false, 1.0f, 10.5f,
        false, 1.0f, 0.71f,
        false, 1.0f, 0.5f),

        new SpawnData(
        false, 1.0f, -8.0f,
        true, 6.5f, 0.0f,
        false, 1.0f, 0.5f))
    { }
}