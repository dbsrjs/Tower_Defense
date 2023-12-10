using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;

    [SerializeField] private int baseEnemies = 8;  //기본 적의 생성 수
    [SerializeField] private float enemiesperSecond = 0.5f;  // 초당 생성되는 적의 수    (1/enemiesperSecond)
    [SerializeField] private float timeBetWeenWaves = 5f;  //웨이브 간 시간 간격
    [SerializeField] private float difficultyScalingFactor = 0.75f;  //난이도 조절 계수

    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 1;  //현재 웨이브
    private float timeSinceLastSpawn;  //마지막 적 생성 이후의 경과 시간
    private int enemiesAlive;  //현재 존재하는 적의 수
    private int enemiesLeftToSpawn;  //생성되지 않은 적의 수
    private bool isSpawning = false;  //적 생성 상태 확인 변수

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);  // 적이 파괴되었을 때 호출될 함수
    }

    private void Start()
    {
        StartCoroutine(StartWave());  //첫 웨이브 시작
    }

    private void Update()
    {
        if (isSpawning == false)
        {
            return;
        }

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / enemiesperSecond) && enemiesLeftToSpawn > 0)    //2초에 1명
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetWeenWaves);  //웨이브 간 시간 간격만큼 대기
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();  //생성될 적의 수 계산
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }

    private void SpawnEnemy()
    {
        GameObject prefabToSpawn = enemyPrefabs[0];
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));  // 현재 웨이브에 대한 생성될 적의 수 계산
    }
}
