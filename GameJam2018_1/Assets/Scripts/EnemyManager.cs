using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EnemyManager : MonoBehaviour
{
    List<EnemyController> enemyManager;

    void Start()
    {
        enemyManager = new List<EnemyController>();
        GameObject[] e = GameObject.FindGameObjectsWithTag ("Enemy");
        //Debug.Log(e.Length);
        for(int i =0; i < e.Length;i++)
        {
            enemyManager.Add(e[i].GetComponent<EnemyController>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i =0; i < enemyManager.Count;i++)
        {
            enemyManager[i].updateMovement();
            if(enemyManager[i].health <= 0)
            { 
                enemyManager.Remove(enemyManager[i]);
                i--;
            }
        }
    }
}
