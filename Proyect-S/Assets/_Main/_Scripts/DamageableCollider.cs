using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableCollider : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private LayerMask _hittableMask;
    public AudioClip wiff;
    public AudioClip backstab;
    private AudioSource _audio;
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
        if ((_hittableMask & 1 << other.gameObject.layer) != 0)
        {
           
            IDamageable damageable = other.GetComponent<LifeController>();
          
            if (damageable != null)
            {
                _audio.PlayOneShot(backstab, 0.7f);
                damageable.TakeDamage(damage);
                
            }
        }
   
    }
}
