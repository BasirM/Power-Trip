using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tasks : MonoBehaviour {

    // Use this for initialization
    public string nextLevel;
    public CableManager cables;
    public GameObject endObj;
    public GameObject player;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(player.transform.position, endObj.transform.position) < 20)
        {
            if(Vector3.Distance(cables.getLastCircle().transform.position,endObj.transform.position) < 20)
            {
                cables.endPos = endObj.transform;
                SceneManager.LoadScene(nextLevel);
            }
        }
	}
}
