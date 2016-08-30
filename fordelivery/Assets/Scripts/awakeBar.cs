using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class awakeBar : MonoBehaviour {
	Image image;
	int powerprogress;
	int original;
	// Use this for initialization
	

	void Start () {
    	image = GetComponent<Image> ();
		image.fillAmount = 0f;
		powerprogress=GameManager.instance.PowerUpBar;
	
	}
	
	// Update is called once per frame
	void Update () {
		//if there is change

		int newdata=listen_bar();
		if (powerprogress!=newdata)
		{
			original=powerprogress;
			powerprogress=newdata;
			StartCoroutine("smoothchange");
		}

	}

	int listen_bar()
	{
		switch(GameManager.instance.PowerUpBar)
		{
		case 4:
			return 3;
		 
		 default:
			return GameManager.instance.PowerUpBar;	
			
		}

	}

	//change gradually
	IEnumerator smoothchange()
	{
		float start_point=original*0.33f;
		float end_point=powerprogress*0.33f;

		for(int cnt=0;cnt<20;cnt++)
		{
			image.fillAmount = start_point+cnt*(end_point-start_point)/20;
			yield return new WaitForEndOfFrame();
		}

	}
}
