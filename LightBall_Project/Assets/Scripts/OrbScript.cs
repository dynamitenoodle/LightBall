using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbScript : Entity {

    // attributes
    GameObject player;
	Vector3 heldPosition;
	public bool isHeld;
	bool canPickup;
	int pickupTimer, pickupTimerMax;
	Collider2D orbCol, orbCatchCol, playerCol;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
		heldPosition = transform.position = player.transform.GetChild(0).gameObject.transform.position;

		orbCol = GetComponent<Collider2D>();
		playerCol = player.GetComponent<Collider2D>();
		orbCatchCol = player.transform.GetChild(0).gameObject.GetComponent<Collider2D>();

		pos = transform.position;
		vel = Vector3.zero;
		acc = Vector3.zero;
		isHeld = true;
		canPickup = true;
		pickupTimer = 0;
		pickupTimerMax = 120;

	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		if (CheckHold())
			pos = heldPosition;

		if (!canPickup)
		{
			if (pickupTimer > pickupTimerMax)
			{
				canPickup = true;
				pickupTimer = 0;
			}
			pickupTimer++;
		}

		UpdatePosition();
	}

	// Checks if we are holding the ball and if we should pick it up
	bool CheckHold()
	{
		// update where the ball should be
		heldPosition = player.transform.GetChild(0).gameObject.transform.position;

		// check if we should be or are being held
		if (isHeld)
			return true;

		else
		{
			if (canPickup)
			{
				if (playerCol.Distance(orbCol).distance < 0 || orbCatchCol.Distance(orbCol).distance < 0)
				{
					Debug.Log("PICK IT UP KID");
					isHeld = true;
					return true;
				}
			}
		}

		return false;
	}

	// tosses the orb
	public void ThrowOrb()
	{
		isHeld = false;
		canPickup = false;

		Debug.Log(player.transform.forward);
	}
}
