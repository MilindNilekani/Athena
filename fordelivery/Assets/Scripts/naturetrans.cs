using UnityEngine;
using System.Collections;

public class naturetrans : MonoBehaviour {

	public Material toplava_static;
	public Material topgrass_static;
	//used for performing transformation

	public Material transmat;

	private MeshRenderer rend;


	// Use this for initialization
	void Start () {
		rend=gameObject.GetComponent<MeshRenderer>();
		StartCoroutine("p_receiver");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator p_receiver()
	{
		while(true)
		{
			float dist=Vector2.Distance(new Vector2(transform.position.x,transform.position.z),
				new Vector2(GameManager.instance.player.transform.position.x,GameManager.instance.player.transform.position.z));

			if(dist<6.9f)
				{StartCoroutine("transform_mat");}

				yield return new WaitForSeconds(0.3f);
		}

	}

	IEnumerator transform_mat()
	{
		/*
		 * mat->transmat
		 * transmat.texture->texture1
		 * transmat._Color.a->(1:0.05:0)
		 * transmat.texture->texture2
		 * transmat._Color.a->(0:0.05:1)
		 * mat-> static
		 */

		StopCoroutine("p_receiver");
					 
		transmat.mainTexture=toplava_static.mainTexture;
		rend.material=transmat;
		Color temp_color=new Color(1f,1f,1f);
		temp_color.a=1f;

		for (int cnt=0;cnt<10;cnt++)
		{
			temp_color.a=1f-cnt*0.1f;
			transmat.color=temp_color;
			yield return new WaitForSeconds(0.02f);
		}

		transmat.mainTexture=topgrass_static.mainTexture;

		for (int cnt=0;cnt<10;cnt++)
		{
			temp_color.a=cnt*0.1f;
			transmat.color=temp_color;
			yield return new WaitForSeconds(0.02f);
		}

		rend.material=topgrass_static;
		yield return new WaitForEndOfFrame();

	}
}
