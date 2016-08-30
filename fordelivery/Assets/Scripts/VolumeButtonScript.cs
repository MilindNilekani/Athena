using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VolumeButtonScript : MonoBehaviour {
    SpriteState st = new SpriteState();
    bool mute=false;
    Button b1;
    public Sprite volume_up;
    public Sprite volume_down;
    public Sprite mute_up;
    public Sprite mute_down;
	// Use this for initialization
	void Start () {
        b1 = GetComponent<Button>();
        if (SoundManager.instance.BGM.volume==0.0f)
        {
            gameObject.GetComponent<Image>().sprite = mute_up;
            st.pressedSprite = mute_down;
            gameObject.GetComponent<Button>().spriteState = st;
            b1.onClick.RemoveAllListeners();
            b1.onClick.AddListener(() => MakeFullVolume());
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = volume_up;
            st.pressedSprite = volume_down;
            gameObject.GetComponent<Button>().spriteState = st;
            b1.onClick.RemoveAllListeners();
            b1.onClick.AddListener(() => MakeVolumeMute());
        }
	}

    void MakeFullVolume()
    {
        SoundManager.instance.BGM.volume = 0.02f;
    }

    void MakeVolumeMute()
    {
        SoundManager.instance.BGM.volume = 0.0f;
    }

	
	// Update is called once per frame
	void Update () {
        b1 = GetComponent<Button>();
        if (SoundManager.instance.BGM.volume == 0.0f)
        {
            gameObject.GetComponent<Image>().sprite = mute_up;
            st.pressedSprite = mute_down;
            gameObject.GetComponent<Button>().spriteState = st;
            b1.onClick.RemoveAllListeners();
            b1.onClick.AddListener(() => MakeFullVolume());
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = volume_up;
            st.pressedSprite = volume_down;
            gameObject.GetComponent<Button>().spriteState = st;
            b1.onClick.RemoveAllListeners();
            b1.onClick.AddListener(() => MakeVolumeMute());
        }
    }
}
