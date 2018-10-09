using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    // attributes
    GameObject orbScript;
    InputManagerScript inputManager;

	// Use this for initialization
	void Start ()
    {
        orbScript = GameObject.FindGameObjectWithTag("Orb");
        inputManager = GameObject.Find("InputManager").GetComponent<InputManagerScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
