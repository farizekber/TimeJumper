using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;

public class SpawnManager : MonoBehaviour {

    public static SpawnManager Instance;// = new SpawnManager();
    public List<ObstacleBase> spawnables = new List<ObstacleBase>();
    public List<ObstacleBase> collectables = new List<ObstacleBase>();
    public Dictionary<ObstacleBase, List<GameObject>> spawnableInstances = new Dictionary<ObstacleBase, List<GameObject>>();
    public Dictionary<ObstacleBase, List<GameObject>> collectableInstances = new Dictionary<ObstacleBase, List<GameObject>>();

    public float obstacleSpawnRate = 5f;
    public float collectableSpawnRate = 3f;
    public int defaultSpawnableCount = 5;
    public int defaultCollectableCount = 20;

    // Use this for initialization
    void Start() {
        Instance = this;
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

        Invoke("InvokeObstacleSpawns", 2f);
        Invoke("InvokeCollectableSpawns", 2f);
    }

    // Update is called once per frame
    void Update () {

    }

    public void InvokeCollectableSpawns()
    {
        if (GameOverAnimation.GetInstance().m_fAnimationInProgress)
            return;

        //float randomNumber = Random.value * 100;
        //float amountSpawn = 100 / spawnables.Count;
        //spawnables[(int)(randomNumber / amountSpawn)].Spawn();

        foreach (ObstacleBase o1 in collectables)
        {
            bool found = false;
            foreach (GameObject o2 in collectableInstances[o1])
            {
                ObstacleBase o = o2.GetComponent<ObstacleBase>();

                if (o.active)
                    continue;

                o.Activate();
                found = true;
                break;
            }
            if (found)
                break;
        }

        if (Random.value > 0.75)
        {
            foreach (ObstacleBase o1 in collectables)
            {
                bool found = false;
                foreach (GameObject o2 in collectableInstances[o1])
                {
                    ObstacleBase o = o2.GetComponent<ObstacleBase>();

                    if (o.active)
                        continue;

                    o.Activate();
                    found = true;
                    break;
                }
                if (found)
                    break;
            }
        }

        if (Random.value > 0.75)
        {
            foreach (ObstacleBase o1 in collectables)
            {
                bool found = false;
                foreach (GameObject o2 in collectableInstances[o1])
                {
                    ObstacleBase o = o2.GetComponent<ObstacleBase>();

                    if (o.active)
                        continue;

                    o.Activate();
                    found = true;
                    break;
                }
                if (found)
                    break;
            }
        }

        Invoke("InvokeCollectableSpawns", (collectableSpawnRate * Random.value + 3) / Global.Instance.speed);
    }

    public void InvokeObstacleSpawns()
    {
        if (GameOverAnimation.GetInstance().m_fAnimationInProgress)
            return;

        float randomNumber = Random.value * 100;
        float amountSpawn = 100 / spawnables.Count;

        foreach (GameObject o2 in spawnableInstances[spawnables[(int)(randomNumber / amountSpawn)]])
        {
            ObstacleBase o = o2.GetComponent<ObstacleBase>();

            if (o.active)
                continue;

            o.Activate();
            break;
        }

        Invoke("InvokeObstacleSpawns", (obstacleSpawnRate * Random.value + 3) / Global.Instance.speed);
    }
}
