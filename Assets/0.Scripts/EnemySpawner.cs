using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;

    [SerializeField] private int baseEnemies = 8;  //�⺻ ���� ���� ��
    [SerializeField] private float enemiesperSecond = 0.5f;  // �ʴ� �����Ǵ� ���� ��    (1/enemiesperSecond)
    [SerializeField] private float timeBetWeenWaves = 5f;  //���̺� �� �ð� ����
    [SerializeField] private float difficultyScalingFactor = 0.75f;  //���̵� ���� ���

    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 1;  //���� ���̺�
    private float timeSinceLastSpawn;  //������ �� ���� ������ ��� �ð�
    private int enemiesAlive;  //���� �����ϴ� ���� ��
    private int enemiesLeftToSpawn;  //�������� ���� ���� ��
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

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / enemiesperSecond) && enemiesLeftToSpawn > 0)    //2�ʿ� 1��
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
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));  // ���� ���̺꿡 ���� ������ ���� �� ���
    }
}
