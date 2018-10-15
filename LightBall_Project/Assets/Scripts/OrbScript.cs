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
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        
        // Check if the player is holding the orb
        if (CheckHold())
            transform.position = heldPosition;
        // it isn't so slow down
        else if (vel.magnitude < .01f)
        {
            vel = Vector3.zero;
        }
        else
            vel *= .9f;

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

			if (pickupTimer > pickupTimerMax)
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
			if (canPickup)
			{
				if (playerCol.Distance(orbCol).distance < 0 || orbCatchCol.Distance(orbCol).distance < 0)
				{
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

		Vector3 direction;
		direction = player.transform.GetChild(0).gameObject.transform.position - player.transform.position;
		direction.z = 0;

		direction *= (force);
		acc = direction;
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        // super temporary collision check
        if (col.gameObject.tag == "Wall")
        {
            RaycastHit2D hitInfoUp = Physics2D.Raycast(transform.position, new Vector2(0, 1));
            RaycastHit2D hitInfoDown = Physics2D.Raycast(transform.position, new Vector2(0, -1));
            RaycastHit2D hitInfoLeft = Physics2D.Raycast(transform.position, new Vector2(-1, 0));
            RaycastHit2D hitInfoRight = Physics2D.Raycast(transform.position, new Vector2(1, 0));

            //Debug.Log(hitInfoUp.distance + " " + hitInfoDown.distance + " " + hitInfoLeft.distance + " " + hitInfoRight.distance);
            if ((hitInfoUp.distance > hitInfoDown.distance && hitInfoUp.distance > hitInfoLeft.distance && hitInfoUp.distance > hitInfoRight.distance) 
                || hitInfoDown.distance > hitInfoLeft.distance && hitInfoDown.distance > hitInfoRight.distance)
            {
                acc.y = -acc.y;
                vel.y = -vel.y;
            }

            else
            {
                acc.x = -acc.x;
                vel.x = -vel.x;
            }

            //UpdatePosition();
        }
    }
}
