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
    private LifeController lifeController;
    private EnemyAI enemyAI;
    private Animator anim;

    ///---------------------STATS-------------------------------///
    [SerializeField] private float speed;
    ///
    ///---------------------BOOLS------------------------------///
    private bool shootState;
    private bool AudioTrigger = false;
    private bool chase;
    /// 
    private float timerBetweenShoot;
    private float timerToShoot;

    public AudioClip alertSFX;
    private AudioSource Audio;

    private void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();

        Audio = GetComponent<AudioSource>();

        lifeController = GetComponent<LifeController>();

        anim = GetComponent<Animator>();
    }
    void Update()
    {

        if (enemyAI.IsInSight(target) && lifeController.CurrentHealth > 0 || chase && lifeController.CurrentHealth > 0)
        {
            chase = true;       
            //Debug.Log($"IsInSight is {enemyAI.IsInSight(target)} from {gameObject.name}");

            if (!AudioTrigger)
            {
                Audio.PlayOneShot(alertSFX, 0.3f);
                AudioTrigger = true;
            }

            if (!shootState && !enemyAI.IsInMinimalDistance(target))
            {
                //Debug.Log($"IsInSight is {enemyAI.IsInSight(target)} from {gameObject.name}");
                Vector3 dir = target.position - transform.position;
                dir.y = 0;
                anim.SetBool("Walk", true);
                transform.rotation = Quaternion.LookRotation(dir);
                transform.position += transform.forward * speed * Time.deltaTime;
            }
            else
            {
                anim.SetBool("Walk", false);
            }

            #region Timer
            if (timerBetweenShoot > 0)
            {
                timerBetweenShoot -= Time.deltaTime;
            }
            else
            {

                ///animacion
                if (!shootState && enemyAI.IsInSight(target))
                {

                    anim.SetTrigger("Shoot");
                    timerToShoot = timerToShootSet;
                    shootState = true;

                }
                if (timerToShoot <= 0)
                {
                   
                    timerBetweenShoot = timerBetweenShootSet;
                    shootState = false;
                }
            }
            #endregion
        }
        else if (!enemyAI.IsInSight(target))
        {
           anim.SetBool("Walk", false);
           shootState = false;
        }
        if (timerToShoot > 0)
        {
            timerToShoot -= Time.deltaTime;
        }
        anim.SetBool("Aim", enemyAI.IsInMinimalDistance(target));
    }

}
