using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableManager : MonoBehaviour {

    // Use this for initialization
    public Transform startPos;
    public Transform endPos;
    private Vector3 currPos;
    public GameObject circlePrefab;
    public GameObject[] Nodes;

    public List<GameObject> cable;

    void Start () {
        cable = new List<GameObject>();
        currPos = startPos.position;

        
        int i = 0;

        while (i < Nodes.Length)
        {
           endPos = Nodes[i].transform;
           Nodes[i].SetActive(true);
            while (Vector3.Distance(currPos, endPos.position) > 3)
            {
                currPos = Vector3.MoveTowards(currPos, endPos.position, .5f);
                cable.Add(Instantiate(circlePrefab, currPos, startPos.rotation));
            }
            i++;
        }
        for(i =0; i < Nodes.Length; i++)
        {
           Nodes[i].SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () { 
        if(Vector3.Distance(currPos,endPos.position) > 1.5)
        {
            currPos = Vector3.MoveTowards(currPos, endPos.position, .5f);
            cable.Add(Instantiate(circlePrefab, currPos, startPos.rotation));
        }
	}
    public GameObject getLastCircle()
    {
        return cable.Count != 0 ? cable[cable.Count - 1] : null;

    }
}
