using UnityEngine;
using System.Collections;

public class textrender : MonoBehaviour {
	public static textrender instance=null;
 

	void Awake()
	{
		if (instance==null)
			instance=this;
		else if (instance!=this)
			Destroy(gameObject);	
	}
	// Use this for initialization
	void Start () {
		transform.LookAt(-camera_controller.instance.current_camera.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(-camera_controller.instance.current_camera.transform.position);
	
	}
}
