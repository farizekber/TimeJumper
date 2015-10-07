using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Objects;
using Assets.Scripts.Parallaxing;

public class SpawnManager : MonoBehaviour {

    public static SpawnManager Instance;
    public List<string> spawnables = new List<string>();
    public List<string> collectables = new List<string>();
    public Dictionary<string, List<GameObject>> spawnableInstances = new Dictionary<string, List<GameObject>>();
    public Dictionary<string, List<GameObject>> collectableInstances = new Dictionary<string, List<GameObject>>();

    public PlatformManager platformManager = new PlatformManager();

    public float obstacleSpawnRate = 5f;
    public float collectableSpawnRate = 10f;
    public int defaultSpawnableCount = 5;
    public int defaultCollectableCount = 1;

    public Sprite mineBegin;
    public Sprite iceBegin;
    public Sprite mineMiddle;
    public Sprite iceMiddle;
    public Sprite mineEnd;
    public Sprite iceEnd;

    void Awake() {
        Instance = this;
    }

    void Start()
    {
        Init();
        platformManager.Start();
        platformManager.Spawn();
        LoadHorizontal(PerspectiveInitializer.ThemeState.Mine);
    }

    public void SpawnPlatform()
    {
        if (Global.Instance.orientation == 0)
        {
            if ((!platformManager.previousHigh) && (Random.value > 0.66f))
            {
                platformManager.previousHigh = true;
                platformManager.Activate(((int)(Random.value * 6)) + 2, false);
            }
            else
            {
                platformManager.previousHigh = false;
                platformManager.Activate(((int)(Random.value * 6)) + 2, true);
            }
        }

        Invoke("SpawnPlatform", 2.5f);
    }

    public bool IsAnyFireballActive()
    {
        bool temp = false;
        int index = 0;

        for (int i = 0; i < spawnables.Count; ++i)
        {
            string o = spawnables[i];
            if (o == "Iceball" || o == "Fireball(Clone)" || o == "Fireball" || o == "Iceball(Clone)")
            {
                index = i;
                temp = true;
                break;
            }
        }

        if (!temp)
            return false;

        temp = false;
        foreach (GameObject gobject in spawnableInstances[spawnables[index]])
        {
            if ((gobject.name == "Iceball(Clone)" || gobject.name == "Fireball(Clone)") && gobject.GetComponent<ObstacleBase>().active)
            {
                temp = true;
                break;
            }
        }

        return temp;
    }

    private void Init()
    {
        List<ObstacleBase> spawnables = new List<ObstacleBase>();
        spawnables.Add((Resources.Load("Prefabs/" + "Crate") as GameObject).GetComponent<Crate>());
        spawnables.Add((Resources.Load("Prefabs/" + "Fireball") as GameObject).GetComponent<Fireball>());
        spawnables.Add((Resources.Load("Prefabs/" + "Pickaxe") as GameObject).GetComponent<Pickaxe>());
        spawnables.Add((Resources.Load("Prefabs/" + "TNT") as GameObject).GetComponent<TNT>());
        spawnables.Add((Resources.Load("Prefabs/" + "BarrelDwarf") as GameObject).GetComponent<BarrelDwarf>());
        spawnables.Add((Resources.Load("Prefabs/" + "IceCrate") as GameObject).GetComponent<IceCrate>());
        spawnables.Add((Resources.Load("Prefabs/" + "Iceball") as GameObject).GetComponent<Iceball>());
        spawnables.Add((Resources.Load("Prefabs/" + "Snowball") as GameObject).GetComponent<Snowball>());
        spawnables.Add((Resources.Load("Prefabs/" + "Snowman") as GameObject).GetComponent<Snowman>());
        spawnables.Add((Resources.Load("Prefabs/" + "BarrelEskimo") as GameObject).GetComponent<BarrelEskimo>());

        List<ObstacleBase> collectables = new List<ObstacleBase>();
        collectables.Add((Resources.Load("Prefabs/" + "MineCarVehicle") as GameObject).GetComponent<MineCarVehicle>());
        collectables.Add((Resources.Load("Prefabs/" + "Orb") as GameObject).GetComponent<Orb>());

        foreach (var item in spawnables)
        {
            spawnableInstances.Add(item.name + "(Clone)", new List<GameObject>());

            for (int i = 0; i < defaultSpawnableCount; i++)
            {
                spawnableInstances[item.name + "(Clone)"].Add(item.Spawn());
            }
        }

        foreach (var item in collectables)
        {
            collectableInstances.Add(item.name + "(Clone)", new List<GameObject>());

            for (int i = 0; i < defaultCollectableCount; i++)
            {
                collectableInstances[item.name + "(Clone)"].Add(item.Spawn());
            }
        }
    }

