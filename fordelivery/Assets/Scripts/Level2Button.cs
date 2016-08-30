using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Level2Button : MonoBehaviour
{
    //public static Level2Button instance = null;
    Button b1;
    public int i;
    public Sprite locked;
    public Sprite unlocked;
    public Sprite unlocked_pressed;

    public Sprite descending_unlocked;
    public Sprite descending_locked;
    public Sprite ascending_unlocked;
    public Sprite ascending_locked;

    public GameObject popup_options;
    public GameObject confirmation;
    public GameObject yes1;
    public GameObject yes2;
    SpriteState b2_st=new SpriteState();
    public Sprite gold_star;
    public Sprite silver_star;
    public Sprite bronze_star;
    public Sprite nostar;
    public GameObject credits;
    void Awake()
    {
        
    }
    // Use this for initialization
    void Start()
    {
        b1 = GetComponent<Button>();
        b2_st = GetComponent<Button>().spriteState;

        //b1.onClick.AddListener(() => CallCustomizationWindow());



        if (SaveScores.instance.CheckLevelStatus(i))
            {
            b1.onClick.AddListener(() => CallOnNextClick());
            gameObject.GetComponent<Image>().sprite = unlocked;
                b2_st.pressedSprite = unlocked_pressed;
                gameObject.GetComponent<Button>().spriteState = b2_st;
                //gameObject.GetComponent<Button>().spriteState.pressedSprite = unlocked_pressed;
                GameObject text_ch = transform.GetChild(0).gameObject;
                GameObject image_stars1 = transform.GetChild(1).gameObject;
                GameObject image_stars2 = transform.GetChild(2).gameObject;
                GameObject image_stars3 = transform.GetChild(3).gameObject;
                GameObject text_hs = transform.GetChild(4).gameObject;
                
                
                image_stars1.SetActive(true);
                image_stars2.SetActive(true);
                image_stars3.SetActive(true);
                text_hs.SetActive(true);
                text_hs.GetComponent<Text>().text = "High Score: " + SaveScores.instance.PrintHighScore(i);
                image_stars1.transform.localScale = new Vector3(2, 2, 2);
                image_stars2.transform.localScale = new Vector3(2, 2, 2);
                image_stars3.transform.localScale = new Vector3(2, 2, 2);
                ///////////////////////Star1//////////////////////
                if (SaveScores.instance.IsStar1(i)==1)
                {
                    image_stars1.GetComponent<Image>().sprite = gold_star;
                }
                else
                {
                    image_stars1.GetComponent<Image>().sprite = nostar;
                }
                ////////////////////Star2//////////////////////////////////
				if(SaveScores.instance.GetStarStatus(i))
				{
                if (SaveScores.instance.IsStar3(i)==3)
                {
                    image_stars3.GetComponent<Image>().sprite = gold_star;
                }
                else if (SaveScores.instance.IsStar3(i) == 2)
                {
                    image_stars3.GetComponent<Image>().sprite = silver_star;
                }
                else if (SaveScores.instance.IsStar3(i) == 1)
                {
                    image_stars3.GetComponent<Image>().sprite = bronze_star;
                }
                else if(SaveScores.instance.IsStar3(i) == 0)
                {
                    image_stars3.GetComponent<Image>().sprite = nostar;
                }
				}
				else
				{
					image_stars3.SetActive(false);
				}
                ////////////////////////Star3////////////////////////////////////
                if (SaveScores.instance.IsStar2(i) == 3)
                {
                    image_stars2.GetComponent<Image>().sprite = gold_star;
                }
                else if (SaveScores.instance.IsStar2(i) == 2)
                {
                    image_stars2.GetComponent<Image>().sprite = silver_star;
                }
                else if (SaveScores.instance.IsStar2(i) == 1)
                {
                    image_stars2.GetComponent<Image>().sprite = bronze_star;
                }
                else
                {
                    image_stars2.GetComponent<Image>().sprite = nostar;
                }

                
                text_ch.SetActive(true);
                text_ch.GetComponent<Text>().text = "" +i;
                
            }
            else
            {
                gameObject.GetComponent<Image>().sprite = locked;
                GameObject text_ch = transform.GetChild(0).gameObject;
                GameObject image_stars1 = transform.GetChild(1).gameObject;
                GameObject image_stars2 = transform.GetChild(2).gameObject;
                GameObject image_stars3 = transform.GetChild(3).gameObject;
                GameObject text_hs = transform.GetChild(4).gameObject;
                
                text_ch.SetActive(false);
                
                b1.onClick.AddListener(() => SoundManager.instance.playSFX(SoundManager.instance.clickedlockedLevel));
            

            image_stars1.SetActive(false);
                image_stars2.SetActive(false);
                image_stars3.SetActive(false);
                text_hs.SetActive(false);
            
            }
        }
    

    void Update()
    {
        
    }

    void CallCredits()
    {
        credits.SetActive(true);

    }

    void CutCredits()
    {
        credits.SetActive(false);
    }

    public void OptionsPopUp()
    {
        popup_options.SetActive(true);
        
    }

    public void CallOnNextClick()
    {
        IndicatorScript.instance.ClearSpriteAndStuff();
        IndicatorScript.instance.selected_button = i;
        IndicatorScript.instance.ChangeSpriteAndStuff();
        
    }

    public void OptionsPopDown()
    {
        popup_options.SetActive(false);

    }

    void CallCustomizationWindow()
    {
        StartCoroutine("StartCustomizationScene");
    }

    IEnumerator StartCustomizationScene()
    {
        SoundManager.instance.playSFX(SoundManager.instance.clickedUnlockedLevel_hold);
        yield return new WaitForSeconds(SoundManager.instance.clickedUnlockedLevel_hold.length);
        Application.LoadLevel("custom");
    }
    void CallClearScores()
    {
        SaveScores.instance.ClearScores();
        Debug.Log("Scores cleared");
        Application.LoadLevel("overworld");
    }

    void UnlockAllLevels()
    {
        SaveScores.instance.UnlockAllLevels();
        Debug.Log("All Levels Unlocked");
        Application.LoadLevel("overworld");
    }

    void CallConfirmBox_Scores()
    {
        
        
        confirmation.SetActive(true);
        yes1.SetActive(true);
        yes2.SetActive(false);
    }

    void CallConfirmBox_Progress()
    {
        
        confirmation.SetActive(true);
        yes1.SetActive(false);
        yes2.SetActive(true);
    }

    void CloseConfirmBox()
    {
        confirmation.SetActive(false);
    }
    void CallClearLevelProgress()
    {
        SaveScores.instance.ClearLevelProgress();
        Debug.Log("Level Progress Cleared");
        CallClearScores();
        Application.LoadLevel("overworld");
    }
    

    // Update is called once per frame
 
}