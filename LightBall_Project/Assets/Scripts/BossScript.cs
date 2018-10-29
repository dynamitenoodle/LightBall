using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : EnemyScript {

    // attributes

    // attack timer variables
    public float attackTimerMax = 180;
    float attackTimer;
    public float attackDelay = 120;
    Vector2 attackPosition;
    bool attackSet;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GetComponent<Rigidbody2D>().freezeRotation = true;
        attackPosition = Vector3.zero;
        attackSet = false;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        // Setting the rotation to the player and increasing the color of reddness
        if (!attackSet)
        {
            SetLook(player.position);

            // setting the color
            float colorChange = (float)(attackTimer / attackTimerMax);
            GetComponent<SpriteRenderer>().color = new Color(1, 1 - colorChange, 1 - colorChange, 1);
        }
        else
            SetLook(attackPosition);

        Debug.Log(attackTimer + " " + (attackTimerMax + attackDelay));

        if (attackTimer >= attackTimerMax && !attackSet)
        {
            attackSet = true;
            attackPosition = player.position;
            SetLook(attackPosition);
        }
        else if (attackTimer >= attackTimerMax + attackDelay)
        {
            Debug.Log("HIT");
            attackSet = false;
            GetComponent<SpriteRenderer>().color = Color.white;
            attackTimer = 0;
        }

        // increment the timer
        attackTimer++;
    }

    void SetLook(Vector3 lookAt)
    {
        transform.up = lookAt - transform.position;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, (transform.rotation.eulerAngles.z + 180));
    }
}
