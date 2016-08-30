using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class titlebutton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Button>().onClick.AddListener(gotoover);
		
	
	}
	
	// Update is called once per frame
	void gotoover () {
		Application.LoadLevel("custom");
	}
}
