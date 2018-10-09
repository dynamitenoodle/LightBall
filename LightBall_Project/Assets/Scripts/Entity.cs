using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour {

	// attrbutes
	[HideInInspector]
	public Vector3 pos, vel, acc;
	public float maxVel, force;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// updating the players position
	public void UpdatePosition()
	{
		vel += acc;
		vel = Vector3.ClampMagnitude(vel, maxVel);
		pos += vel;
		transform.position = pos;
		acc = Vector3.zero;
	}
}
