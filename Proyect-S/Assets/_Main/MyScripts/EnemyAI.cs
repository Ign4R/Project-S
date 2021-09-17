using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float range = 10;
    public float angle = 90;
    public LayerMask mask;
    public LayerMask targetMask;
    //Implementar
    private void Start()
    {

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
        Gizmos.DrawRay(transform.position, transform.forward * range);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, angle / 2, 0) * transform.forward * range);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -angle / 2, 0) * transform.forward * range);

    }

    public void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {

            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log(hit.transform.name);
            }
        }
    }
}