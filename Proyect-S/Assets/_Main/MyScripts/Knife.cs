using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField]
    Animator _animator;
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
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Aprete click derecho");
            _animator.SetTrigger("Attack");
        }

        if (Input.GetMouseButtonUp(0))
        {
            _animator.SetTrigger("Attack");
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            IDamageable enemy = other.GetComponent<LifeController>();
            if (enemy != null)
            {
                enemy.TakeDamage(100);
                _audio.PlayOneShot(backstab, 0.7f);
                particle.GetComponent<ParticleSystem>().Play();
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
