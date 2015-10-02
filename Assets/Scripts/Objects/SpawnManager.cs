using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Objects;

public class SpawnManager : MonoBehaviour {

    public static SpawnManager Instance;// = new SpawnManager();
    public List<ObstacleBase> spawnables = new List<ObstacleBase>();
    public List<ObstacleBase> collectables = new List<ObstacleBase>();
    public Dictionary<ObstacleBase, List<GameObject>> spawnableInstances = new Dictionary<ObstacleBase, List<GameObject>>();
    public Dictionary<ObstacleBase, List<GameObject>> collectableInstances = new Dictionary<ObstacleBase, List<GameObject>>();

    public PlatformManager platformManager = new PlatformManager();

    public float obstacleSpawnRate = 5f;
    public float collectableSpawnRate = 10f;
    public int defaultSpawnableCount = 5;
    public int defaultCollectableCount = 20;
    
    void Awake() {
        Instance = this;
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

    public void RemoveAll()
    {
        CancelInvoke();

        foreach (var item in spawnableInstances)
        {
            foreach (var item2 in item.Value)
            {
                GameObject.Destroy(item2);
            }
        }

        foreach (var item in collectableInstances)
        {
            foreach (var item2 in item.Value)
            {
                GameObject.Destroy(item2);
            }
        }

        spawnableInstances.Clear();
        collectableInstances.Clear();
        spawnables.Clear();
        collectables.Clear();
    }

    public bool IsAnyFireballActive()
    {
        bool temp = false;
        int index = 0;

        for (int i = 0; i < spawnables.Count; ++i)
        {
            ObstacleBase o = spawnables[i];
            if (o.name == "Fireball" || o.name == "Fireball(Clone)")
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
            if ((gobject.name == "Fireball" || gobject.name == "Fireball(Clone)") && gobject.GetComponent<ObstacleBase>().active)
            {
                temp = true;
                break;
            }
        }

        return temp;
    }

    public void Init()
    {
        foreach (var item in spawnables)
        {
            for (int i = 0; i < defaultSpawnableCount; i++)
            {
                if (!spawnableInstances.ContainsKey(item))
                {
                    spawnableInstances.Add(item, new List<GameObject>());
                }

                spawnableInstances[item].Add(item.Spawn());
            }
        }

        foreach (var item in collectables)
        {
            if (!collectableInstances.ContainsKey(item))
            {
                collectableInstances.Add(item, new List<GameObject>());
            }

            for (int i = 0; i < defaultSpawnableCount; i++)
            {
                collectableInstances[item].Add(item.Spawn());
            }
        }

        if (Global.Instance.orientation == 0)
        {
            platformManager.Start();
            platformManager.Spawn();
            Invoke("SpawnPlatform", 1.5f);
        }
        else
        {
            platformManager.FinalizeObject();
        }

        Invoke("InvokeObstacleSpawns", 2f);
        Invoke("InvokeCollectableSpawns", 2f);
    }

    // Update is called once per frame
    void Update () {
        platformManager.Update();
    }

    public void InvokeCollectableSpawns()
    {
        if (GameOverAnimation.GetInstance().m_fAnimationInProgress)
            return;

        if (spawnables.Count > 0)
        {
            ObstacleBase o1 = collectables[(int)Mathf.Round((Random.value * (collectables.Count - 1)))];

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
            if (o2.name == "Pickaxe(Clone)" && Time.time < 30)
                break;

            if (o2.name == "Fireball(Clone)" && Time.time < 80)
                break;
            if (o2.name == "Fireball(Clone)" && Random.value < 0.5f)
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
