using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class godseye : MonoBehaviour {

	public static godseye instance=null;

	public Sprite winsprite;
    public Sprite winbg;
    public Sprite losesprite;
    public Sprite losebg;
    public Sprite emptysprite;
    public Sprite star_shine;
    public Sprite[] star_light;
    public Sprite star_grey;
	public Image loading;

    private Image status;//0
    private Image background;//1
    private Button replay;//2
    private Image[] ranking=new Image[3];//3
    private Text totalscore;//4
    private Button next;

    private bool win;
    private bool started_screen;

    void Awake ()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
	}

	// Use this for initialization
	void Start () {
        background= transform.GetChild(0).gameObject.GetComponent<Image>();
        status =transform.GetChild(1).gameObject.GetComponent<Image>();
        replay =  transform.GetChild(2).gameObject.GetComponent<Button>();
        for (int cnt = 0; cnt < 3; cnt++)
        { ranking[cnt] = transform.GetChild(3).GetChild(cnt).GetComponent<Image>(); }
        totalscore=transform.GetChild(4).gameObject.GetComponent<Text>();
        next = transform.GetChild(5).gameObject.GetComponent<Button>();
        replay.onClick.AddListener(next_level);
        next.onClick.AddListener(reload);
        clear_info();
        started_screen =false;
        win = false;
	}


    public void winning()
    {
        //make sure the coroutine is only called for one time 
        if (started_screen == false)
        {
            StartCoroutine("winScreen");
        } 
    }

    public void lose()
    {
        if (started_screen == false)
        {
            StartCoroutine("loseScreen");
        }

    }

    private void reload()
    {
        LevelLoader.instance.level = LevelLoader.instance.current_level;
    }


    private void next_level()
    {
 
			GameManager.instance.ready=false;
			loading.gameObject.SetActive(true);
			SoundManager.instance.playBGM(SoundManager.instance.BGM_overworld);
			Application.LoadLevel("overworld") ;       
      
    }

    public void clear_info()
	{
        for (int cnt = 0; cnt < transform.childCount; cnt++)
        {
            Transform child = transform.GetChild(cnt);
            child.gameObject.SetActive(false);
        }

	}
    public void show_info(string element)
    {
        switch (element)
        {
            case "bgbox":
                transform.GetChild(0).gameObject.SetActive(true);
                return;
            case "status":
                transform.GetChild(1).gameObject.SetActive(true);
                return;
            case "replay_button":
                transform.GetChild(2).gameObject.SetActive(true);
                return;
            case "ranking1":
                show_stats("structures");
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(3).GetChild(0).gameObject.SetActive(true);
                
                return;
            case "ranking2":
                show_stats("bestroute");
                transform.GetChild(3).GetChild(1).gameObject.SetActive(true);
                return;
            case "ranking3":
                show_stats("enemies");
                transform.GetChild(3).GetChild(2).gameObject.SetActive(true);
                return;
            case "totalscore":
                transform.GetChild(4).gameObject.SetActive(true);
                return;
            case "next_button":
                transform.GetChild(5).gameObject.SetActive(true);
                return;
            default:
                return;
        }

    }

    public void show_stats(string element)
    {
        switch(element)
        {
            case "structures":
                transform.GetChild(3).GetChild(0).GetChild(2).GetComponent<Text>().text = GameManager.instance.structures_dest + "/" + GameManager.instance.structures_dest;
                return;
            case "bestroute":
                transform.GetChild(3).GetChild(1).GetChild(2).GetComponent<Text>().text = (GameManager.instance.levelMoves) + "/" + SaveScores.instance.GetOptimalPathValue(LevelLoader.instance.current_level);
                return;
            case "enemies":
                transform.GetChild(3).GetChild(2).GetChild(2).GetComponent<Text>().text = GameManager.instance.EnemiesKilled() + "/" + GameManager.instance.enemy_number;
                return;
            default:
                return;

        }
    }

    public void render_info(string INFO)
	{
		//showing specific sprites on UI
		status.sprite=SpriteFinder(INFO);
		status.SetNativeSize();
	
	}

	public Sprite SpriteFinder(string INFO)
	{
		switch(INFO)
		{
		case "win":
			return winsprite;
		case "lose":
			return losesprite;
		default:
			return emptysprite;
		}
	}

    IEnumerator winScreen()
    {
        win = true;
        started_screen = true;
        yield return new WaitForEndOfFrame();
        show_info("bgbox");
        background.sprite = winbg;
        yield return new WaitForSeconds(0.5f);
        show_info("status");
        render_info("win");
        yield return new WaitForSeconds(0.5f);
      
       
        
        show_info("ranking1");
        show_info("ranking2");
        if (GameManager.instance.stars[2] >= 0) { show_info("ranking3"); }
        yield return new WaitForSeconds(0.2f);
        for (int cnt = 0; cnt < 3; cnt++)
        {
            if (GameManager.instance.stars[cnt] > 0)
            { ranking[cnt].sprite = star_light[GameManager.instance.stars[cnt] - 1];
                yield return new WaitForSeconds(0.2f);
            }
        }
        yield return new WaitForSeconds(0.5f);
        show_info("totalscore");
        totalscore.text = GameManager.instance.level_Points.ToString();
        yield return new WaitForEndOfFrame();
        show_info("replay_button");
        yield return new WaitForEndOfFrame();
        show_info("next_button");
        
    }

    IEnumerator loseScreen()
    {
        started_screen = true;
        win = false;
        yield return new WaitForEndOfFrame();
        show_info("bgbox");
        background.sprite = losebg;
        yield return new WaitForSeconds(0.5f);
        show_info("status");
        render_info("lose");
        yield return new WaitForSeconds(0.5f);
        show_info("replay_button");
        yield return new WaitForEndOfFrame();
        show_info("next_button");
       
    }



}
