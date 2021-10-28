using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    private LineRenderer hookLine;
    private Vector3 grapplePoint;
    private float currentDistance;

    [SerializeField] private float maxDistance = 40f;  
    [SerializeField] private float distanceRange;
    [SerializeField] private float hookSpeed;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float cooldown;

    private float currentCooldown;
    private Rigidbody rb;
    private bool check;

    void Awake()
    {
        hookLine = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody>();
        playerTransform = GetComponent<Transform>();
    }

    void Update()
    {
        if (currentCooldown >= 0)
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z) && currentCooldown <= 0) 
        {
            currentCooldown = cooldown;
            StartGrapple();
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            Physics.gravity = new Vector3(0f, -9.81f, 0f);
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            StopGrapple();

        }
        if (check == true)
        {
            currentDistance = Vector3.Distance(playerTransform.position, grapplePoint);
            if(currentDistance > distanceRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, grapplePoint, hookSpeed * Time.deltaTime);
            }
            else
            {
                rb.constraints = RigidbodyConstraints.FreezePosition;
                
            }

        }
        DrawRope();
    }

    void StartGrapple()
    {

        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, maxDistance))
        {
            rb.velocity = Vector3.zero;
            check = true;
            grapplePoint = hit.point;
            hookLine.positionCount = 2;
            Physics.gravity = Vector3.zero;
        }

    }


    void StopGrapple()
    {
        hookLine.positionCount = 0;
        check = false;

    }

    void DrawRope()
    {
        if (check == false) return;
        hookLine.SetPosition(index: 0, playerTransform.position);
        hookLine.SetPosition(index: 1, grapplePoint);

    }
}
