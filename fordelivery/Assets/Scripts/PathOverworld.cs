using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PathOverworld : MonoBehaviour {
    public int i;

    public Sprite descending_unlocked;
    public Sprite descending_locked;
    public Sprite ascending_unlocked;
    public Sprite ascending_locked;
    // Use this for initialization
    void Start () {
        GameObject image_conn = transform.GetChild(5).gameObject;
        if (i == 30)
        {
            image_conn.SetActive(false);
        }
        if ((i % 4 == 1 || i % 4 == 2) && i < 30)
        {
            if (SaveScores.instance.CheckLevelStatus(i + 1))
            {
                image_conn.GetComponent<Image>().sprite = descending_unlocked;
            }
            else
            {
                image_conn.GetComponent<Image>().sprite = descending_locked;
            }

        }
        else if ((i % 4 == 0 || i % 4 == 3) && i < 30)
        {
            if (SaveScores.instance.CheckLevelStatus(i + 1))
            {
                image_conn.GetComponent<Image>().sprite = ascending_unlocked;
            }
            else
            {
                image_conn.GetComponent<Image>().sprite = ascending_locked;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
