using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NonOverworldButton : MonoBehaviour {
    Button b1;
    

    public GameObject popup_options;
    public GameObject confirmation;
    public GameObject yes1;
    public GameObject yes2;
    SpriteState b2_st = new SpriteState();
    
    
    public GameObject credits;
    // Use this for initialization
    void Start () {
        b1 = GetComponent<Button>();
        b2_st = GetComponent<Button>().spriteState;
        if (this.gameObject.name == "ClearScores")
        {
            b1.onClick.AddListener(() => CallConfirmBox_Scores());
            GameObject text_ch = transform.GetChild(0).gameObject;
            text_ch.GetComponent<Text>().text = "Clear Scores";
        }
        else if (this.gameObject.name == "Options")
        {
            popup_options.SetActive(false);
            confirmation.SetActive(false);
            b1.onClick.AddListener(() => OptionsPopUp());


        }
        else if (this.gameObject.name == "Yes1")
        {
            b1.onClick.AddListener(() => CallClearScores());


        }
        else if (this.gameObject.name == "Yes2")
        {

            b1.onClick.AddListener(() => CallClearLevelProgress());

        }
        else if (this.gameObject.name == "No")
        {

            b1.onClick.AddListener(() => CloseConfirmBox());
        }

        else if (this.gameObject.name == "Cancel")
        {

            b1.onClick.AddListener(() => OptionsPopDown());


        }
        else if (this.gameObject.name == "Credits")
        {
            credits.SetActive(false);
            b1.onClick.AddListener(() => CallCredits());
        }
        else if (this.gameObject.name == "ExitCredits")
        {
            b1.onClick.AddListener(() => CutCredits());
        }
        else if (this.gameObject.name == "ClearLevelProgress")
        {
            b1.onClick.AddListener(() => CallConfirmBox_Progress());
            GameObject text_ch = transform.GetChild(0).gameObject;
            text_ch.GetComponent<Text>().text = "Clear Progress";
        }
        else if (this.gameObject.name == "UnlockAllLevels")
        {
            b1.onClick.AddListener(() => UnlockAllLevels());
            GameObject text_ch = transform.GetChild(0).gameObject;
            text_ch.GetComponent<Text>().text = "Unlock";
        }
        else if (this.gameObject.name == "ColorPicker")
        {
            b1.onClick.AddListener(() => CallCustomizationWindow());

        }
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
    void Update () {
	
	}
}
