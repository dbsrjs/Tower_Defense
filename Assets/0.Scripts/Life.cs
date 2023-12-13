using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public static Life main;

    public GameObject[] life;

    private int num = 0;

    private void Awake()
    {
        main = this;
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
