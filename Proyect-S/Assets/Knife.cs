using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField] private LayerMask _rotateMask;
    [SerializeField] private LayerMask _hittableMask;
    [SerializeField] private int damage;
    bool canRotate;
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        canRotate = true;
    }
    void Update()
    {
        print(canRotate);
        if (canRotate)
            transform.Rotate(Vector3.right, 500 * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
       
        if ((_hittableMask & 1 << collision.gameObject.layer) != 0)
        {

            IDamageable damageable = collision.collider.GetComponent<LifeController>();

            if (damageable != null)
            {
                damageable.TakeDamage(damage);

            }
        }
   
        if(!collision.gameObject.CompareTag("Damageable"))
        canRotate = false;
        if ((_rotateMask & 1 << collision.gameObject.layer) != 0)
            canRotate = true;
        else
        {
            rb.velocity = Vector3.zero;
        }

    }

}
