using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class coincollect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.GetComponent<Text>().text="Moves : "+GameManager.instance.levelMoves;
	
	}
}
