using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class uirot : MonoBehaviour {

    // Use this for initialization
    private Image normal_circle;
    private bool parent_clicked;
 
  
  
   
	void Start () {       
        parent_clicked = false;
        normal_circle = GetComponent<Image>();
		normal_circle.sprite= SoundManager.instance.anim_circle[0];

    } 
	
	// Update is called once per frame
	void Update() {
        if (parent_clicked)
        { normal_circle.sprite = SoundManager.instance.anim_circle[1];
            GetComponent<RectTransform>().Rotate(0f, 0f, 2f);
        }
        else {
			normal_circle.sprite = SoundManager.instance.anim_circle[0];
            GetComponent<RectTransform>().Rotate(0f, 0f, 0.5f);
        }
        
	}

    public void rotating(bool parent)
    {
        parent_clicked = parent;
    }
}
