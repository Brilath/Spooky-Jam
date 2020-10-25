using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject[] healthBar;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gameoverPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private bool gameover;

    private void Awake()
    {
        Health.OnHealthChanged += OnHealthChanged;
        WinController.OnWinGame += OnWinGame;
    }

    private void OnDestroy()
    {
        Health.OnHealthChanged -= OnHealthChanged;
        WinController.OnWinGame -= OnWinGame;
    }

    private void Update()
    {
        if (gameover) return;
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenuPanel();
        }
    }

    private void OnHealthChanged(int health)
    {
        Debug.Log($"Changing UI Health {health}");
        foreach(GameObject h in healthBar)
        {
            h.SetActive(false);
        }
        for(int i = 0; i < health; i++)
        {
            healthBar[i].SetActive(true);
        }

        if (health <= 0)
        {
            gameover = true;
            ToggleGameOverPanel();
        }
    }


    private void OnWinGame()
    {
        gameover = true;
        ToggleWinPanel();
    }

    private void ToggleGameOverPanel()
    {
        gameoverPanel.SetActive(true);
    }

    private void ToggleWinPanel()
    {
        winPanel.SetActive(true);
    }

    private void ToggleMenuPanel()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);

        if(menuPanel.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void Resume()
    {
        ToggleMenuPanel();
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
