using Assets.Scripts;

public class Orb : ObstacleBase
{
    public Orb() : base(Obstacles.Stationary, 1.0f, "Orb", new SpawnData(
        false, 1.0f, 10.5f,
        true, 5.21f, 1.29f,
        false, 1.0f, 0.5f),

        new SpawnData(
        false, 1.0f, -8.0f,
        true, 6.5f, 0.0f,
        false, 1.0f, 0.5f))
    { }
}