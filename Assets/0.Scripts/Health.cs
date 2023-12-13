using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    public static Health main;

    [SerializeField] private int hitPoints = 2; //HP
    [SerializeField] private int currencyWorth = 50;    //¶³±¸´Â µ·
    [SerializeField] private TMP_Text hp_text;

    private bool isDestoryed = false;

    private void Awake()
    {
        main = this;
        hp_text.text = hitPoints.ToString();
    }

    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;
        hp_text.text = hitPoints.ToString();
        if (hitPoints <= 0 && isDestoryed != true)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyWorth);
            isDestoryed = true;
            Destroy(gameObject);
        }
    }

    public void HpUphrade()
    {
        hitPoints += 2;
    }
}
