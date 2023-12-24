using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ui : MonoBehaviour
{
    public static Ui main;

    public GameObject[] life;
    public TMP_Text wave_Text;

    private int num = 0;

    private void Awake()
    {
        main = this;
        wave_Text.text = "1";
    }

    private void Update()
    {
        
    }

    public void Hit()
    {
        Destroy(life[num]);
        num++;
        if (num == 3)
        {
            Time.timeScale = 0;
        }
    }
}
