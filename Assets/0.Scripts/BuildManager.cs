using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [SerializeField] private Tower[] towers;

    private int selectedTopwer = 0;

    private void Awake()
    {
        main = this;
    }

    public Tower GetSelectedTower()
    {
        return towers[selectedTopwer];
    }

    public void SetSelectedTower(int _selectedTower)
    {
        selectedTopwer = _selectedTower;
    }
}
