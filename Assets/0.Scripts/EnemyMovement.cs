using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float moveSpeed = 2f;  //�̵� �ӵ�

    private Transform target;
    private int pathIndex = 0;

    private void Start()
    {
        target = LevelManager.main.path[pathIndex];  //��ǥ ���� ����
    }

    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)  //���� ��ġ�� ��ǥ �������� �Ÿ��� 0.1f ������ ���
        {
            pathIndex++;  //���� ��ǥ �������� �̵�

            if (pathIndex == LevelManager.main.path.Length)  //End Point
            {
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];  //���� ��ǥ ���� ����
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;  //ĳ���Ͱ� ��ǥ �������� ���ϴ� ������ ���� ���ͷ� ���

        rb.velocity = direction * moveSpeed;  //Rigidbody2D ������Ʈ�� �ӵ��� ����� �̵� �ӵ��� ���� ������ �����Ͽ� �̵�
    }
}