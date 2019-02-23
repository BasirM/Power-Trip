using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public GameObject bulletPrefab;
    public GameObject controller;
    public GameObject player;
    private bool hasSighted;
    public GameObject healthBar;


    //Stats
    public int Health;
    public int moveSpeed;
    public int attackRange;
    public int attackDamage;


    private int attackCoolDown;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(player);
        hasSighted = false;
	}
	
	// Update is called once per frame
	void Update () {}

    public void updateMovement()
    {
        if (Health <= 0)
            Destroy(controller);
        if (hasSighted)
        {
            if (Vector3.Distance(controller.transform.position, player.transform.position) <= attackRange)
            {
                controller.transform.LookAt(player.transform.position);
                shoot();
            }
            else
            {
                controller.transform.LookAt(player.transform.position);
                float speed = moveSpeed * Time.deltaTime;
                controller.transform.position = Vector3.MoveTowards(controller.transform.position, player.transform.position, speed);
            }
        }
        else if (Vector3.Distance(controller.transform.position, player.transform.position) < attackRange + 50)
        {
            hasSighted = true;
        }
        attackCoolDown = --attackCoolDown <= 0 ? 0 : attackCoolDown;
    }

    public void shoot()
    {
        if (attackCoolDown != 0) return;


        //MEELE OPTION
        if(attackRange == 1){
            PlayerControlller p = player.GetComponent<PlayerControlller>();
            p.tankHealth -= attackDamage;
        }else{
            BulletManager b = Instantiate(bulletPrefab, controller.transform.position, controller.transform.rotation).GetComponent<BulletManager>();

            b.tag = "EnemyBullet";
            b.target_Tag = "Player";
            b.velocity = controller.transform.forward * 40;
            b.damage = attackDamage;

            attackCoolDown = 200;
        }

    }


    public void OnTriggerEnter(Collider other)
    {
		Debug.Log (other.tag);
        if(other.tag == "PlayerBullet")
        {
            Health -= other.GetComponent<BulletManager>().damage;
            Destroy(other.gameObject);

            healthBar.transform.localScale = new Vector3(healthBar.transform.localScale.x - .6f, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        }
    }
}
