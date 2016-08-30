using UnityEngine;
using System.Collections;

public class changemat : MonoBehaviour {
    public Material objective;
    private MeshRenderer[] rends;
    private SkinnedMeshRenderer[] skin_rends;

	// Use this for initialization
	void Start () {
        rends = GetComponentsInChildren<MeshRenderer>();
        skin_rends = GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (MeshRenderer rend in rends)
        { rend.material = objective; }
        foreach (SkinnedMeshRenderer skin_rend in skin_rends)
        { skin_rend.material = objective; }

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