    public void LoadVertical(PerspectiveInitializer.ThemeState themeState)
    {
        platformManager.DisableAll();

        spawnables.Clear();
        collectables.Clear();

        if (themeState == PerspectiveInitializer.ThemeState.Mine)
        {
            spawnables.Add("Crate(Clone)");
            spawnables.Add("TNT(Clone)");
            foreach (GameObject go in spawnableInstances["Crate(Clone)"])
            {
                go.GetComponent<Animator>().SetBool("IsRotating", true);
            }
            foreach (GameObject go in spawnableInstances["TNT(Clone)"])
            {
                go.GetComponent<Animator>().SetBool("IsRotating", true);
            }
        }
        else
        {
            spawnables.Add("IceCrate(Clone)");
            spawnables.Add("Snowman(Clone)");
            foreach (GameObject go in spawnableInstances["IceCrate(Clone)"])
            {
                go.GetComponent<Animator>().SetBool("IsRotating", true);
            }
            foreach (GameObject go in spawnableInstances["Snowman(Clone)"])
            {
                go.GetComponent<Animator>().SetBool("IsRotating", true);
            }
        }

        foreach (KeyValuePair<string, List<GameObject>> entry in spawnableInstances)
        {
            foreach (GameObject gobject in entry.Value)
            {
                gobject.SetActive(spawnables.Contains(entry.Key));
            }
        }

        collectables.Add("Orb(Clone)");
        
        foreach (KeyValuePair<string, List<GameObject>> entry in collectableInstances)
        {
            foreach (GameObject gobject in entry.Value)
            {
                gobject.SetActive(collectables.Contains(entry.Key));
            }
        }
        
        if (Global.Instance.orientation == 0)
        {
            Invoke("SpawnPlatform", 1.5f);
        }

        spawnables.TrimExcess();
        collectables.TrimExcess();

        Invoke("InvokeObstacleSpawns", 2f);
        Invoke("InvokeCollectableSpawns", 2f);
    }

    public void DisableAll()
    {
        foreach (var item in spawnableInstances)
        {
            foreach (var item2 in item.Value)
            {
                item2.GetComponent<ObstacleBase>().Disable();
            }
        }

        foreach (var item in collectableInstances)
        {
            foreach (var item2 in item.Value)
            {
                item2.GetComponent<ObstacleBase>().Disable();
            }
        }
    }

