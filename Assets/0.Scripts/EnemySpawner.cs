using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;

    [SerializeField] private int baseEnemies = 8;  //�⺻ ���� ���� ��
    [SerializeField] private float enemiesPerSecond = 0.5f;  // �ʴ� �����Ǵ� ���� ��    (1/enemiesperSecond)
    [SerializeField] private float timeBetWeenWaves = 5f;  //���̺� �� �ð� ����
    [SerializeField] private float difficultyScalingFactor = 0.8f;  //���̵� ���� ���
    [SerializeField] private float enemiesPerSecondCap = 15f;

    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 1;  //���� ���̺�
    private int upgradeWave = 0;
    private float timeSinceLastSpawn;  //������ �� ���� ������ ��� �ð�
    private int enemiesAlive;  //���� �����ϴ� ���� ��
    private int enemiesLeftToSpawn;  //�������� ���� ���� ��
    private float eps;  //Enemies Per Second
    private bool isSpawning = false;  //�� ���� ���� Ȯ�� ����

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);  // ���� �ı��Ǿ��� �� ȣ��� �Լ�
    }

    private void Start()
    {
        StartCoroutine(StartWave());  //ù ���̺� ����
    }

    private void Update()
    {
        if (isSpawning == false)
        {
            return;
        }

        if (upgradeWave == 3)
        {
            Health.main.HpUphrade();
            upgradeWave = 0;
        }

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0)    //2�ʿ� 1��
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
        yield return new WaitForSeconds(timeBetWeenWaves);  //���̺� �� �ð� ���ݸ�ŭ ���
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();  //������ ���� �� ���
        eps = EnemiesPerSecond();
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        upgradeWave++;
        StartCoroutine(StartWave());
    }

    private void SpawnEnemy()
    {
        int index = Random.Range(0, enemyPrefabs.Length);
        GameObject prefabToSpawn = enemyPrefabs[index];
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));  // ���� ���̺꿡 ���� ������ ���� �� ���
    }

    private float EnemiesPerSecond()
    {
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0f, enemiesPerSecondCap);  // ���� ���̺꿡 ���� ������ ���� �� ���
    }
}
