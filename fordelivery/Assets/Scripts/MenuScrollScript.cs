using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuScrollScript : MonoBehaviour {
   
    public RectTransform panel;
    public Button[] button;
    public RectTransform centre;

    public Image leftSprite;
    public Image rightSprite;

    int i = 0;

    float[] distance;
    float[] distance_reposition;
    bool dragging = false;

    int buttonDistance;
    int minButtonname;

   void Awake()
    {
       
        int buttonLength = button.Length;
        distance = new float[buttonLength];
        distance_reposition = new float[buttonLength];
        
        buttonDistance = (int)Mathf.Abs(button[1].GetComponent<RectTransform>().anchoredPosition.x - button[0].GetComponent<RectTransform>().anchoredPosition.x);
        
    }
    void Start()
    {
        
        
        
    }

    void Update()
    {
        for(int i=0; i< button.Length; i++)
        {
            distance_reposition[i] = centre.GetComponent<RectTransform>().position.x - button[i].GetComponent<RectTransform>().position.x;
            distance[i] = Mathf.Abs(distance_reposition[i]);
          
        }
        float minDistance = Mathf.Min(distance);
        for(int j=0; j< button.Length;j++)
        {
            if(minDistance==distance[j])
            {
                minButtonname = j;
            }
        }
        if(!dragging)
        {
            //LerpToButton(minButtonname * -buttonDistance);
            LerpToButton(-button[minButtonname].GetComponent<RectTransform>().anchoredPosition.x);
        }
        if(dragging)
        {
            SoundManager.instance.playSFX(SoundManager.instance.scrollingSound);
        }
    }

    public void LerpToButton(float position)
    {
        float newX = Mathf.Lerp(panel.anchoredPosition.x, position, Time.deltaTime * 10f);
        Vector2 newPosition = new Vector2(newX, panel.anchoredPosition.y);
        panel.anchoredPosition = newPosition;
       
    }

    public void StartDrag()
    {
        dragging = true;
    }

    public void EndDrag()
    {
        dragging = false;
    }
}
