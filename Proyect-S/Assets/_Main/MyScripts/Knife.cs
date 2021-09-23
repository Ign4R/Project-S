using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField]
    Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
      

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

            }

        }
    }

}
