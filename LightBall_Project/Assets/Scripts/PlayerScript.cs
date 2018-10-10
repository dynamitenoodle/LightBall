using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : Entity {

    // attributes
    GameObject orb;
    InputManagerScript inputManager;

	// Use this for initialization
	void Start ()
    {
        orb = GameObject.FindGameObjectWithTag("Orb");
        inputManager = GameObject.Find("InputManager").GetComponent<InputManagerScript>();
		pos = transform.position;
		vel = Vector3.zero;
		acc = Vector3.zero;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		MoveUpdate();

		if (Input.GetKeyDown(KeyCode.Space))
			if (orb.GetComponent<OrbScript>().isHeld)
				orb.GetComponent<OrbScript>().ThrowOrb();
	}

	void MoveUpdate()
	{
		// Vertical Movement
		if (Input.GetKey(KeyCode.W))
			acc.y += force;

		if (Input.GetKey(KeyCode.S))
			acc.y -= force;

		if (NoKeysHeld(0))
		{
			acc.y = 0;
			vel.y *= .8f;
		}

		// Horizontal Movement
		if (Input.GetKey(KeyCode.D))
			acc.x += force;

		if (Input.GetKey(KeyCode.A))
			acc.x -= force;

		if (NoKeysHeld(1))
		{
			acc.x = 0;
			vel.x *= .8f;
		}

		UpdatePosition();
		UpdateDirection();
	}

	// Updating which way the player is facing
	void UpdateDirection()
	{
		bool up, down, left, right;
		up = down = left = right = false;

		if (Input.GetKey(KeyCode.W))
			up = true;

		if (Input.GetKey(KeyCode.S))
			down = true;

		if (Input.GetKey(KeyCode.D))
			right = true;

		if (Input.GetKey(KeyCode.A))
			left = true;

		// checking Directions
		if (up && !down && !left && !right || up && !down && left && right) // UP
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

		else if (!up && down && !left && !right || !up && down && left && right) // DOWN
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));

		else if (!up && !down && left && !right || up && down && left && !right) // LEFT
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));

		else if (!up && !down && !left && right || up && down && !left && right) // RIGHT
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270));

		else if (up && !down && left && !right)
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, 45)); // UP LEFT

		else if (!up && down && left && !right)
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, 135)); // DOWN LEFT

		else if (up && !down && !left && right)
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, 315)); // UP RIGHT

		else if (!up && down && !left && right)
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, 225)); // DOWN LEFT
	}

	// 0 = vertical  1 - horizontal
	bool NoKeysHeld(int axis)
	{
		if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) && axis == 0)
		{
			return false;
		}

		if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && axis == 1)
		{
			return false;
		}

		return true;
	}
}
