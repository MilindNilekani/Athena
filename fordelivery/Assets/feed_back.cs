using UnityEngine;
using System.Collections;

public class feed_back : MonoBehaviour {
    public int combos;
    public Texture[] combo_textures;
    public Texture[] cheering;

    private Material mat;
    private ParticleSystem particles;
	// Use this for initialization
	void Start () {
       
        particles = GetComponent<ParticleSystem>();
        mat = particles.GetComponent<Renderer>().material;
        if (combos > 1)
            mat.mainTexture = combo_textures[combos-2];
        else
        {
            int i = (Mathf.FloorToInt(Random.value * 10))%3;
            mat.mainTexture = cheering[i];
        }
        Destroy(gameObject, 1f);
    }
	
	// Update is called once per frame
	void Update () {
      /*  if (combos > 1)
            mat.mainTexture = combo_textures[combos];
        else
        {
            int i = (Mathf.FloorToInt(Random.value * 10)) % 3;
            mat.mainTexture = cheering[i];
        }*/
    }
}
