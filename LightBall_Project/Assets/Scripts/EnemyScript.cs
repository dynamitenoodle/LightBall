﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : Entity {
    RaycastHit2D hitInfo;
    static RaycastHit2D NOTCOLLIDING;
    public float sightDist;
    public float radius;
    public int speed = 1;
    public int health = 100;
    Transform player;
    public Vector3 direction;
    // Use this for initialization
    static EnemyScript()
    {
        NOTCOLLIDING = new RaycastHit2D();
    }
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        pos = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if(direction == Vector3.zero)
        direction = (player.position - transform.position).normalized;

        if (health < 0)
        {
            Destroy(this.gameObject);
        }
        
       
            hitInfo = NOTCOLLIDING;

      
           
            if (Physics2D.Raycast(transform.position, direction).distance < sightDist)
            {
                hitInfo = Physics2D.Raycast(transform.position, direction);
            }
            else if (Physics2D.Raycast(transform.position + transform.right * radius, direction).distance < sightDist)
            {
                hitInfo = Physics2D.Raycast(transform.position + transform.right * radius, direction);
            }

            else if (Physics2D.Raycast(transform.position + transform.right * -radius, direction).distance < sightDist)
            {
                hitInfo = Physics2D.Raycast(transform.position + transform.right * -radius, direction);
            }
            else if((Physics2D.Raycast(transform.position,transform.right).distance<sightDist))
            {
                hitInfo = Physics2D.Raycast(transform.position, transform.right);
            }
            else if (Physics2D.Raycast(transform.position, -transform.right).distance < sightDist)
            {
                hitInfo = Physics2D.Raycast(transform.position, -transform.right);
            }
            else if (Physics2D.Raycast(transform.position - transform.up * radius, transform.right).distance < sightDist)
            {
                hitInfo = Physics2D.Raycast(transform.position - transform.up * radius, transform.right);
            }
            else if(Physics2D.Raycast(transform.position - transform.up * radius, -transform.right).distance < sightDist)
            {
                hitInfo = Physics2D.Raycast(transform.position - transform.up * radius, -transform.right);
            }
            if (hitInfo != NOTCOLLIDING)
           { 
               
                    Vector3 dir1 = new Vector3(hitInfo.normal.y, hitInfo.normal.x).normalized;
                    Vector3 dir2 = new Vector3(-hitInfo.normal.y, -hitInfo.normal.x).normalized;
                    if (Vector3.Dot(dir1, direction.normalized) > 0)
                    {
                        direction = Vector3.Slerp(direction, dir1, .1f);
                        
                    }
                    else
                    {
                        direction = Vector3.Slerp(direction, dir2, .1f);
            }
                  

            }
            else
            {
                
                direction = Vector3.Slerp(direction,(player.position - transform.position).normalized,.1f);
                Debug.DrawRay(transform.position, direction);
            }
           
       
        
        transform.up = direction;
        acc += force * direction;
        UpdatePosition();
	}
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (collider.gameObject.tag == "Orb")
        {
            health -= 60;
        }
    }
}
