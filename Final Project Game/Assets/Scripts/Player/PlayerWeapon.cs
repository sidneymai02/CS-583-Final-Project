using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{

    public int gunDamage = 1; // damage our weapon will do to enemies
    public float fireRate = .25f; // time between each shot
    public float weaponRange = 50f; // how far our raycast will go
    public float hitForce = 100f;
    public Transform Emitter; // muzzle point of raycast
    
    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f); // how long our bullet stays active
    private AudioSource gunAudio; // reference to our shooting sound
    private LineRenderer laserLine; // draw line between 2 points (gun,object)
    private float nextFire; // when player is allowed to shoot again

    void Start()
    {
        laserLine = GetComponent<LineRenderer> (); // store reference to line render in laserLine
        gunAudio = GetComponent<AudioSource> (); // store reference to audio source in gunAudio
        fpsCam = GetComponentInParent<Camera> (); // search game object attached to it and store the type of object encountered
    }

    
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && Time.time > nextFire) // check for user input and time between shot requirement is met
        {
            nextFire = Time.time + fireRate;
            StartCoroutine(ShotEffect());
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint (new Vector3(0.5f, 0.5f, 0.0f)); // center our ray relative to the camera 
            RaycastHit hit; // returns info of object collider our ray hit
            laserLine.SetPosition(0,Emitter.position);

            if(Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange)) // here, we determine what our ray will do if contact w gameObj is made, or if it misses completely
            {
                laserLine.SetPosition(1, hit.point);
                Enemy health = hit.collider.GetComponent<Enemy>();

                if (health != null) {
                    health.Damage(gunDamage);
                }
            }
            else {
                laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
            }
        }
    }
    private IEnumerator ShotEffect() {
    
        laserLine.enabled = true; // when to turn off our raycast line
        yield return shotDuration;
        laserLine.enabled = false;
    }
}