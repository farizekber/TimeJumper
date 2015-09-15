using Assets.Scripts;

public class Bat : ObstacleBase
{
    public Bat() : base(Obstacles.Flying, 1.5f, "Bat", new SpawnData(
        false, 1.0f, 10.5f,
        true, 6.5f, 0.0f,
        false, 1.0f, 0.5f))
    { }
}