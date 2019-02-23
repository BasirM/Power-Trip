using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesManager : MonoBehaviour {

    // Use this for initialization

    public GameObject[] ShieldPanels;
    public GameObject[] FirePanels;
    public GameObject[] LifeSupportPanels;
    public GameObject[] LightPanels;
    public PlayerControlller player;

	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        updatePanel(FirePanels, player.selectedAbilities[1]);
        updatePanel(ShieldPanels, player.selectedAbilities[2]);
        updatePanel(LifeSupportPanels, player.selectedAbilities[3]);
        updatePanel(LightPanels, player.selectedAbilities[4]);
    }


    public void updatePanel(GameObject[] panels,int size)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            Image img = panels[i].GetComponent<Image>();
            if (i < size)
            {
                Color c = new Color();
                ColorUtility.TryParseHtmlString("#3344F0FF", out c);
                img.color = c;
            }
            else
            {
                Color c = new Color();
                ColorUtility.TryParseHtmlString("#FFFFFFFF", out c);
                img.color = c;
            }
        }
    }
}
