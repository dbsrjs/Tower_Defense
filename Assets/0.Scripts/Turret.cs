using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform turretRotationPoint;  //��ž ȸ�� ������ Transform ������Ʈ
    [SerializeField] private LayerMask enemyMask;  //�� ĳ���͸� �Ǻ��ϱ� ���� ���̾� ����ũ

    [SerializeField] private float targetingRange = 5f;  //���� �Ÿ�
    [SerializeField] private float rotationSpeed = 5f;  //��ž ȸ�� �ӵ�

    private Transform target;  // ���� Ÿ������ ������ Transform ������Ʈ

    private void Update()
    {
        if (target == null)
        {
            FindTarget();  // Ÿ���� ã�� �޼��� ȣ��
            return;
        }

        RotateTowardsTarget();  // Ÿ�� �������� ȸ��

        if (CheckTargetIsinRange() == false)
        {
            target = null;  // Ÿ���� ���� �Ÿ��� ����� Ÿ�� �ʱ�ȭ
        }
    }

    private void FindTarget()
    {
        //CircleCastAll�� ����Ͽ� �ֺ��� �� ĳ���͸� ã��
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;  //���� ����� ���� Ÿ������ ����
        }
    }

    private bool CheckTargetIsinRange()
    {
        //Ÿ�ٰ��� �Ÿ��� ���� �Ÿ� �̳����� Ȯ��
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void RotateTowardsTarget()
    {
        // Ÿ���� ��ġ�� �������� ��ž�� ȸ���ؾ� �� ���� ���
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan; //������ ����
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);  //������ �󿡼� ���� �Ÿ��� �ð������� ǥ��
    }
}
