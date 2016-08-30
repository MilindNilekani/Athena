using UnityEngine;
using System.Collections;

public class uipat : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<RectTransform>().anchoredPosition = new Vector2(2*Input.mousePosition.x-Screen.width, 2 * Input.mousePosition.y - Screen.height);
        }
	
	}
}
