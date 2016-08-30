using UnityEngine;
using System.Collections;


public class SoundManager : MonoBehaviour {


	public static SoundManager instance=null;

	public AudioSource SFX1;
	public AudioSource SFX2;
	public AudioSource SFX3;
	public AudioSource dialoge;
    public AudioSource BGM;

    public AudioClip clickedlockedLevel;
    public AudioClip clickedUnlockedLevel_hold;
    public AudioClip clickedUnlockedLevel_release;
    public AudioClip scrollingSound;
    public AudioClip shimmerSound;
	public AudioClip BGM_overworld;

	public AudioClip Explo;
	//public AudioClip PowerSound;
	public AudioClip monstersound;
	public AudioClip righttap;
	public AudioClip wrongtap;
	public AudioClip EnemyDistroyed;
	public AudioClip Player_gun;
	public AudioClip Player_walk;
	public AudioClip[] BGM_gameplay;
	public AudioClip[] player_talk;
	public AudioClip enemystunned;
	public AudioClip rampagevoice;

	public Sprite[] anim_circle;


    
	void Awake ()
    {
		if (instance == null)
			instance = this;
		else
			if (instance != this)
			Destroy (this.gameObject);
		DontDestroyOnLoad(gameObject);
        if(Application.loadedLevel==0)
        {
            playBGM(BGM_overworld);
        }
    }
	

	public void playBGM(AudioClip Clip)
	{
        BGM.clip = Clip;
		BGM.loop=true;
        BGM.volume = 0.2f; ;
		BGM.Play ();

	}

    public void playSFX(AudioClip Clip)
    {
        if (!(SFX1.isPlaying))
        {
            SFX1.clip = Clip;
            SFX1.Play();
        }
        else if (!(SFX2.isPlaying))
		{
            SFX2.clip = Clip;
            SFX2.Play();
        }
		else
		{
			SFX3.clip = Clip;
			SFX3.Play();
		}

    }
	public void playdialoge(AudioClip Clip)
	{
		dialoge.clip = Clip;
		dialoge.Play();

	}

}
