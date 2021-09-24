using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public Animator anim;
    public int CurrentHealth { get; private set; }
    public AudioClip deathSFX;
    private AudioSource Audio;
    private bool AudioTrigger = false;

    void Start()
    {
        CurrentHealth = 100;
        anim = GetComponent<Animator>();
        Audio = GetComponent<AudioSource>();
    }
    void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        Debug.Log($"{name} has {CurrentHealth} health");
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Debug.Log($"{name} DIE");
            anim.SetTrigger("Death");

            if (!AudioTrigger)
            {
                Audio.PlayOneShot(deathSFX, 0.1f);
                AudioTrigger = true;
            }
            //Destroy(gameObject);
           
        }
    }






}
