using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float shootY;
    private Vector3 shootPosition;
    public float range = 10;
    public float angle = 90;
    public LayerMask mask;
    public LayerMask targetMask;
    //Implementar
    private void Start()
    {

    }
    private void Update()
    {
        shootPosition = new Vector3(transform.position.x, shootY, transform.position.z);
    }
    public bool IsInSight(Transform target)
    {
        Vector3 diff = target.position - transform.position;
        float distance = diff.magnitude;
        if (distance > range) return false;
        Vector3 front = transform.forward;
        if (!InAngle(diff, front)) return false;
        if (!IsInView(diff.normalized, distance, mask)) return false;

        return true;
    }

    bool InAngle(Vector3 from, Vector3 to)
    {
        float angleToTarget = Vector3.Angle(from, to);
        return angleToTarget < angle / 2;
    }

    bool IsInView(Vector3 dirToTarget, float distance, LayerMask mask)
    {
        return !(Physics.Raycast(transform.position, dirToTarget, distance, mask));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(shootPosition, transform.forward * range);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, angle / 2, 0) * transform.forward * range);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -angle / 2, 0) * transform.forward * range);

    }

    public void Shoot()
    {
        Debug.Log("Disparo");
        RaycastHit hit;

        if (Physics.Raycast(shootPosition, transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log(hit.transform.name);
                hit.collider.GetComponent<LifeController>().TakeDamage(damage);
            }
        }
    }
}