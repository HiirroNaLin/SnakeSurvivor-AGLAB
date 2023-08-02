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
        public int foodSpawnCount;//�����ν����ɵ�ʳ������
        public int foodCount;//�����������ɵ�ʳ������
        public List<EnemyGroup> enemyGroups;//�����ν����ɵĵ������б�
        public int waveQuota;//���������ɵĵ�������
        public float spawnInterval;//���ɵ��˵ļ��ʱ��
        public int spawnCount;//���������Ѿ������˵ĵ�����
        public int defeatEnemiesCount;//�����λ��ܵĵ�����
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount;//��������ڱ������е�������
        public int spawnCount;//��������ڱ��������Ѿ������˵ĵ�����
        public GameObject enemyPrefab;
    }

    public GameObject[] foodPrefabArray;//ʳ��Ԥ��������
    public List<Wave> waves;//���в������б�
    public int currentWaveCount;//��ǰ���ε��±꣬�б��0��ʼ

    [Header("Spawner Attributes")]
    public float spawnTimer;//��һ���������ɵ�ʱ��
    public int enemiesAlive;//���ĵ�����
    public float spawnFoodInterval;//����ʳ��ļ��ʱ��
    public bool isWaveActive = false;//����ʱ�򼤻�

    //---------------------------------------------------------------------//

    Transform Snake;

    private WaitUntil waitUntilNoEnemy;

    //---------------------------------------------------------------------//

    protected override void Awake()
    {
        base.Awake();
        waitUntilNoEnemy = new WaitUntil(() => enemiesAlive == 0);
    }

    #region Unity�������ں���
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
           
            //��ͣ���л���Prepare״̬�����нڵ������ѡ��ӳ�
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
        Debug.LogWarning("���������ɵĵ�������: "+currentWaveQuota);
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
