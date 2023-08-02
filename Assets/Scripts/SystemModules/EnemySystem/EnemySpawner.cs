using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemySpawner : PersistentSingleton<EnemySpawner>
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public int foodSpawnCount;//本波次将生成的食物数量
        public int foodCount;//本波次已生成的食物数量
        public List<EnemyGroup> enemyGroups;//本波次将生成的敌人组列表
        public int waveQuota;//本波次生成的敌人总数
        public float spawnInterval;//生成敌人的间隔时间
        public int spawnCount;//本波次中已经生成了的敌人数
        public int defeatEnemiesCount;//本波次击败的敌人数
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount;//此类敌人在本波次中的生成数
        public int spawnCount;//此类敌人在本波次中已经生成了的敌人数
        public GameObject enemyPrefab;
    }

    public GameObject[] foodPrefabArray;//食物预制体数组
    public List<Wave> waves;//所有波数的列表
    public int currentWaveCount;//当前波次的下标，列表从0开始

    [Header("Spawner Attributes")]
    public float spawnTimer;//下一个敌人生成的时间
    public int enemiesAlive;//存活的敌人数
    public float spawnFoodInterval;//生成食物的间隔时间
    public bool isWaveActive = false;//波次时候激活

    //---------------------------------------------------------------------//

    Transform Snake;

    private WaitUntil waitUntilNoEnemy;

    //---------------------------------------------------------------------//

    protected override void Awake()
    {
        base.Awake();
        waitUntilNoEnemy = new WaitUntil(() => enemiesAlive == 0);
    }

    #region Unity生命周期函数
    void Start()
    {
        Snake = FindObjectOfType<SnakeController>().transform;
        CalculateWaveQuota();
    }

    
    void Update()
    {
        if (currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0&&!isWaveActive)
        {
            if (currentWaveCount > 0)
            {
                GameManager.Instance.Prepare();
            }
           
            //暂停，切换至Prepare状态，进行节点调整和选择加成
            StartCoroutine(BeginNextWave());
        }

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0;
            SpawnEnemies();
            SpawnFoods();
        }
    }
    #endregion

    IEnumerator BeginNextWave()
    {
        isWaveActive = true;

        yield return waitUntilNoEnemy;

        if (currentWaveCount < waves.Count - 1)
        {
            isWaveActive = false;
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }

    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }

        waves[currentWaveCount].waveQuota = currentWaveQuota;
        Debug.LogWarning("本波次生成的敌人总数: "+currentWaveQuota);
        enemiesAlive = currentWaveQuota;
        waves[currentWaveCount].defeatEnemiesCount = 0;
    }

    void SpawnEnemies()
    {
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota)
        {
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    Vector2 spawnPosition = new Vector2(Snake.transform.position.x + Random.Range(-100f, 100f),
                        Snake.transform.position.y + Random.Range(-100f, 100f));
                    ObjectPool.Instance.GetObject(enemyGroup.enemyPrefab, spawnPosition, Quaternion.identity);

                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                }
            }
        }

        
    }

    void SpawnFoods()
    {
        int temp;
        if (waves[currentWaveCount].foodCount < waves[currentWaveCount].foodSpawnCount)
        {
            for (int i = 0; i < waves[currentWaveCount].foodSpawnCount; i++)
            {
                temp = Random.Range(0, 3);
                Vector2 spawnPosition = new Vector2(Snake.transform.position.x + Random.Range(-100f, 100f),
                    Snake.transform.position.y + Random.Range(-100f, 100f));
                ObjectPool.Instance.GetObject(foodPrefabArray[temp], spawnPosition, Quaternion.identity);
                waves[currentWaveCount].foodCount++;
            }
        }
    }

    public void OnEnemyDied()
    {
        enemiesAlive--;
        waves[currentWaveCount].defeatEnemiesCount++;
    }

    public void OnNextWave()
    {

    }

    public void OnWaveFinished()
    {

    }

}
