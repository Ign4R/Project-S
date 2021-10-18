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
    private Collider coll;
    void Start()
    {
        CurrentHealth = 100;
        coll = GetComponent<Collider>();
        Audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
    }
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            coll.enabled = false;
            CurrentHealth = 0;
            anim.SetTrigger("Death");
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
