using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PointsScript : MonoBehaviour {

	private int temp_score;
    private int stages;
    private Image flash;
    public Sprite flashing;
    public Sprite nflashing;
    // Use this for initialization
	void Start () {
        flash = transform.parent.GetComponent<Image>();
        stages = 0;
	}
	
	// Update is called once per frame
	void Update () {

        //this.gameObject.GetComponent<Text>().text =GameManager.instance.level_Points.ToString();
        if (temp_score != GameManager.instance.level_Points && (stages == 0))
        {
            StartCoroutine("AddScore");
            flash.sprite = flashing;
        }
        else if (stages == 1)
        { GetComponent<Text>().text = temp_score.ToString(); }
    }

    IEnumerator AddScore()
    {
        stages = 1;
        int score_gap = GameManager.instance.level_Points - temp_score;
        int frames = 20;
        for (int cnt = 0; cnt < frames; cnt++)
        {
            temp_score += score_gap/frames;
            GetComponent<Text>().text = temp_score.ToString();
            yield return new WaitForEndOfFrame();
        }
        flash.sprite = nflashing;
        stages = 0;
    }
}
