using System;
using Assets.Scripts;
using UnityEngine;

public class MineCarVehicle : ObstacleBase
{
    public Sprite mine;
    public Sprite ice;

    public MineCarVehicle() : base(Obstacles.Riding, 1f, "MineCarVehicle", new SpawnData(
        false, 1.0f, 10.5f,
        false, 1.0f, 0.602f,
        false, 1.0f, 0.5f),
        
        new SpawnData(
        false, 1.0f, 10.5f,
        false, 1.0f, 0.581f,
        false, 1.0f, 0.5f))
    {
        active = true;
    }
    
    public void AdjustToTheme(PerspectiveInitializer.ThemeState themeState)
    {
        if (themeState == PerspectiveInitializer.ThemeState.Mine)
        {
            GetComponent<SpriteRenderer>().sprite = mine;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = ice;
        }
    }
}