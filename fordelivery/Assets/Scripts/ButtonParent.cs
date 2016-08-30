using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonParent :MonoBehaviour,IPointerDownHandler, IPointerUpHandler{
    Button thisbutton;

    void Start()
    {
        thisbutton = GetComponent<Button>();
    }

   public void OnPointerDown(PointerEventData eventData)
    {
        transform.GetChild(0).GetComponent<uirot>().SendMessage("rotating", true);
    }

   public void OnPointerUp(PointerEventData eventData)

    {
        transform.GetChild(0).GetComponent<uirot>().SendMessage("rotating", false);
    }


}
