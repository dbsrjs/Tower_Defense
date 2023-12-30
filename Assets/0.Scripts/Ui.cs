using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ui : MonoBehaviour
{
    public static Ui main;

    public TMP_Text life;
    public TMP_Text wave;

    [HideInInspector] public int num = 3;

    private void Awake()
    {
        main = this;
        life.text = num.ToString();
        wave.text = "1";
    }

    public void Hit()
    {
        num--;
        life.text = num.ToString();
        if (num == 0)
        {
            Time.timeScale = 0;
            GameManager.main.Game_Over();
        }
    }
}
