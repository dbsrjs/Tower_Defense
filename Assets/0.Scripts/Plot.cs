using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    [HideInInspector] public GameObject towerObj;   //��ž ���� ����
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

        if (towerObj != null)   //��ž�� ���� ��
        {
            turret.OpenUpgradeUI();
            return;
        }

        Tower towerToBuild = BuildManager.main.GetSelectedTower();

        if (towerToBuild.cost > LevelManager.main.currency) //��ž�� ��ġ �� ���� ������ ��
        {
            Debug.Log("Ÿ���� ������ �� �����ϴ�.");
            return;
        }

        LevelManager.main.SpendCurrency(towerToBuild.cost);

        towerObj = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
        turret = towerObj.GetComponent<Turret>();
    }

    private void OnMouseOver()  //���콺�� ������Ʈ ���� �ö� �ִ� ����
    {
        if (towerObj != null)
        {
            turret = towerObj.GetComponent<Turret>();
            turretSlow = towerObj.GetComponent<TurretSlow>();

            if (towerObj.GetComponent<Turret>() == true)    //�ͷ� �����Ÿ� ǥ��
            {
                turret.border.SetActive(true);
            }

            else
            {
                turretSlow.OnDrawGizmosSelected();
            }

            if (Input.GetMouseButtonDown(1))    //�ͷ� ����(��Ŭ��)
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
