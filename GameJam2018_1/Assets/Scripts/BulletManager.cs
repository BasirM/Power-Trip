using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour {

    // Use this for initialization
    public GameObject controller;
    public string target_Tag;
    public Vector3 velocity;

    public int damage;

	void Start () {
        controller = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        controller.transform.position += velocity * Time.deltaTime;
	}
   }
