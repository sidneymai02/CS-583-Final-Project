 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 public class PlayerGrapple : MonoBehaviour
 {
    [Header("References")]
    private PlayerMovement pm;
    public Transform cam;
    public Transform grappleTip;
    public LayerMask grappleable;
    public LineRenderer lr;

    [Header("Grappling")]
    public float maxGrappleDistance;
    public float grappleDelayTime;
    public float overshootYAxis;

    private Vector3 grapplePoint;

    [Header("Cooldown")]
    public float grapplingCd;
    private float grapplingCdTimer;

    [Header("Input")]
    public KeyCode grappleKey = KeyCode.Mouse1;

    private bool grappling;

    private void Start()
    {
        pm = GetComponent<PlayerMovement>();
    }

    private void Update() 
    {
        if (Input.GetKeyDown(grappleKey)) {
            StartGrapple();
        }
        if (grapplingCdTimer > 0) {
            grapplingCdTimer -= Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        if(grappling) {
            lr.SetPosition(0, grappleTip.position); 
        }
    }

    private void StartGrapple()
    {
        if(grapplingCdTimer > 0) {
            return;
        }

        grappling = true;

        pm.freeze = true;

        RaycastHit hit;
        if(Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, grappleable))
        {
            grapplePoint = hit.point;

            Invoke(nameof(ExecuteGrapple), grappleDelayTime);
        }

        else 
        {
            grapplePoint = cam.position + cam.forward * maxGrappleDistance;

            Invoke(nameof(StopGrapple), grappleDelayTime);
        }

        lr.enabled = true;
        lr.SetPosition(1, grapplePoint);

    }

    private void ExecuteGrapple()
    {
        pm.freeze = false;

        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointRelativeYPos = grapplePoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis;

        if (grapplePointRelativeYPos < 0) highestPointOnArc = overshootYAxis;

        pm.JumpToPosition(grapplePoint, highestPointOnArc);

        Invoke(nameof(StopGrapple), 1f);
    }

    public void StopGrapple() 
    {
        pm.freeze = false;
        grappling = false;
        grapplingCdTimer = grapplingCd;
        lr.enabled = false;


    }


 
 }