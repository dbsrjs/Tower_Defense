using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager main;

    [SerializeField] private GameObject title;
    [SerializeField] private GameObject check_Exit;
    [SerializeField] private GameObject game_Over;

    [SerializeField] private GameObject text;

    [SerializeField] private GameObject start_Btn;
    [SerializeField] private GameObject exit_Btn;

    [SerializeField] private GameObject ui;
    [SerializeField] private GameObject menu;


    // Start is called before the first frame update
    void Start()
    {
        main = this;
        Time.timeScale = 0;
        title.SetActive(true);
        game_Over.SetActive(false);
        ui.SetActive(false);
        menu.SetActive(false);
        check_Exit.SetActive(false);
    }

    public void Start_Btn()
    {
        Time.timeScale = 1;
        title.SetActive(false);
        ui.SetActive(true);
        menu.SetActive(true);
    }

    public void Exit_Btn()
    {
        start_Btn.SetActive(false);
        exit_Btn.SetActive(false);
        text.SetActive(false);
        check_Exit.SetActive(true);
    }

    public void Yes_Btn()
    {
        UnityEditor.EditorApplication.isPlaying = false;    //게임 종료(실행)
        //Application.Quit(); // 게임 종료(build)
    }

    public void No_Btn()
    {
        exit_Btn.SetActive(true);
        start_Btn.SetActive(true);
        text.SetActive(true);
        check_Exit.SetActive(false);
    }

    public void Game_Over()
    {
        Time.timeScale = 0;
        game_Over.SetActive(true);

        Ui.main.num = 3;
        Ui.main.wave.text = "1";
        Health.main.hitPoints = 2;
        EnemySpawner.main.ReGame();
        LevelManager.main.currency = 300;
    }

    public void RePlay_Yes()
    {
        game_Over.SetActive(false);
        Time.timeScale = 1;
    }

    public void RePlay_No()
    {
        game_Over.SetActive(false);
        title.SetActive(true);
    }
}
