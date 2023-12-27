using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    [HideInInspector] public GameObject towerObj;   //포탑 유무 여부
    [HideInInspector] public Turret turret;
    [HideInInspector] public TurretSlow turretSlow;
    private Color startColor;

    private void Start()
    {
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        if (UIManager.main.IsHoveringUI())
        {
            return;
        }

        if (towerObj != null)   //포탑이 있을 때
        {
            turret.OpenUpgradeUI();
            return;
        }

        Tower towerToBuild = BuildManager.main.GetSelectedTower();

        if (towerToBuild.cost > LevelManager.main.currency) //포탑을 설치 할 돈이 부족할 때
        {
            Debug.Log("타워를 구매할 수 없습니다.");
            return;
        }

        LevelManager.main.SpendCurrency(towerToBuild.cost);

        towerObj = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
        turret = towerObj.GetComponent<Turret>();
    }

    private void OnMouseOver()  //마우스가 오브젝트 위에 올라가 있는 동안
    {
        if (towerObj != null)
        {
            turret = towerObj.GetComponent<Turret>();
            turretSlow = towerObj.GetComponent<TurretSlow>();

            if (towerObj.GetComponent<Turret>() == true)    //터렛 사정거리 표시
            {
                turret.border.SetActive(true);
            }

            else
            {
                turretSlow.OnDrawGizmosSelected();
            }

            if (Input.GetMouseButtonDown(1))    //터렛 삭제(우클릭)
            {
                

                if (towerObj.GetComponent<Turret>() == true)
                {
                    turret.TurretDestoy();
                }

                else
                {
                    turretSlow.TurretDestoy();
                }
            }
        }
    }
}
