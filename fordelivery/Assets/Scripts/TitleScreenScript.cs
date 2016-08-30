using UnityEngine;
using System.Collections;

public class TitleScreenScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if(Application.platform==RuntimePlatform.IPhonePlayer)
        {
            int fingerCount = 0;
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
                    fingerCount++;

            }
            if (fingerCount > 0)
                Application.LoadLevel("overworld");
        }
    if(Application.platform==RuntimePlatform.WindowsEditor)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Application.LoadLevel("overworld");
            }
        }
	}
}
