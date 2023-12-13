using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int hitPoints = 2; //HP
    [SerializeField] private int currencyWorth = 50;    //¶³±¸´Â µ·

    private bool isDestoryed = false;

    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;
        if (hitPoints <= 0 && isDestoryed != true)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyWorth);
            isDestoryed = true;
            Destroy(gameObject);
        }
    }
}
