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
            EnemyController enemyController = other.GetComponent<EnemyController>();

            if (damageable != null)
            {

                _audio.PlayOneShot(backstab, 0.7f);
                if (!enemyController.IsInVision)
                {
                    print("Instakill");
                    damageable.TakeDamage(damage);
                }
                else
                {
                    print("NO Instakill");
                    damageable.TakeDamage(50);
                }
            }

        }
   
    }
}
