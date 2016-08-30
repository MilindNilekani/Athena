using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class rotatecam : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{

	Button rotatebutton;
	bool Popdown;
	// Use this for initialization
	void Start () {
		StartCoroutine("rotatecamera");
		rotatebutton=gameObject.GetComponent<Button>();
		//rotatebutton.onClick.AddListener(recalc);


	}

	void recalc()
	{
		Vector3 playerposition=GameManager.instance.player.transform.position;
		Vector3 cameraposition=camera_controller.instance.followplayer.transform.position;
		float directionx=playerposition.x-cameraposition.x;
		float directionz=playerposition.z-cameraposition.z;
		camera_controller.instance.camera_rotater=camera_controller.instance.calc_theta(directionx,directionz);
	}
	
	IEnumerator rotatecamera()
	{
		yield return new WaitForSeconds(0.3f);

		while(true)
		{
			if(Popdown)
			{

				int temp=camera_controller.instance.camera_rotater;
				camera_controller.instance.camera_rotater=(temp+1)%100;
			}

			yield return new WaitForEndOfFrame();
		}
	}

	public void OnPointerDown (PointerEventData eventData) 
	{
		Popdown = true;
		camera_controller.instance.rotating=true;

        transform.GetChild(0).GetComponent<uirot>().SendMessage("rotating", true);
	}
	   
	public void OnPointerUp (PointerEventData eventData) 	
		
	{
			Popdown = false;
			camera_controller.instance.rotating=false;
        transform.GetChild(0).GetComponent<uirot>().SendMessage("rotating", false);
    }


}
