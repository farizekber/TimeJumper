using Assets.Scripts;

public class TNT : ObstacleBase
{
    public TNT() : base(Obstacles.Riding, 1f, "TNT", new SpawnData(
        false, 1.0f, 10.5f,
        false, 1.0f, 0.757f,
        false, 1.0f, 0.5f))
    { }
}