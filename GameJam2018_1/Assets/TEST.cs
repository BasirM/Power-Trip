using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TEST : MonoBehaviour {

    public GameObject player; //the player to target
    public NavMeshAgent agent;

    // Use this for initialization
    void Start () {
        agent.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
        agent.SetDestination(player.transform.position);
	}
}
