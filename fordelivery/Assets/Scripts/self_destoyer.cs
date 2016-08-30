using UnityEngine;
using System.Collections;

public class self_destoyer : MonoBehaviour {

    public float sec;
    // Use this for initialization
	void Start () {
        Destroy(this.gameObject, sec);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
