using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : Entity {

    // attributes
    GameObject orb;
    Vector3 goalRot;
    bool pushed;
    
    public float chargeMultiplier;
    public float chargeMax = 1f;

    float maxVelDef;
    float chargeTimer;
    
    // Use this for initialization
    void Start ()
    {
        pushed = false;
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
        chargeTimer = 0.0f;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		MoveUpdate();	
	}

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space)&&orb.GetComponent<OrbScript>().isHeld)
        {
            chargeTimer += chargeMultiplier * Time.deltaTime;
            pushed = true;
        }

        if (Input.GetKeyUp(KeyCode.Space)&&pushed || chargeTimer > chargeMax)
            if (orb.GetComponent<OrbScript>().isHeld)
            {
                float t = Mathf.Clamp(chargeTimer, .5f, chargeMax);
                orb.GetComponent<OrbScript>().ThrowOrb(t);
                Debug.Log("chargeTimer: " + chargeTimer);
                chargeTimer = 0.0f;
                pushed = false;
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
        float sprintMult = 0;
        // Movement
        if (Input.GetKey(KeyCode.LeftShift))
        {
            sprintMult = 1.5f;
            maxVel = 6;
        }
        else
        {
            sprintMult = 1f;
            maxVel = 3;
        }
        vel.x += x * force * sprintMult;
		vel.y += y * force * sprintMult;

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