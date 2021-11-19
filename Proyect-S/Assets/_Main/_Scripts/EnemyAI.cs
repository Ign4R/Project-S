using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float rangeAlert = 10;
    [SerializeField] private int damage;
    [SerializeField] private float shootY;
    [SerializeField] private float range = 10;
    [SerializeField] private float minimalRange = 5;
    [SerializeField] private float angle = 90;
    private Vector3 shootPosition;    
    public LayerMask mask;
    public LayerMask targetMask;
    public LayerMask maskAlert;
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

    public void Alert(Transform transform, Collider collider)
    {

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, rangeAlert, maskAlert);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i] == collider) continue;
            Collider enemies = hitColliders[i];
            enemies.GetComponent<EnemyController>().Chase = true;           
            enemies.GetComponent<EnemyController>().OverlapLock = true;           
        }       
    }


    public bool IsInMinimalDistance(Transform target)
    {
        Vector3 diff = target.position - transform.position;
        float distance = diff.magnitude;
        if (distance > minimalRange) return false;
        
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
        Gizmos.DrawWireSphere(transform.position, minimalRange);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(shootPosition, transform.forward * range);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, angle / 2, 0) * transform.forward * range);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -angle / 2, 0) * transform.forward * range);

    }

    public void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(shootPosition, transform.forward, out hit, range))
        {
            if ((targetMask & 1 << hit.collider.gameObject.layer) != 0)
            {
                hit.collider.GetComponent<LifeController>().TakeDamage(damage);
            }
        }
    }
}