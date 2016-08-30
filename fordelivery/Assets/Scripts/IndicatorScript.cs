using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class IndicatorScript : MonoBehaviour {
    public static IndicatorScript instance = null;
    public int selected_button=0;
    public Sprite unlocked;
    public Sprite selected;
    public Sprite selected_pressed;
    public Button[] button;
    SpriteState b2_st = new SpriteState();

    private UnityAction action;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        //DontDestroyOnLoad(gameObject);

    }
    // Use this for initialization
    void Start ()
    {
        
    }

    public void ClearSpriteAndStuff()
    {
        if (selected_button == 0)
        {
            return;
        }
        else
        {

            button[selected_button - 1].onClick.RemoveAllListeners();
        
            
            Level2Button l2b = button[selected_button - 1].GetComponent<Level2Button>();
           button[selected_button - 1].onClick.AddListener(()=> l2b.CallOnNextClick());
            button[selected_button-1].gameObject.GetComponent<Image>().sprite = unlocked;

        }
    }

    public void ChangeSpriteAndStuff()
    {
        
        
        button[selected_button-1].gameObject.GetComponent<Image>().sprite = selected;
        b2_st.pressedSprite = selected_pressed;
        button[selected_button-1].spriteState = b2_st;
        button[selected_button-1].onClick.AddListener(() => CallLevel());
    }

    void CallLevel()
    {
        SoundManager.instance.playSFX(SoundManager.instance.clickedUnlockedLevel_hold);
        LevelLoader.instance.level = selected_button;
        //StartCoroutine("StartUnlockedLevel");
       
    }

    

    // Update is called once per frame
    void Update () {
	
	}
}
