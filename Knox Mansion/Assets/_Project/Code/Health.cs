using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int max;
    [SerializeField] private int current;

    public static Action<int> OnHealthChanged = delegate {};
    public static Action OnDamageTaken = delegate { };

    private void Start()
    {
        if (string.Compare(gameObject.tag, "Monster") == 0 || string.Compare(gameObject.tag, "Boss") == 0)
        {
            current = max;
        }        
    }

    public void TakeDamage(int damage)
    {
        current -= damage;
        current = Mathf.Clamp(current, 0, max);
        if (string.Compare(gameObject.tag, "Player") == 0)
        {
            UpdatePlayerHealth();
        }
        else if (current <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            //OnDamageTaken?.Invoke();
        }
    }

    public void TakeHealing(int heal)
    {
        current += heal;
        current = Mathf.Clamp(current, 0, max);
        if (string.Compare(gameObject.tag, "Player") == 0)
        {
            UpdatePlayerHealth();
        }
    }

    public void SetHealth(int amount)
    {
        current = amount;
        current = Mathf.Clamp(current, 0, max);
        if (string.Compare(gameObject.tag, "Player") == 0)
        {
            UpdatePlayerHealth();
        }
    }

    private void UpdatePlayerHealth()
    {
        PlayerPrefs.SetInt("HEALTH", current);
        OnHealthChanged?.Invoke(current);
    }
}
