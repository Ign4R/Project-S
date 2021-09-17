using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //---------------TRANSFORM TARGET--------------------------///
    [SerializeField] private Transform target;

    ///-------------------TIMER---------------------------------///
    [SerializeField] private float timerToShootSet;
    [SerializeField] private float timerBetweenShootSet;

    ///----------------ENEMY COMPONENTS-------------------------///
    private Enemy enemy;
    private EnemyAI enemyAI;

    ///---------------------STATS-------------------------------///
    [SerializeField] private float speed;
    ///---------------------****------------------------------///
    private bool shootState;
    private float timerBetweenShoot;
    private float timerToShoot;

    private void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
        enemy = GetComponent<Enemy>();
    }
    void Update()
    {
        if (enemyAI.IsInSight(target) && enemy.CurrentHealth > 0)
        {
            Debug.Log($"IsInSight is {enemyAI.IsInSight(target)}");
            enemy.anim.SetBool("Walk", true);
            ///new method
            Vector3 dir = target.position - transform.position;
            dir.y = 0;
            transform.rotation = Quaternion.LookRotation(dir);
            transform.position += transform.forward * speed * Time.deltaTime;

            #region timer
            if (timerBetweenShoot > 0)
            {
                timerBetweenShoot -= Time.deltaTime;
            }
            else
            {

                ///animacion
                if (!shootState)
                {
                    timerToShoot = timerToShootSet;
                    shootState = true;
                }
                if (timerToShoot <= 0)
                {
                    enemyAI.Shoot();
                    //Debug.Log("Disparo");
                    timerBetweenShoot = timerBetweenShootSet;
                    shootState = false;
                }
            }
            if (timerToShoot <= 0 && enemy.CurrentHealth > 0)
            {

            }
            #endregion
        }
        else if(!enemyAI.IsInSight(target))
        {
            enemy.anim.SetBool("Walk", false);
        }
        if (timerToShoot > 0)
        {
            timerToShoot -= Time.deltaTime;
        }

    }
}
