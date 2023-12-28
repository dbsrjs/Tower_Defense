using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TurretSlow : MonoBehaviour
{
    [SerializeField] private LayerMask enemyMask;  //�� ĳ���͸� �Ǻ��ϱ� ���� ���̾� ����ũ

    [SerializeField] private float targetingRange = 2f;  //���� �Ÿ�
    [SerializeField] private float aps = 4.4f;    //���� �ӵ�
    [SerializeField] private float freezeTime = 1f; //���� �ð�
    public GameObject border;

    private float timeUntilFire;

    private void Start()
    {
        border.SetActive(false);
        border.transform.localScale = new Vector3(targetingRange / 2f, targetingRange / 2f, targetingRange / 2f);
    }

    private void Update()
    {
        timeUntilFire += Time.deltaTime;

        if (timeUntilFire >= 1f / aps)
        {
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

    public void TurretDestoy()  //�ͷ� ����
    {
        Destroy(gameObject);
        LevelManager.main.currency += 100;
    }
}
