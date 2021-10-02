using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour, IDamageable
{
    public Animator anim;
    public int CurrentHealth { get; private set; }
    public AudioClip deathSFX;
    private AudioSource Audio;
    private bool AudioTrigger = false;
    private Rigidbody rb;

    void Start()
    {
        CurrentHealth = 100;
        anim = GetComponent<Animator>();
        Audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
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
            rb.constraints = RigidbodyConstraints.FreezePosition;
            rb.isKinematic = true;

            if (!AudioTrigger)
            {
                Audio.PlayOneShot(deathSFX, 0.1f);
                AudioTrigger = true;
            }
            //Destroy(gameObject);
           
        }
    }






}
