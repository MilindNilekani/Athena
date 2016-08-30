using UnityEngine;
using System.Collections;

public class floatingcrystal : MonoBehaviour {

	// Use this for initialization
	private float start_time;
    public Vector3 start_pos;


    void Start () {
		start_time=Time.time;
		
        //start_pos = new Vector3(0f,5f,0f);

	}
		
	// Update is called once per frame
	void Update()
	{
		float spline_x=Time.time-start_time;
		transform.localPosition=new Vector3(0f,0.5f*Mathf.Sin(5*spline_x),0f)+start_pos;
	}

}
