using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputLevelcheat : MonoBehaviour {
	InputField levelcheat;
	  

	// Use this for initialization
	void Start () {
		levelcheat=this.gameObject.GetComponent<InputField>();
		levelcheat.shouldHideMobileInput=false;
	}
	
	// Update is called once per frame
	void Update () {

		if (levelcheat.text.Length>0)
		{
			int InputLevel=int.Parse(levelcheat.text);
			LevelLoader.instance.level=InputLevel;
		}

	}
}
