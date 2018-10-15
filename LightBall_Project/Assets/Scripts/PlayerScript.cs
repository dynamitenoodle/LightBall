using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : Entity {

    // attributes
    GameObject orb;
    InputManagerScript inputManager;
    Vector3 goalRot;

    // Use this for initialization
    void Start ()
    {
        orb = GameObject.FindGameObjectWithTag("Orb");
        inputManager = GameObject.Find("InputManager").GetComponent<InputManagerScript>();
		pos = transform.position;
		vel = Vector3.zero;
		acc = Vector3.zero;
		Physics2D.IgnoreCollision(GetComponent<Collider2D>(), orb.GetComponent<Collider2D>(), true);
		Physics2D.IgnoreCollision(transform.GetChild(0).gameObject.GetComponent<Collider2D>(), orb.GetComponent<Collider2D>(), true);
		Physics2D.IgnoreCollision(GetComponent<Collider2D>(), transform.GetChild(0).gameObject.GetComponent<Collider2D>(), true);
        goalRot = Vector3.zero;
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
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");

		// Movement
		vel.x += x * force;
		vel.y += y * force;

		if (x == 0)
			vel.x *= .8f;

		if (y == 0)
			vel.y *= .8f;

		UpdatePosition();
		UpdateDirection(x,y);
	}

	// Updating which way the player is facing
	void UpdateDirection(float x, float y)
	{
        if (x != 0)
            goalRot = new Vector3(x, goalRot.y, goalRot.z);

        if (y != 0)
            goalRot = new Vector3(goalRot.x, y, goalRot.z);

        goalRot.z = Mathf.Sqrt((goalRot.x*goalRot.x) + (goalRot.y * goalRot.y));
        Vector3 goalRotEuler = Vector3.zero;
        goalRotEuler.z = Mathf.Atan(goalRot.y / goalRot.x);
        Debug.Log(goalRotEuler.z);

        transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.rotation.eulerAngles, goalRotEuler, .1f));
    }

	
}


/* Old Movement Code
// Vertical Movement
if (Input.GetKey(KeyCode.W))
	acc.y += force;

if (Input.GetKey(KeyCode.S))
	acc.y -= force;

if (NoKeysHeld(0))
	acc.y = 0;

// Horizontal Movement
if (Input.GetKey(KeyCode.D))
	acc.x += force;

if (Input.GetKey(KeyCode.A))
	acc.x -= force;

if (NoKeysHeld(1))
	acc.x = 0;

if (NoKeysHeld(0) && NoKeysHeld(1))
{
	vel *= .8f;
}*/

/* Old Rotation Code
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
    {
        if (transform.rotation.eulerAngles.z > 180)
            goalRotEuler = new Vector3(0, 0, 360);//transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        else
            goalRotEuler = new Vector3(0, 0, 0);
    }

    else if (!up && down && !left && !right || !up && down && left && right) // DOWN
        goalRotEuler = new Vector3(0, 0, 180);//transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));

    else if (!up && !down && left && !right || up && down && left && !right) // LEFT
        goalRotEuler = new Vector3(0, 0, 90);//transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));

    else if (!up && !down && !left && right || up && down && !left && right) // RIGHT
        goalRotEuler = new Vector3(0, 0, 270);//transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270));

    else if (up && !down && left && !right)
        goalRotEuler = new Vector3(0, 0, 45);//transform.rotation = Quaternion.Euler(new Vector3(0, 0, 45)); // UP LEFT

    else if (!up && down && left && !right)
        goalRotEuler = new Vector3(0, 0, 135);//transform.rotation = Quaternion.Euler(new Vector3(0, 0, 135)); // DOWN LEFT

    else if (up && !down && !left && right)
        goalRotEuler = new Vector3(0, 0, 315);//transform.rotation = Quaternion.Euler(new Vector3(0, 0, 315)); // UP RIGHT

    else if (!up && down && !left && right)
        goalRotEuler = new Vector3(0, 0, 225);//transform.rotation = Quaternion.Euler(new Vector3(0, 0, 225)); // DOWN LEFT

    if (goalRotEuler.z > 180 && transform.rotation.z == 0)
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 360));

    if (goalRotEuler.z <= 180 && transform.rotation.z == 360)
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

    transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.rotation.eulerAngles, goalRotEuler, .1f));
    */

/*
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
}*/
