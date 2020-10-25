using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject flashLight;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            ToggleItems(pistol);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleItems(flashLight);
        }
    }

    private void ToggleItems(GameObject item)
    {
        if(pistol.Equals(item))
        {
            pistol.SetActive(true);
            flashLight.SetActive(false);
        }
        else
        {
            pistol.SetActive(false);
            flashLight.SetActive(true);
        }
    }
}
