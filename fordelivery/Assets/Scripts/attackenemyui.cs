using UnityEngine;
using System.Collections;

public class attackenemyui : MonoBehaviour {

    private float init_size = 0.1f;
    private float finish_size = 4f;
    public Material mat;

	// Use this for initialization
	void Start () {
        transform.GetChild(2).gameObject.SetActive(false);
        StartCoroutine("attackobject");
        StartCoroutine("opening");
        StartCoroutine("rotating");
        StartCoroutine("scanning");
    }
	
	// Update is called once per frame
	IEnumerator attackobject() {
        yield return new WaitForEndOfFrame();
       mat = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material;
        while (true)
        {
            mat.SetColor("_TintColor",new Color(0.3f,0.3f,0.3f));
            yield return new WaitForSeconds(Random.value/10);
            mat.SetColor("_TintColor", new Color(0.5f, 0.5f, 0.5f));
            yield return new WaitForSeconds(Random.value);
        }
	
	}

    IEnumerator opening()
    {
        for (int cnt = 0; cnt < 39; cnt++)
        { transform.GetChild(0).localScale = new Vector3(6f, init_size + 0.1f * cnt, 6f);
            yield return new WaitForEndOfFrame();
        }
        transform.GetChild(2).gameObject.SetActive(true);
    }

    IEnumerator rotating()
    {
        while (true)
        { transform.Rotate(0,2f,0f);
            yield return new WaitForEndOfFrame();
        }

    }

    IEnumerator scanning()
    {
        while (true)
        {
            for (int cnt = 0; cnt <90; cnt++)
            {
                transform.GetChild(2).localPosition =new Vector3(0f,cnt*(4.5f/90),0f);
                yield return new WaitForEndOfFrame();
            }
        }

    }
}
