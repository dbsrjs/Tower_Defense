using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform turretRotationPoint;  //포탑 회전 지점의 Transform 컴포넌트
    [SerializeField] private LayerMask enemyMask;  //적 캐릭터를 판별하기 위한 레이어 마스크

    [SerializeField] private float targetingRange = 5f;  //사정 거리
    [SerializeField] private float rotationSpeed = 5f;  //포탑 회전 속도

    private Transform target;  // 현재 타겟으로 지정된 Transform 컴포넌트

    private void Update()
    {
        if (target == null)
        {
            FindTarget();  // 타겟을 찾는 메서드 호출
            return;
        }

        RotateTowardsTarget();  // 타겟 방향으로 회전

        if (CheckTargetIsinRange() == false)
        {
            target = null;  // 타겟이 사정 거리를 벗어나면 타겟 초기화
        }
    }

    private void FindTarget()
    {
        //CircleCastAll을 사용하여 주변의 적 캐릭터를 찾음
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;  //가장 가까운 적을 타겟으로 지정
        }
    }

    private bool CheckTargetIsinRange()
    {
        //타겟과의 거리가 사정 거리 이내인지 확인
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void RotateTowardsTarget()
    {
        // 타겟의 위치를 기준으로 포탑이 회전해야 할 각도 계산
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan; //프레임 색상
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);  //에디터 상에서 사정 거리를 시각적으로 표시
    }
}
