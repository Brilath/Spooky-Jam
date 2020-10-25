using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int max;
    [SerializeField] private int current;

    public static Action<int> OnHealthChanged = delegate {};

    private void Start()
    {
        if (string.Compare(gameObject.tag, "Monster") == 0)
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
            OnHealthChanged?.Invoke(current);
        }
        else if (current <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeHealing(int heal)
    {
        current += heal;
        current = Mathf.Clamp(current, 0, max);
        if (string.Compare(gameObject.tag, "Player") == 0)
        {
            OnHealthChanged?.Invoke(current);
        }
    }

    public void SetHealth(int amount)
    {
        current = amount;
        current = Mathf.Clamp(current, 0, max);
        if (string.Compare(gameObject.tag, "Player") == 0)
        {
            OnHealthChanged?.Invoke(current);
        }
    }
}
