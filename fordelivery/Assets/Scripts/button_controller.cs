using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class button_controller : MonoBehaviour {

    public Button exit;
    public Button replaybut;
    public Image flyingUI;
    public Image rampage;
    public Image rampage_camera;
	public Image loading;


    // Use this for initialization
    void Start () {
        exit.onClick.AddListener(tooverworld);
        replaybut.onClick.AddListener(replay);
    }
	
	// Update is called once per frame
	void tooverworld () {
		GameManager.instance.ready=false;
		loading.gameObject.SetActive(true);
        SaveScores.instance.SetJustPlayedLevel(LevelLoader.instance.current_level-1);
        //SaveLevel.instance.UpdateXMLList();
        Application.LoadLevel("overworld");
        SoundManager.instance.playBGM(SoundManager.instance.BGM_overworld);
	}

    void replay()
    {
        LevelLoader.instance.level = LevelLoader.instance.current_level;
    }

    void Update()
    {
        /*if (GameManager.instance.rampageUI)
        {
            if (GameManager.instance.Get_PowerUp == false||GameManager.instance.PowerUpBar==4)
            { StartCoroutine("uiFly"); }    
        }*/
		if(GameManager.instance.ready){loading.gameObject.SetActive(false);}
        if (GameManager.instance.Get_PowerUp == true)
        {
            rampage_camera.gameObject.SetActive(true);
            rampage_camera.color = rpgvfxcolor();
       
        }

        else { rampage_camera.gameObject.SetActive(false); }
    }

    private Vector2 Transpos_UI(Vector2 dest)
    {
       Vector2 ui_pos= new Vector2(2 *dest.x - Screen.width, 2 * dest.y - Screen.height);
        return ui_pos;
    }

    private Vector2 flyDest()
    {
        int i = GameManager.instance.PowerUpBar;
        Vector2 Dest;
        RectTransform bar = rampage.rectTransform;
        if (GameManager.instance.Get_PowerUp == false)
        { Dest = (i - 2f) * new Vector2(142, 0) + bar.anchoredPosition; }
        else
        { Dest = (i - 3f) * new Vector2(142, 0) + bar.anchoredPosition; }
        return Dest;
    }


    private Color rpgvfxcolor()
    {
        Color c0 = Color.white; 
        switch (GameManager.instance.PowerUpBar)
        {
            case 1:
                c0 = new Color(0.64f, 0.2f, 0.16f);
                return c0;
            case 2:
                c0 = new Color(0.96f,0.56f,0.43f);
                return c0;
            case 3:
                c0 = new Color(1f, 0.94f, 0.68f);
                return c0;
            default:
                return c0; 
            }

    }

  IEnumerator uiFly()
    {
        GameManager.instance.rampageUI = false;
        flyingUI.gameObject.SetActive(true);
        Vector3 player_pos=grid.instance.calcWorldCoord(new Vector2(GameManager.instance.player_pos[0], GameManager.instance.player_pos[1]));
        Vector2 start_pos = camera_controller.instance.current_camera.WorldToScreenPoint(player_pos);
        start_pos = Transpos_UI(start_pos);
        flyingUI.rectTransform.anchoredPosition = start_pos;
        for (int cnt = 0; cnt < 20; cnt++)
        {
            flyingUI.rectTransform.localScale +=new Vector3(0.005f*Mathf.Abs(cnt-10), 0.005f * Mathf.Abs(cnt - 10), 0.005f * Mathf.Abs(cnt - 10));
            yield return new WaitForEndOfFrame();
        }
        //fly
        for (int cnt=0;cnt<30;cnt++)
        {
            Vector2 moving_vector =(flyDest()-start_pos)/30f+20*new Vector2(Mathf.Sin(cnt * Mathf.PI*2/30),Mathf.Sin(cnt*Mathf.PI * 2 / 30));
            if (cnt < 10)
            {flyingUI.rectTransform.localScale -= new Vector3(0.01f * Mathf.Abs(cnt - 5), 0.01f * Mathf.Abs(cnt - 5), 0.01f * Mathf.Abs(cnt - 5));}
                flyingUI.rectTransform.anchoredPosition += moving_vector;
            yield return new WaitForEndOfFrame();
        }

        flyingUI.gameObject.SetActive(false);

    }
}
