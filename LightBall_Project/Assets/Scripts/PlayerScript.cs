using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : Entity {

    // attributes
    GameObject orb;
    Vector3 goalRot;

    [Range(1.0f, 50.0f)]
    public float chargeMultiplier;
    public float chargeMax = 2f;

    float maxVelDef;
    float chargeTimer;
    
    // Use this for initialization
    void Start ()
    {
        orb = GameObject.FindGameObjectWithTag("Orb");
		pos = transform.position;
		vel = Vector3.zero;
        maxVelDef = maxVel;
		acc = Vector3.zero;
		Physics2D.IgnoreCollision(GetComponent<Collider2D>(), orb.GetComponent<Collider2D>(), true);
		Physics2D.IgnoreCollision(transform.GetChild(0).gameObject.GetComponent<Collider2D>(), orb.GetComponent<Collider2D>(), true);
		Physics2D.IgnoreCollision(GetComponent<Collider2D>(), transform.GetChild(0).gameObject.GetComponent<Collider2D>(), true);
        goalRot = Vector3.zero;
		GetComponent<Rigidbody2D>().freezeRotation = true;
        chargeTimer = 1.0f;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		MoveUpdate();	
	}

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            chargeTimer += chargeMultiplier * Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space) || chargeTimer > chargeMax)
        {
            if (orb.GetComponent<OrbScript>().isHeld)
            {
                orb.GetComponent<OrbScript>().ThrowOrb(Mathf.Clamp(chargeTimer, 1.0f, chargeMax));
                Debug.Log("chargeTimer: " + chargeTimer);
                chargeTimer = 1.0f;
            }
        }

        //if (!orb.GetComponent<OrbScript>().isHeld)
        //{
        //    if (Input.GetKey(KeyCode.LeftShift))
        //        maxVel = maxVelDef * 1.5f;
        //    else
        //        maxVel = maxVelDef;
        //}
        //else
        //    maxVel = maxVelDef;

    }

    void MoveUpdate()
	{
		float x = Input.GetAxisRaw("Horizontal");
		float y = Input.GetAxisRaw("Vertical");

		// Movement
		vel.x += x * force;
		vel.y += y * force;

		if (x == 0)
			vel.x *= .5f;

		if (y == 0)
			vel.y *= .5f;

        if (vel.magnitude < 0.1f)
            vel = Vector3.zero;

		UpdatePosition();

        if (x != 0.0f || y != 0.0f)
        {
            if (Vector3.Dot(transform.up, new Vector3(x, y, 0)) < -.9f)
            {
                transform.up = Vector3.Lerp(transform.up, (new Vector3(x, y, 0f).normalized + transform.right).normalized, .3f);
            }
            else if ((transform.up - new Vector3(x, y, 0f).normalized).magnitude < .09f)
            {
                transform.up = new Vector3(x, y, 0f).normalized;
            }
            else
            {
                transform.up = Vector3.Lerp(transform.up, new Vector3(x, y, 0f).normalized, .3f);
            }
        }
    }
}