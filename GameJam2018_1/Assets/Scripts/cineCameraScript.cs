using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cineCameraScript : MonoBehaviour {

    public GameObject camera;

	// Use this for initialization
	void Start ()
    {
        camera.transform.Translate(camera.transform.position.x, camera.transform.position.y, 88.2f);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
