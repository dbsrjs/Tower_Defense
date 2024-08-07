using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint;
    public Transform[] path;

    public int currency;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        currency = 300;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.F1))
            currency += 100000;

    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            currency -= amount;
            return true;
        }

        else
        {
            Debug.Log("돈이 충분하지 않습니다.");
            return false;
        }
    }
}
