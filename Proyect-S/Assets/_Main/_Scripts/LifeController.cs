using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void SimpleDelegate();

public class LifeController : MonoBehaviour, IDamageable
{
    [SerializeField] private Animator anim;
    public float CurrentHealth { get; private set; }
    [SerializeField] float healthRegen = 1f;
    private float maxHealth;
    private Rigidbody rb;
    private Collider coll;

    public event SimpleDelegate OnDeath;
    public event SimpleDelegate OnHealthChange;
    void Start()
    {
        maxHealth = 100;
        CurrentHealth = maxHealth;
        coll = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (CurrentHealth < maxHealth)
        {
            CurrentHealth += healthRegen * Time.deltaTime;
        }
        if (CurrentHealth > maxHealth)
        {
            CurrentHealth = maxHealth;
        }

    }
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        OnHealthChange?.Invoke();
        if (CurrentHealth <= 0)
        {
            coll.enabled = false;
            CurrentHealth = 0;
            anim.SetTrigger("Death");
            rb.isKinematic = true;

            OnDeath?.Invoke();
            //Destroy(gameObject);

        }
    }
}
