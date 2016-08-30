using UnityEngine;
using System.Collections;

public class atkvfx : MonoBehaviour {

	public float radius=0.2f;
	Vector3 startpoint;

	// Use this for initialization
	void Start () {
		radius=0.2f;
		startpoint=transform.position;
		StartCoroutine("blingvfx");
	}
	
	// Update is called once per frame
	IEnumerator blingvfx() {
		while(true)
		{
		for(int cnt=0;cnt<200;cnt++)
		{
			radius+=0.01f-cnt*0.0001f;
	        transform.Translate(radius*Mathf.Cos (cnt*Mathf.PI/20),0.01f-cnt*0.0001f,radius*Mathf.Sin(cnt*Mathf.PI/20));
			yield return new WaitForSeconds(0.01f);
		}
			radius=0.2f;
			transform.position=startpoint;
		}
	}
}
