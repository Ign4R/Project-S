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
    [SerializeField] private GameObject visionCone;
    private LifeController lifeController;
    private EnemyAI enemyAI;
    private Animator anim;
    private Collider coll;
    private Rigidbody rb;
    public AudioClip alertSFX;
    private AudioSource Audio;
    public AudioClip deathSFX;
    public AudioClip attackSFX;
    private ParticleSystem ShotParticles;

    ///---------------------STATS-------------------------------///
    [SerializeField] private float speed;
    ///
    ///---------------------BOOLS------------------------------///
    private bool shootState;
    private bool AudioTrigger = false;
    [SerializeField] private bool chase;
    [SerializeField] private bool overlapLock;
    public bool Chase { get => chase; set => chase = value; }
    public bool OverlapLock { get => overlapLock; set => overlapLock = value; }

    /// 
    private float timerBetweenShoot;
    private float timerToShoot;

 

    private void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();

        Audio = GetComponent<AudioSource>();
        ShotParticles = GetComponentInChildren<ParticleSystem>();

        lifeController = GetComponent<LifeController>();
        lifeController.OnDeath += OnDeath;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        coll= GetComponent<Collider>();
    }
    private void Start()
    {
        
    }
    void Update()
    {
        if (lifeController.CurrentHealth <= 0)
        {
            print("death enemy");
            anim.SetTrigger("Death");
            rb.isKinematic = true;
            coll.enabled = false;
            visionCone.SetActive(false);

        }

        if (Chase)
        {
            Vector3 dir = target.position - transform.position;
            dir.y = 0;
            transform.rotation = Quaternion.LookRotation(dir);
            anim.SetBool("Walk", true);
            transform.position += transform.forward * speed * Time.deltaTime;

        }
        else
        {
            anim.SetBool("Walk", false);
        }

        if (enemyAI.IsInSight(target) && lifeController.CurrentHealth > 0 )
        {
            visionCone.SetActive(false);
            Chase = false;
            if (!OverlapLock)
            {
                enemyAI.Alert(transform, coll);
                overlapLock = true;
            }


            if (!AudioTrigger)
            {
                Audio.PlayOneShot(alertSFX, 0.3f);
                AudioTrigger = true;
            }

            if (!shootState)
            {
                Vector3 dir = target.position - transform.position;
                dir.y = 0;
                transform.rotation = Quaternion.LookRotation(dir);

                if (!enemyAI.IsInMinimalDistance(target))
                {
                    
                    anim.SetBool("Walk", true);
                    transform.position += transform.forward * speed * Time.deltaTime;
                }
                else
                {
                    anim.SetBool("Walk", false);
                }
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

                if (!shootState && enemyAI.IsInSight(target))
                {
                    anim.SetTrigger("Shoot");
                    timerToShoot = timerToShootSet;
                    shootState = true;
                    if(ShotParticles != null)
                    {
                        ShotParticles.Play();
                    }

                    if (attackSFX != null)
                        Audio.PlayOneShot(attackSFX, 0.2f);
                }
                if (timerToShoot <= 0)
                {

                    timerBetweenShoot = timerBetweenShootSet;
                    shootState = false;
                }
            }
            #endregion
        }
        else if (!enemyAI.IsInSight(target) && !Chase && lifeController.CurrentHealth > 0)
        {
            visionCone.SetActive(true);
            anim.SetBool("Walk", false);
            shootState = false;
        }
        if (timerToShoot > 0)
        {
            timerToShoot -= Time.deltaTime;
        }

    }

    private void OnDeath()
    {
        Audio.PlayOneShot(deathSFX, 0.2f);
        AudioTrigger = true;
        //if (!AudioTrigger)
        //{
           
          

        //}
    }
}
