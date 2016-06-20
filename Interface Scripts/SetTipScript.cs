using UnityEngine;
using System.Collections;

public class SetTipScript : MonoBehaviour {

    private bool isLoadGame = false;
    private bool tipsView = false;
    public Canvas loadGameCanvas;
    public GameObject []tipsObj;
    private int idx = 0;
	// Use this for initialization
	// Update is called once per frame
	void Update () {
	    if(loadGameCanvas.enabled == true && isLoadGame == false)
        {
            isLoadGame = true;
            idx = SetIndexToEnable(tipsObj.Length - 1);
        }
        else if(loadGameCanvas.enabled == false && isLoadGame == true)
        {
            isLoadGame = false;
        }
        if(isLoadGame == true && tipsView == false)
        {
            for(int i = 0; i < tipsObj.Length; i++)
            {
                if(idx == i)
                {
                    tipsObj[i].SetActive(true);
                }
                else
                {
                    tipsObj[i].SetActive(false);
                }
            }
            tipsView = true;
        }
        else if(isLoadGame == false && tipsView == true)
        {
            tipsView = false;
        }
	}
    private int SetIndexToEnable (int max)
    {
        return (int)Random.RandomRange(0, max+1);
    }
}
