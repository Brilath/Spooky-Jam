using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject flashLight;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip flashLightOnClip;
    [SerializeField] private AudioClip rackPistolClip;

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
            audioSource.PlayOneShot(rackPistolClip);
            pistol.SetActive(true);
            flashLight.SetActive(false);
        }
        else
        {
            audioSource.PlayOneShot(flashLightOnClip);
            pistol.SetActive(false);
            flashLight.SetActive(true);
        }
    }
}
