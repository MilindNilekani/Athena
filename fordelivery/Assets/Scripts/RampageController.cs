using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RampageController : MonoBehaviour
{

    public Sprite[] RampageSprites;
    public Image RampageUI;

    private int noOfBuilding;
    private bool isRampage;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {

        noOfBuilding = GameManager.instance.PowerUpBar;
        isRampage = GameManager.instance.Get_PowerUp;
        if (noOfBuilding == 4) { noOfBuilding = 3; }
        if (!isRampage)
        {
            switch (noOfBuilding)
            {

                case 0:
                    RampageUI.sprite = RampageSprites[0];
                    return;
                case 1:
                    RampageUI.sprite = RampageSprites[1];
                    return;
                case 2:
                    RampageUI.sprite = RampageSprites[2];
                    return;
                default:
                    Debug.LogError("Rampage integer value out of bounds!");
                    RampageUI.sprite = RampageSprites[0];
                    return;
            }    
        }
        else
        {
            switch (noOfBuilding)
            {

                case 3:
                    RampageUI.sprite = RampageSprites[3];
                    return;
                case 2:
                    RampageUI.sprite = RampageSprites[4];
                    return;
                case 1:
                    RampageUI.sprite = RampageSprites[5];
                    return;
                default:
                    Debug.LogError("Rampage integer value out of bounds!");
                    RampageUI.sprite = RampageSprites[0];
                    return;
            }
        }
    }
}
