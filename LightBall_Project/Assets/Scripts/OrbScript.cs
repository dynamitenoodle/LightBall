using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbScript : Entity {

    // attributes
    GameObject player;
    Vector3 heldPosition, thrownPosition;
    public bool isHeld;
    
    bool canPickup;
    int pickupTimer, pickupTimerMax;
    Collider2D orbCol, orbCatchCol, playerCol;
    public bool Damage
    {
        get { return vel.magnitude > .06f; }
    }
	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
		heldPosition = player.transform.GetChild(0).gameObject.transform.position;
		transform.position = heldPosition = new Vector3(heldPosition.x, heldPosition.y, transform.position.z); // sets the position of the orb and the held position with correct z positions

		// setting colliders
		orbCol = GetComponent<Collider2D>();
		playerCol = player.GetComponent<Collider2D>();
		orbCatchCol = player.transform.GetChild(0).gameObject.GetComponent<Collider2D>();

		// setting other variables to have default states
		pos = transform.position;
		vel = Vector3.zero;
		acc = Vector3.zero;
		isHeld = true;
		canPickup = true;
		pickupTimer = 0;
		pickupTimerMax = 60;
		thrownPosition = Vector3.zero;

	}

	// Update is called once per frame
	void FixedUpdate ()
    {
        // Check if the player is holding the orb
        if (CheckHold())
        {
            if (vel != Vector3.zero)
                vel = Vector3.zero;
            if (acc != Vector3.zero)
                acc = Vector3.zero;
            transform.position = heldPosition;
        }
        // it isn't so slow down
        else if (vel.magnitude < .01f)
        {
            vel = Vector3.zero;
        }
        else
        {
            if(pickupTimer/pickupTimerMax<.5f)
            vel *= .97f;
        }
        // If the orb can't be picked up yet
        if (Damage)
        {
            if (GetComponent<SpriteRenderer>().color != Color.red)
                GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (GetComponent<SpriteRenderer>().color != Color.yellow)
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;

        }
        if (!canPickup)
		{

			if (vel.magnitude <.03f)
			{
				canPickup = true;
				pickupTimer = 0;
			}
			pickupTimer++;
		}
        else if (GetComponent<SpriteRenderer>().color != Color.green)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }

        UpdatePosition();
	}

	// Checks if we are holding the ball and if we should pick it up
	bool CheckHold()
	{
		// update where the ball should be
		heldPosition = player.transform.GetChild(0).gameObject.transform.position;
		heldPosition = new Vector3(heldPosition.x, heldPosition.y, transform.position.z);

		// check if we should be or are being held
		if (isHeld)
			return true;

		else
		{
			if (canPickup && !Input.GetKey(KeyCode.Space))
			{
				if (playerCol.Distance(orbCol).distance < 0 || orbCatchCol.Distance(orbCol).distance < 0)
				{
					isHeld = true;
					pickupTimer = 0;
					return true;
				}
			}
		}

		return false;
	}

    
    // tosses the orb
    public void ThrowOrb(float f_m = 1.0f)
	{
		isHeld = false;
		canPickup = false;
        pickupTimerMax = (int)(60f * f_m);
		Vector3 direction;
		thrownPosition = player.transform.position;
		direction = player.transform.GetChild(0).gameObject.transform.position - player.transform.position;
		direction.z = 0;
		direction *= (force) * f_m;
		acc = direction;
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        // super temporary collision check
        if (col.gameObject.tag == "Wall")
        {

			Vector3 hit = col.contacts[0].normal;
			float angle = Vector3.Angle(hit, Vector3.up);

			if (Mathf.Approximately(angle, 0) || Mathf.Approximately(angle, 180))
			{
				//Down and Up
				vel.y = -vel.y;
				acc.y = -acc.y;
			}
			if (Mathf.Approximately(angle, 90))
			{
				// Sides
				vel.x = -vel.x;
				acc.x = -acc.x;
				/*
				Vector3 cross = Vector3.Cross(Vector3.forward, hit);
				if (cross.y > 0)
				{ // left side of the player
					Debug.Log("Left");
				}
				else
				{ // right side of the player
					Debug.Log("Right");
				}
				*/
			}

		}
    }
}