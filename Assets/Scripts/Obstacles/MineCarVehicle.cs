using Assets.Scripts;

public class MineCarVehicle : ObstacleBase
{
    public MineCarVehicle() : base(Obstacles.Riding, 1f, "MineCarVehicle", new SpawnData(
        false, 1.0f, 10.5f,
        false, 1.0f, 0.581f,
        false, 1.0f, 0.5f),
        
        new SpawnData(
        false, 1.0f, 10.5f,
        false, 1.0f, 0.581f,
        false, 1.0f, 0.5f))
    {
        active = true;
    }
}