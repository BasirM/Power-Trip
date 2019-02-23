using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject bulletPrefab; //the bullet prefab
    public GameObject controller; //this game object
    public GameObject player; //the player to target
    private bool hasSighted; //is the player in range
    private bool patroling; //is the player patroling
    public GameObject healthBar; //the visual representation of the enemies health
    public NavMeshAgent agent; //the agent on the enemy game object
    public GameObject[] navPoints; //points the enemy patrols
    private int currentPoint = 0;

    //Stats//
    public int health; //enemy health
    public int bulletSpeed; //how fast the bullets move
    public int attackRange; //the range where the enemy can start attacking
    public int attackDamage; //how much damage the enemy deals
    private int attackCoolDown; //how long until the enemy can attack again


	// Use this for initialization
	void Start ()
    {
        agent.enabled = true;
        hasSighted = false; //on game start the enemy has not seen the player
        agent.autoBraking = false;
        patroling = false;
        Patrol();
    }
    public void Patrol()
    {
        if (navPoints.Length == 0)
            return;

        //set the agent to the currently selected destination
        agent.destination = navPoints[currentPoint].transform.position;
        Debug.Log(currentPoint);
        //go to the next point, cycling tothe start if necessary
        currentPoint = (currentPoint + 1) % navPoints.Length;
        Debug.Log(currentPoint);
    }

    // Update is called once per frame
    public void updateMovement()
    {
        if (agent.enabled)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                Patrol();
            }

            if (health <= 0)
                Destroy(controller); //if enemy health is less than or equal to 0

            if (hasSighted) //if the player is in the enemies range
            {
                patroling = false;
                agent.SetDestination(player.transform.position); // move towards the player
                Shoot(); //and shoot at the player
            }
            if (Vector3.Distance(controller.transform.position, player.transform.position) < attackRange) //if the player isn't in range, keep looking if the player enters range
            {
                hasSighted = true; //change in range to true
            }
            
            attackCoolDown = --attackCoolDown <= 0 ? 0 : attackCoolDown;
        }
       
	}

    public void Shoot()
    {
        if (attackCoolDown != 0)
            return; // if there is still time left in the cooldown don't shoot

        BulletManager b = Instantiate(bulletPrefab, controller.transform.position, controller.transform.rotation).GetComponent<BulletManager>(); // make a bullet object
        b.tag = "EnemyBullet"; //tag the bullet copy
        b.target_Tag = "Player"; //tag the bullets target
        b.velocity = controller.transform.forward * bulletSpeed; //set the bullets forward speed
        b.damage = attackDamage; //set the damage done by the bullet

        attackCoolDown = 200; //resets the cooldown
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerBullet") //if the tag of the collided object is a player bullet
        {
            health -= other.GetComponent<BulletManager>().damage; //subtract the damage of the bullet from the enemies health
            Destroy(other.gameObject); //destroy the player bullet

            healthBar.transform.localScale = new Vector3(healthBar.transform.localScale.x - .6f, healthBar.transform.localScale.y, healthBar.transform.localScale.z); //make the enemy health bar smaller
        }
    }
}
