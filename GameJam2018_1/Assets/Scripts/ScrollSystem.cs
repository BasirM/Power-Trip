using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollSystem : MonoBehaviour {


    public RectTransform powerLevel;
    public RectTransform maxPower;
    public RectTransform tankHealth;
    public RectTransform playerHealth;
    public PlayerControlller player;


    double startWidth;
    double startHeight;
    // Use this for initialization
    void Start () {
        startWidth = powerLevel.rect.width;
        startHeight = powerLevel.rect.height;
    }
	
	// Update is called once per frame
	void Update () {
        powerLevel.sizeDelta = new Vector2((float)((player.powerLevel / 500) * startWidth), (float)startHeight);
        maxPower.sizeDelta = new Vector2((float)(((5-player.maxPowerUsage)/5) * startWidth), (float)startHeight);
        tankHealth.sizeDelta = new Vector2((float)((player.tankHealth / 150) * startWidth), (float)startHeight);
        playerHealth.sizeDelta = new Vector2((float)((player.playerHealth / 100) * startWidth), (float)startHeight);
	}
}
