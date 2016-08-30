using UnityEngine;
using System.Collections;

public class trialparticle : MonoBehaviour {

	bool power_up;
    private Vector3 player_shrink = new Vector3(0.04f,0.04f,0.04f);

	// Use this for initialization
	void Start () {
		
		power_up=GameManager.instance.Get_PowerUp;
		for (int cnt=0;cnt<transform.childCount;cnt++)
			transform.GetChild(cnt).gameObject.SetActive(false);
		StartCoroutine("parrotate");
		StartCoroutine("magiccontroller");
	}

	IEnumerator magiccontroller()
	{
		while(true)
		{
		if (!(power_up==GameManager.instance.Get_PowerUp))
		{
			if (GameManager.instance.Get_PowerUp==true)
			{
				StartCoroutine("expand");
				print("wtffffff");
			}
			else
				StartCoroutine("fadeout");
		}


		power_up=GameManager.instance.Get_PowerUp;
		yield return new WaitForSeconds(0.05f);
		}
	}


	// Update is called once per frame
	IEnumerator parrotate() {
		while(true)
		{
			if(transform.childCount==3)
			{
			MovingObject.instance.SetLayerof(this.gameObject,GameManager.instance.player.layer);
				transform.position=GameManager.instance.player.transform.position;
			this.gameObject.transform.GetChild(0).Rotate(0f,0.72f,0f);
			this.gameObject.transform.GetChild(1).Rotate(0f,-1f,0f);
			this.gameObject.transform.GetChild(2).Rotate(0f,-2f,0f);
			}
			
			yield return new WaitForSeconds(0.01f);
		}
		
		}

	IEnumerator expand()
	{
		for (int cnt=0;cnt<transform.childCount;cnt++)
			transform.GetChild(cnt).gameObject.SetActive(true);
		yield return new WaitForEndOfFrame();
		for (int cnt=0;cnt<15;cnt++)
		{
            GameManager.instance.player.transform.localScale += player_shrink;
            transform.localScale=cnt*new Vector3(0.1f,0.1f,0.1f);
			yield return new WaitForEndOfFrame();
		}

	}

	IEnumerator fadeout()
	{
		
		for (int cnt=0;cnt<15;cnt++)
		{
            GameManager.instance.player.transform.localScale -= player_shrink;
            transform.localScale=(15-cnt)*new Vector3(0.1f,0.1f,0.1f);
			yield return new WaitForEndOfFrame();
		}
		for (int cnt=0;cnt<transform.childCount;cnt++)
			transform.GetChild(cnt).gameObject.SetActive(false);
		yield return new WaitForEndOfFrame();

	}

}
