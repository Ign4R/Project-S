using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrapplingHook : MonoBehaviour
{
    //private LineRenderer hookLine;
    private Vector3 grapplePoint;
    private float currentDistance;
    private float currentCooldown = 0;
    private Rigidbody rb;
    private bool check;
    private bool onClick;
    private float hookDistance;
    private float hookSpeed;
    private float middleDistance;
    private float closeDistance;

    
    [SerializeField] private float farSpeed;
    [SerializeField] private float middleSpeed;
    [SerializeField] private float closeSpeed;    
    [SerializeField] private Image hitMark;
    [SerializeField] private Image cooldownImage;
    [SerializeField] private float maxDistance = 40f;
    [SerializeField] private float hookMinRange;
    [SerializeField] private float distanceRange;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float cooldown;

    public bool Check => this.check;

    void Awake()
    {
        //hookLine = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody>();
        playerTransform = GetComponent<Transform>();
    }
    private void Start()
    {
        hookSpeed = farSpeed;
    }
    void Update()
    {
        cooldownImage.fillAmount = currentCooldown / cooldown;

        if (currentCooldown < cooldown && onClick == false )
        {
            currentCooldown += Time.deltaTime;
        }
        if(currentCooldown > cooldown)
        {
            currentCooldown = cooldown;
        }

    }

    private void LateUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, maxDistance) && hit.distance > hookMinRange)
        {
            hitMark.color = Color.green;
        }
        else
        {
            hitMark.color = Color.red;
        }

        if (Input.GetMouseButtonDown(1) && currentCooldown == cooldown) 
        {
            onClick = true;
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            onClick = false;
            Physics.gravity = new Vector3(0f, -9.81f, 0f);
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            StopGrapple();

        }
        if (check == true)
        {
            currentDistance = Vector3.Distance(playerTransform.position, grapplePoint);
            middleDistance = hookDistance / 2;
            closeDistance = hookDistance / 5;

            if (currentDistance > distanceRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, grapplePoint, hookSpeed * Time.deltaTime);
            }
            if(currentDistance <= middleDistance && currentDistance > closeDistance)
            {
                hookSpeed = middleSpeed;
            }
            if(currentDistance <= closeDistance)
            {
                hookSpeed = closeSpeed;
            }
            else if(currentDistance <= distanceRange)
            {
                rb.constraints = RigidbodyConstraints.FreezePosition;
                
            }

        }
        //DrawRope();
    }

    void StartGrapple()
    {

        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, maxDistance))
        {
            if (hit.distance < hookMinRange)
            {
                StopGrapple();
                return;
            }

            currentCooldown = 0;
            rb.velocity = Vector3.zero;
            check = true;
            grapplePoint = hit.point;
            //hookLine.positionCount = 2;
            hookDistance = Vector3.Distance(playerTransform.position, grapplePoint);
            Physics.gravity = Vector3.zero;
        }

    }

    public Vector3 GetGrapplePoint()
    {
        if (grapplePoint == null)
            return Vector3.zero;
        else
            return grapplePoint;
    }

    void StopGrapple()
    {
        //hookLine.positionCount = 0;
        check = false;
        hookSpeed = farSpeed;

    }

    //void DrawRope()
    //{
    //    if (check == false) return;
    //    hookLine.SetPosition(index: 0, playerTransform.position);
    //    hookLine.SetPosition(index: 1, grapplePoint);

    //}
}
