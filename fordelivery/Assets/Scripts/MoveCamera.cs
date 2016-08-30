using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {
	
	public float speed = 0.005f;

    public float perspectiveZoomSpeed = 0.5f;
    public float orthoZoomSpeed = 0.5f;
    public float minZoom = 20f;
    public float maxZoom = 100f;

    private Camera _camera;



    // Use this for initialization
    void Start () {
		_camera = GetComponent<Camera>();

    }
	
	IEnumerator fixmapcamera()
	{
		yield return new WaitForSeconds(0.2f);
        gameObject.transform.position=new Vector3(GameManager.instance.player.transform.position.x,20,GameManager.instance.player.transform.position.z);

	}

    // Update is called once per frame
    void Update()
    {

        if (true)
        {

            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                //Get movement of the finger since last frame
                Vector2 touchPosition = Input.GetTouch(0).deltaPosition;
                Vector3 movingVector = new Vector3(-speed * touchPosition.x, -speed * touchPosition.y, 0);
                //Move object across XY plane
                transform.Translate(movingVector);
            }

            else if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float currTouchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                float deltaMagnitudeDiff = prevTouchDeltaMag - currTouchDeltaMag;

                //Use these for orthographic camera

                _camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
                _camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView, minZoom, maxZoom);

                //print("zzzz");
                //react to the rotating camera
                //process takes 0.5s,during this process, no drag or reacting to something else
            }
            else 
            {
                if (Input.GetAxis("Mouse ScrollWheel") < 0)
                    _camera.fieldOfView+=5;
                else if ( (Input.GetAxis("Mouse ScrollWheel") > 0))
                     _camera.fieldOfView-=5;
                _camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView, minZoom, maxZoom);

            }


        }
    }//update
}

