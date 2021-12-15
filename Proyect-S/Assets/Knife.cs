using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField] private LayerMask _hittableMask;
    bool canRotate;
    private void Start()
    {
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
        print(collision.gameObject.name);
        if(!collision.gameObject.CompareTag("Damageable"))
        canRotate = false;
        else
        {
            if ((_hittableMask & 1 << collision.gameObject.layer) != 0)
                canRotate = true;
        }
    }
}
