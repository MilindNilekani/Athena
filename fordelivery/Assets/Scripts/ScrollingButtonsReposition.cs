using UnityEngine;
using System.Collections;

public class ScrollingButtonsReposition : MonoBehaviour {
    int buttonDistance = 400;
	// Use this for initialization
	void Start () {
        int ind = SaveScores.instance.GetMostRecentUnlockedLevelID();
        float c = GetComponent<RectTransform>().anchoredPosition.x;
        float d = GetComponent<RectTransform>().anchoredPosition.y;
        c -= buttonDistance * ind;
        Vector2 ha = new Vector2(c, d);
        GetComponent<RectTransform>().anchoredPosition = ha;
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
