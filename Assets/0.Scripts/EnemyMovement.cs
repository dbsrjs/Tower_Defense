using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float moveSpeed = 2f;  //이동 속도

    private Transform target;
    private int pathIndex = 0;

    private void Start()
    {
        target = LevelManager.main.path[pathIndex];  //목표 지점 설정
    }

    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)  //현재 위치와 목표 지점과의 거리가 0.1f 이하일 경우
        {
            pathIndex++;  //다음 목표 지점으로 이동

            if (pathIndex == LevelManager.main.path.Length)  //End Point
            {
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];  //다음 목표 지점 설정
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;  //캐릭터가 목표 지점으로 향하는 방향을 단위 벡터로 계산

        rb.velocity = direction * moveSpeed;  //Rigidbody2D 컴포넌트의 속도를 방향과 이동 속도를 곱한 값으로 설정하여 이동
    }
}