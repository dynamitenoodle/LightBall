using System.Collections;
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
    Vector3 Target;
    Transform player;
    Vector3 home;
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
        home = pos;
        Target = home;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if(direction == Vector3.zero)
        direction = (player.position - transform.position).normalized;
        if ((player.position - transform.position).sqrMagnitude < sightDist * sightDist)
        {
            Target = player.position;//if we see the player go to player
        }
        else
        {
            Target = home;//else go home
        }
        if (health < 0)
        {
            Destroy(this.gameObject);
        }
        
       
            hitInfo = NOTCOLLIDING;
        

        if (GetComponent<SpriteRenderer>().isVisible) {
            
            if (Physics2D.Raycast(transform.position, Target - transform.position).distance < sightDist)//wall between us and target
            {
                hitInfo = Physics2D.Raycast(transform.position, Target - transform.position);
            }
            else if (Physics2D.Raycast(transform.position + transform.right * radius, player.position - transform.position).distance < sightDist)//wall between us and target on right
            {
                hitInfo = Physics2D.Raycast(transform.position + transform.right * radius, Target - transform.position);
            }

            else if (Physics2D.Raycast(transform.position + transform.right * -radius, Target - transform.position).distance < sightDist)//wall between us and target on left
            {
                hitInfo = Physics2D.Raycast(transform.position + transform.right * -radius, Target - transform.position);
            }
            else if ((Physics2D.Raycast(transform.position, transform.right).distance < sightDist))//wall on right
            {
                hitInfo = Physics2D.Raycast(transform.position, transform.right);
            }
            else if (Physics2D.Raycast(transform.position, -transform.right).distance < sightDist)//wall on left
            {
                hitInfo = Physics2D.Raycast(transform.position, -transform.right);
            }
            else if (Physics2D.Raycast(transform.position - transform.up * radius, transform.right).distance < sightDist)//wall behind us on right
            {
                hitInfo = Physics2D.Raycast(transform.position - transform.up * radius, transform.right);
            }
            else if (Physics2D.Raycast(transform.position - transform.up * radius, -transform.right).distance < sightDist)//wall behind us on left
            {
                hitInfo = Physics2D.Raycast(transform.position - transform.up * radius, -transform.right);
            }
            if (hitInfo != NOTCOLLIDING && Physics2D.Raycast(transform.position, Target - transform.position).distance < sightDist)//we're headed straight for a wall
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

                direction = Vector3.Slerp(direction, (Target - transform.position).normalized, .04f);
                Debug.DrawRay(transform.position, direction);
            }
            
        }

        if ((Target - transform.position).sqrMagnitude > (sightDist/8)*(sightDist/8))
        {
            transform.up = new Vector3(direction.x,direction.y,0);
            acc += force * new Vector3(direction.x, direction.y, 0);
        }
        else
        {
            vel = Vector3.zero;
            acc = Vector3.zero;
            if ((transform.up - Vector3.up).magnitude > .01f)
                transform.up = Vector3.Lerp(transform.up, Vector3.up, .3f);
            else
            {
                transform.up = Vector3.up;
            }
        }
        UpdatePosition();
	}
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            home = collider.transform.position;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (collider.gameObject.tag == "Orb" && collider.gameObject.GetComponent<OrbScript>().Damage)
        {
            home = collider.transform.position;
            health -= 60;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            home = pos;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (collision.gameObject.tag == "Orb" && collision.gameObject.GetComponent<OrbScript>().Damage)
        {
            home = pos;
            health -= 60;
        }
    }
}