    public void LoadHorizontal(PerspectiveInitializer.ThemeState themeState)
    {
        platformManager.DisableAll();

        spawnables.Clear();
        collectables.Clear();

        if (themeState == PerspectiveInitializer.ThemeState.Mine)
        {
            spawnables.Add("Crate(Clone)");
            spawnables.Add("Fireball(Clone)");
            spawnables.Add("Pickaxe(Clone)");
            spawnables.Add("TNT(Clone)");
            spawnables.Add("BarrelDwarf(Clone)");

            foreach (GameObject go in spawnableInstances["Crate(Clone)"])
            {
                go.GetComponent<Animator>().SetBool("IsRotating", false);
            }
            foreach (GameObject go in spawnableInstances["TNT(Clone)"])
            {
                go.GetComponent<Animator>().SetBool("IsRotating", false);
            }
        }
        else
        {
            spawnables.Add("IceCrate(Clone)");
            spawnables.Add("Iceball(Clone)");
            spawnables.Add("Snowball(Clone)");
            spawnables.Add("Snowman(Clone)");
            spawnables.Add("BarrelEskimo(Clone)");

            foreach (GameObject go in spawnableInstances["IceCrate(Clone)"])
            {
                go.GetComponent<Animator>().SetBool("IsRotating", false);
            }
            foreach (GameObject go in spawnableInstances["Snowman(Clone)"])
            {
                go.GetComponent<Animator>().SetBool("IsRotating", false);
            }
        }

        foreach (KeyValuePair<string, List<GameObject>> entry in spawnableInstances)
        {
            foreach (GameObject gobject in entry.Value)
            {
                gobject.SetActive(spawnables.Contains(entry.Key));
            }
        }
        
        collectables.Add("MineCarVehicle(Clone)");
        collectables.Add("Orb(Clone)");

        foreach (KeyValuePair<string, List<GameObject>> entry in collectableInstances)
        {
            foreach (GameObject gobject in entry.Value)
            {
                if(entry.Key == "MineCarVehicle(Clone)")
                    gobject.GetComponent<MineCarVehicle>().AdjustToTheme(themeState);

                gobject.SetActive(collectables.Contains(entry.Key));
            }
        }

        if (Global.Instance.orientation == 0)
        {
            platformManager.AdjustTheme(themeState, ref mineBegin, ref mineMiddle, ref mineEnd, ref iceBegin, ref iceMiddle, ref iceEnd);
            GameObject.Find("Background Manager").GetComponent<BackgroundManager>().AdjustTheme(themeState);
            Invoke("SpawnPlatform", 1.5f);
        }

        spawnables.TrimExcess();
        collectables.TrimExcess();

        Invoke("InvokeObstacleSpawns", 2f);
        Invoke("InvokeCollectableSpawns", 2f);
    }

    // Update is called once per frame
    void Update () {
        platformManager.Update();
    }

    public void InvokeCollectableSpawns()
    {
        if (GameOverAnimation.GetInstance(  ).m_fAnimationInProgress)
            return;

        if (collectables.Count > 0)
        {
            string o1 = collectables[(int)Mathf.Round((Random.value * (collectables.Count - 1)))];

            foreach (GameObject o2 in collectableInstances[o1])
            {
                ObstacleBase o = o2.GetComponent<ObstacleBase>();

                if (o.active)
                    continue;

                o.Activate();
                break;
            }
        }

        Invoke("InvokeCollectableSpawns", ((collectableSpawnRate / 2.0f) * Random.value + collectableSpawnRate / 2.0f) / Global.Instance.speed + 5);
    }

    public void InvokeObstacleSpawns()
    {
        if (GameOverAnimation.GetInstance().m_fAnimationInProgress)
            return;

        float randomNumber = Random.value * 100;
        float amountSpawn = 100 / spawnables.Count;

        foreach (GameObject o2 in spawnableInstances[spawnables[(int)(randomNumber / amountSpawn)]])
        {
            if (o2.name == "Pickaxe(Clone)" && Time.time - Global.startTime < 30)
                break;

            if ((o2.name == "Fireball(Clone)" || o2.name == "Iceball(Clone)") && Time.time - Global.startTime < 80)
                break;
            if ((o2.name == "Fireball(Clone)" || o2.name == "Iceball(Clone)" ) && Random.value < 0.5f)
                break;

            ObstacleBase o = o2.GetComponent<ObstacleBase>();

            if (o.active)
                continue;

            o.Activate();
            break;
        }

        Invoke("InvokeObstacleSpawns", (obstacleSpawnRate * Random.value + 3) / Global.Instance.speed);
    }
}
