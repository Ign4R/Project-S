using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableCollider : MonoBehaviour
{
    [SerializeField] private int damage;
    public AudioClip wiff;
    public AudioClip backstab;
    private AudioSource _audio;
    public GameObject particle;
    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Damageable")
        {
            print("hize daño");
            IDamageable damageable = other.GetComponent<LifeController>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
                _audio.PlayOneShot(backstab, 0.7f);
                //particle.GetComponent<ParticleSystem>().Play();
            }
            else
            {
                _audio.PlayOneShot(wiff, 0.7f);
            }
        }
        else
        {
            _audio.PlayOneShot(wiff, 0.7f);
        }
    }

}
