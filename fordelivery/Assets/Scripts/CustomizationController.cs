using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CustomizationController : MonoBehaviour
{
    
    private Vector2 fp; 
    private Vector2 lp;
    
    public Transform markerLeft;
    
    public Transform markerMiddle;
    
    public Transform markerRight;

   

    public Transform markerRight2;
    public Button b_ok;
    public Button b_cancel;
    public Button b_left;
    public Button b_right;
    public Transform[] charsPrefabs;

    public GameObject loading;

    
    private GameObject[] chars;

    

    private Ray ray;
    private RaycastHit hit;
    private int currentChar = 0;

    void StartGame()
    {
        loading.SetActive(true);
        SaveLevel.instance.SetRobotTextureIndex(currentChar);
        Application.LoadLevel("overworld");
    }

    void CancelGame()
    {
        loading.SetActive(false);
        Application.LoadLevel("overworld");
    }

    void MoveLeft()
    {
        currentChar--;

        if (currentChar < 0)
        {
            currentChar = 0;
        }
    }

    void MoveRight()
    {
        currentChar++;

        if (currentChar >= chars.Length)
        {
            currentChar = chars.Length - 1;
        }
    }



    void Start()
    {
        loading.SetActive(false);
        chars = new GameObject[charsPrefabs.Length];
        b_ok.onClick.AddListener(() => StartGame());
        b_cancel.onClick.AddListener(() => CancelGame());
        b_left.onClick.AddListener(() => MoveLeft());
        b_right.onClick.AddListener(() => MoveRight());
        int index = 0;
        foreach (Transform t in charsPrefabs)
        {
            chars[index] = GameObject.Instantiate(t.gameObject, markerRight2.position, Quaternion.EulerAngles(0,135,0)) as GameObject;
            chars[index].transform.localScale = new Vector3(4, 4, 4);
            index++;
        }
        
    }

   /*void OnGUI()
    {

        if (GUI.Button(new Rect(10, (Screen.height - 50) / 2, 100, 50), "Previous"))
        {
            currentChar--;

            if (currentChar < 0)
            {
                currentChar = 0;
            }
        }


        if (GUI.Button(new Rect(Screen.width - 100 - 10, (Screen.height - 50) / 2, 100, 50), "Next"))
        {
            currentChar++;

            if (currentChar >= chars.Length)
            {
                currentChar = chars.Length - 1;
            }
        }


        GameObject selectedChar = chars[currentChar];
        string labelChar = selectedChar.name;
        //GUI.Label(new Rect((Screen.width - 100) / 2, 20, 100, 50), labelChar);

    }*/
        void Update()
            {
        /*if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount > 1)
            {
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        fp = touch.position;
                        lp = touch.position;
                    }
                    if (touch.phase == TouchPhase.Moved)
                    {
                        lp = touch.position;
                    }
                    if (touch.phase == TouchPhase.Ended)
                    {
                        if ((fp.x - lp.x) > 80)
                        {
                            currentChar++;

                            if (currentChar >= chars.Length)
                            {
                                currentChar = chars.Length - 1;
                            }
                        }
                        else if ((fp.x - lp.x) < -80)
                        {
                            currentChar--;

                            if (currentChar < 0)
                            {
                                currentChar = 0;
                            }
                        }

                    }
                }
            }
           
        }*/


        int middleIndex = currentChar;
        
        int leftIndex = currentChar - 1;
       
        int rightIndex = currentChar + 1;

     
        for (int index = 0; index < chars.Length; index++)
        {
            Transform transf = chars[index].transform;

            
            
            if (index > rightIndex)
            {
                transf.position = Vector3.Lerp(transf.position, markerRight2.position, Time.deltaTime*10);
                
                
            }
            else if (index == leftIndex)
            {
                transf.position = Vector3.Lerp(transf.position, markerLeft.position, Time.deltaTime*10);

               
            }
            else if (index == middleIndex)
            {
                transf.position = Vector3.Lerp(transf.position, markerMiddle.position, Time.deltaTime*10);
                if (chars[currentChar].tag == "yellow")
                {
                    chars[currentChar].GetComponent<Animator>().SetBool("is_attacking", true);
                    chars[currentChar].GetComponent<Animator>().speed = 0.5f;
                }
                else if (chars[currentChar].tag == "purple")
                {
                    chars[currentChar].GetComponent<Animator>().SetBool("is_moving", true);
                    chars[currentChar].GetComponent<Animator>().speed = 0.5f;
                }
                else if (chars[currentChar].tag == "green")
                {
                    chars[currentChar].GetComponent<Animator>().SetBool("powerup", true);
                    chars[currentChar].GetComponent<Animator>().speed = 4f;
                }
                else
                {
                    chars[currentChar].GetComponent<Animator>().SetBool("combat", true);
                    chars[currentChar].GetComponent<Animator>().speed = 2f;
                }
            }
            else if (index == rightIndex)
            {
                transf.position = Vector3.Lerp(transf.position, markerRight.position, Time.deltaTime*10);
                
            }
        }
    }

}
