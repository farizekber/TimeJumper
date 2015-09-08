using Assets.Scripts;

public class Bat : ObstacleBase
{
    public Bat() : base(Obstacles.Flying, 1.5f, "Bat", new SpawnData(
        false, 1.0f, 16.5f,
        true, 8.5f, -1.0f,
        false, 1.0f, 0.5f))
    { }
}