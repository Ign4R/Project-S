using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraplingRope : MonoBehaviour
{
    private LineRenderer hookLine;
    private Spring spring;
    private Vector3 currentGrapplePosition;
    private Vector3 grapplePoint;


    [SerializeField] private GrapplingHook grapplingHook;
    [SerializeField] private GameObject hookTip;
    [SerializeField] private int quality;
    [SerializeField] private float damper;
    [SerializeField] private float stregth;
    [SerializeField] private float velocity;
    [SerializeField] private float waveCount;
    [SerializeField] private float waveHeight;
    [SerializeField] private AnimationCurve affectCurve;

    private void Awake()
    {
        hookLine = GetComponent<LineRenderer>();
        spring = new Spring();
        spring.SetTarget(0);
    }

    private void LateUpdate()
    {
        DrawRope();

    }

    void DrawRope()
    {
        if (grapplingHook.Check== false)
        {
            grapplePoint = grapplingHook.transform.position;
            spring.Reset();
            if (hookLine.positionCount > 0)
            {
                hookLine.positionCount = 0;
                
            }
            return;

        }

        if(hookLine.positionCount == 0)
        {
            spring.SetVelocity(velocity);
            hookLine.positionCount = quality + 1;
        }

        spring.SetDamper(damper);
        spring.SetStrength(stregth);
        spring.Update(Time.deltaTime);

        grapplePoint = grapplingHook.GetGrapplePoint();
        var grappleOut = hookTip.transform.position;
        var up = Quaternion.LookRotation(grapplePoint - grappleOut.normalized) * Vector3.up;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 12f);

        for (int i = 0; i < quality +1; i++)
        {
            var delta = i / (float) quality;
            var offset = up * waveHeight * Mathf.Sin(delta * waveCount * Mathf.PI * spring.Value * affectCurve.Evaluate(delta));
            hookLine.SetPosition(i, Vector3.Lerp(grappleOut, currentGrapplePosition, delta) + offset);
        }

    }
}
