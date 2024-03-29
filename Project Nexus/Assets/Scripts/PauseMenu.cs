using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject Weapon;
    public GameObject Arrow;

    void start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        Arrow.SetActive(true);
        Weapon.SetActive(true);
        pauseMenuUI.SetActive(false);
        FindObjectOfType<AudioManager>().Play("BattleTheme");
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()
    {
        Arrow.SetActive(false);
        Weapon.SetActive(false);
        pauseMenuUI.SetActive(true);
        FindObjectOfType<AudioManager>().Stop("BattleTheme");
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Loading MainMenu...");
    }
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}