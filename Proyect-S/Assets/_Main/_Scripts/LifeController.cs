using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour, IDamageable
{
    public int CurrentHealth { get; private set; }
    void Start()
    {
        CurrentHealth = 100;
    }
    void Update()
    {

    }
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {         
            CurrentHealth = 0;               
        }
    }
}
