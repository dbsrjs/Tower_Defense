using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    [HideInInspector] public GameObject towerObj;
    [HideInInspector] public Turret turret;
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
            if (Input.GetMouseButtonDown(1))
            {
                turret = towerObj.GetComponent<Turret>();
                turret.TurretDestoy();
            }
        }
    }
}
