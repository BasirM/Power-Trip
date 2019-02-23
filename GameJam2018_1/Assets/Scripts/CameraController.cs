using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // Use this for initialization
    private Transform controller;
    public Transform player;
    public int viewMultiplier = 30;

	void Start () {
        controller = this.gameObject.transform;
        controller.position = player.position;
        controller.LookAt(player);
	}
	
	// Update is called once per frame
	void Update () {
        controller.position = player.position;
        controller.Translate(Vector3.up * viewMultiplier, Space.World);
	}
}
