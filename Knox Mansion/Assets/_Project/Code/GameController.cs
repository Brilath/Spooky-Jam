using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private bool gameStart;
    [SerializeField] private GameObject player;

    private void Start()
    {
        Time.timeScale = 1;
        if(gameStart)
        {
            PlayerPrefs.SetInt("HEALTH", 5);
        }
        player.GetComponent<Health>().SetHealth(PlayerPrefs.GetInt("HEALTH"));
    }

}
