using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TurretSlow : MonoBehaviour
{
    [SerializeField] private LayerMask enemyMask;  //적 캐릭터를 판별하기 위한 레이어 마스크

    [SerializeField] private float targetingRange = 5f;  //사정 거리
    [SerializeField] private float aps = 4.4f;    //공격 속도
    [SerializeField] private float freezeTime = 1f; //정지 시간

    private float timeUntilFire;

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

    public void TurretDestoy()  //터렛 삭제
    {
        Destroy(gameObject);
        LevelManager.main.currency += 100;
    }


    public void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan; //프레임 색상
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);  //에디터 상에서 사정 거리를 시각적으로 표시
    }
}
