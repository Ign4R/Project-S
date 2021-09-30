using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    private LineRenderer hookLine;
    private Vector3 hookDir;
    private Vector3 grapplePoint;
    private float distanceFromPoint;
    [SerializeField]
    private float maxDistance = 40f;
    private float currentDistance;
    public float distanceRange;
    public float hookSpeed;
    public LayerMask toGrapple;
    public Transform hook;
    public Transform cameraTransform;
    public Transform player;
    private Rigidbody rb;

    public bool check;
    public float force;
    public float forceMultiplier;
    public float velocityForce;
    private float distanceDiference;

    void Awake()
    {
        hookLine = GetComponent<LineRenderer>();
        rb = player.GetComponent<Rigidbody>();
    }

    void Update()
    {

    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            rb.velocity = Vector3.zero;
            
            StartGrapple();
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            Physics.gravity = new Vector3(0f, -9.81f, 0f);
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            StopGrapple();

        }
        if (check == true)
        {
            currentDistance = Vector3.Distance(player.position, grapplePoint);
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
            check = true;
            grapplePoint = hit.point;
            hookDir = (grapplePoint - transform.position).normalized;
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
        hookLine.SetPosition(index: 0, hook.position);
        hookLine.SetPosition(index: 1, grapplePoint);


    }
}
