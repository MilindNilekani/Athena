using UnityEngine;
using System.Collections;

public class testmeshpar : MonoBehaviour {
	public GameObject meshparticle;
	public GameObject house;
	public GameObject mask;
	public Camera camera_last;
	public Camera camera_first;
	public Shader glow;
	public Shader normal;
	Vector3 originpos;
	Vector3 endpos=new Vector3(1f,3.9f,-8.6f);
	Material mat;


	// Use this for initialization
	void Start () {
		
		meshparticle.SetActive(false);
		mat=house.GetComponent<MeshRenderer>().material;
		mat.shader=normal;
	 
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.A))
			StartCoroutine("blingblinggone");
		RenderTexture.active=camera_first.targetTexture;


		if (Input.GetKeyDown(KeyCode.Z))
			mat.shader=glow;

		if (Input.GetKeyDown(KeyCode.X))
			mat.shader=normal;

	}


	Texture2D RTImage(Camera cam) {
		RenderTexture currentRT = RenderTexture.active;
		RenderTexture.active = cam.targetTexture;
		cam.Render();
		Texture2D image = new Texture2D(cam.targetTexture.width, cam.targetTexture.height);
		image.ReadPixels(new Rect(0, 0, cam.targetTexture.width, cam.targetTexture.height), 0, 0);
		image.Apply();
		RenderTexture.active = currentRT;
		return image;
	}


	IEnumerator blingblinggone()
	{

		meshparticle.SetActive(true);


		for(int cnt=0;cnt<50;cnt++)
		{
		
			mask.transform.localScale=new Vector3(0.4f*cnt,0.4f*cnt,0.4f*cnt);

			yield return new WaitForSeconds(0.02f);
		
		}//p.startSpeed=3f;

		house.SetActive(false);
		yield return new WaitForSeconds(3f);
		meshparticle.SetActive(false);
		house.SetActive(true);
		mask.transform.position=originpos;
		mask.transform.localScale=new Vector3(0.1f,0.1f,0.1f);
	}
}
