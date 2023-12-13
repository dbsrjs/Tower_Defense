using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TurretSlow : MonoBehaviour
{
    [SerializeField] private LayerMask enemyMask;  //�� ĳ���͸� �Ǻ��ϱ� ���� ���̾� ����ũ

    [SerializeField] private float targetingRange = 5f;  //���� �Ÿ�
    [SerializeField] private float aps = 4f;    //���� �ӵ�
    [SerializeField] private float freezeTime = 1f; //���� �ð�

    private float timeUntilFire;

    private void Update()
    {
        timeUntilFire += Time.deltaTime;

        if (timeUntilFire >= 1f / aps)
        {
            Debug.Log("Freeze");
            FreezeEnemies();
            timeUntilFire = 0f;
        }
    }

    public void FreezeEnemies()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                EnemyMovement em = hit.transform.GetComponent<EnemyMovement>();
                em.UpdateSpeed(0.5f);
                StartCoroutine(ResetEnemyspeed(em));
            }
        }
    }

    private IEnumerator ResetEnemyspeed(EnemyMovement em)
    {
        yield return new WaitForSeconds(freezeTime);

        em.ResetSpeed();
    }

    /*private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan; //������ ����
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);  //������ �󿡼� ���� �Ÿ��� �ð������� ǥ��
    }*/
}