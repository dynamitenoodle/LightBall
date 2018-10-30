using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : EnemyScript {

	// attributes
	RaycastHit2D hitInfo;

	// attack timer variables
	public float attackTimerMax = 180;
    float attackTimer;
    public float attackDelay = 90;
    public Vector2 attackPosition;
    bool attackSet, attackAnimation;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GetComponent<Rigidbody2D>().freezeRotation = true;
        attackPosition = Vector3.zero;
        attackSet = false;
		attackAnimation = false;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		// If we are attacking
		if (attackAnimation)
		{
			Vector2 direction = attackPosition.normalized;
			acc = direction * force;

			//Debug.Log(Vector2.Distance(attackPosition, transform.position) + " " + attackTimer);
			//Debug.Log(hitInfo.transform.gameObject + " " + hitInfo.distance);

			if (hitInfo.collider.Distance(GetComponent<Collider2D>()).distance < 0.5f)
			{
				acc = Vector3.zero;
				vel = Vector3.zero;
				pos = Vector3.zero;
				GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				attackAnimation = false;
				attackPosition = Vector2.zero;
				hitInfo = new RaycastHit2D();
				GetComponent<SpriteRenderer>().color = Color.white;
			}
			
			UpdatePosition();
		}

		// If we are preparing to attack
		else
		{
			// Setting the rotation to the player and increasing the color of reddness
			if (!attackSet)
			{
				SetLook(player.position);

				// setting the color
				float colorChange = (float)(attackTimer / attackTimerMax);
				GetComponent<SpriteRenderer>().color = new Color(1, 1 - colorChange, 1 - colorChange, 1);
			}
			// look at where he will charge
			else
				SetLook(attackPosition);

			// Setting the position to charge after a certain time
			if (attackTimer >= attackTimerMax && !attackSet)
			{
				attackSet = true;
				attackPosition = player.position;
				SetLook(attackPosition);
				hitInfo = Physics2D.Raycast(transform.position, attackPosition.normalized);
			}
			// CHARGING
			else if (attackTimer >= attackTimerMax + attackDelay)
			{
				attackSet = false;
				attackTimer = 0;
				attackAnimation = true;
			}

			// increment the timer
			attackTimer++;
		}
	}

	void SetLook(Vector3 lookAt)
    {
        transform.up = lookAt - transform.position;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, (transform.rotation.eulerAngles.z + 180));
    }
}
