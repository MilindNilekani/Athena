using UnityEngine;
using System.Collections;

public class adjustment_env : MonoBehaviour {
	private bool size;

	// Use this for initialization
	void Start () {
		size=false;
	}
	
	// Update is called once per frame
	void Update () {
		foreach (int bigl in LevelLoader.instance.biglevels)
		{
			if(LevelLoader.instance.current_level==bigl)
				size=true;
		}

		if(size==false)
		{transform.GetChild(1).gameObject.SetActive(true);
		transform.GetChild(0).gameObject.SetActive(false);
		}
		else
			{transform.GetChild(0).gameObject.SetActive(true);
				transform.GetChild(1).gameObject.SetActive(false);
			}

	}
}
