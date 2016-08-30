using UnityEngine;
using System.Collections;

public class PinchToZoom : MonoBehaviour
{

	public float perspectiveZoomSpeed = 0.5f;
	public float orthoZoomSpeed = 0.5f;
	public float minZoom = 40f;
	public float maxZoom = 100f;
	public bool zoom = true;

	private Camera _camera;

	void Start()
	{
		_camera = camera_controller.instance.current_camera;
	}


	// Update is called once per frame
	void Update ()
	{
		if (zoom) {
			if (Input.touchCount == 2) {
				Touch touchZero = Input.GetTouch (0);
				Touch touchOne = Input.GetTouch (1);

				Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
				Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

				float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
				float currTouchDeltaMag = (touchZero.position - touchOne.position).magnitude;

				float deltaMagnitudeDiff = prevTouchDeltaMag - currTouchDeltaMag;

				//Use these for orthographic camera

				_camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
				_camera.fieldOfView = Mathf.Clamp (_camera.fieldOfView, minZoom, maxZoom);
			} 
		}

	}
}
