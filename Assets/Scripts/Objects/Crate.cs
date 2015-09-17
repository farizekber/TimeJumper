using Assets.Scripts;

public class Crate : ObstacleBase
{
    public Crate() : base(Obstacles.Riding, 1f, "Crate", new SpawnData(
        false, 1.0f, 10.5f,
        false, 1.0f, -0.269f,
        false, 1.0f, 0.5f),

        new SpawnData(
        false, 1.0f, -8.0f,
        true, 6.5f, 0.0f,
        false, 1.0f, 0.5f))
    { }
}
