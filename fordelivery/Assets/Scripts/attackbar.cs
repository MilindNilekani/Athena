using UnityEngine;
using System.Collections;

public class attackbar : MonoBehaviour {
	//this is on the layer transparent UI

	public static attackbar instance=null;
	float healthbarWidth;
	Transform healthbar;
	// Use this for initialization
	void Awake ()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
	}
	

	void Start () {
		healthbar=this.gameObject.transform.GetChild(0);
		healthbarWidth =healthbar.GetComponent<MeshRenderer>().bounds.size.x;
		StartCoroutine("timepassing");
	}

	// Update is called once per frame
IEnumerator timepassing()
	{
		healthbar.localPosition=new Vector3(0f,0f,-0.001f);
		for (int cnt=0;cnt<60;cnt++)
		{
			healthbar.localScale=new Vector3(-0.9f/60+healthbar.localScale.x,healthbar.localScale.y,healthbar.localScale.z);
			healthbar.Translate(new Vector3(-0.015f*healthbarWidth,0f,0f));
		    yield return new WaitForSeconds(0.02f);
		}
		//destroy game object

		Destroy(this.gameObject);
	}//end timepassing
}
