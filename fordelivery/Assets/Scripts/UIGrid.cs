using UnityEngine;
using System.Collections;

public class UIGrid : MonoBehaviour {

	// Use this for initialization
	private Color c1;
	private Color c2;

	void Start () {
			StartCoroutine("Sparkle");
	}
	
	// Update is called once per frame
	void Update(){

        bool noenemythere = true;
        if (GameManager.instance.enemy_stun)
        {
            for (int cnt = 0; cnt < GameManager.instance.enemy_number; cnt++)
            {
                if (GameManager.instance.Enemy_die[cnt] == false)
                {
                    Transform enem = GameManager.instance.enemy.transform.GetChild(cnt);
                    if ((Mathf.Abs(transform.position.x - enem.position.x) < 1f )&& (Mathf.Abs(transform.position.z- enem.position.z) < 1f))
                    { c1 = new Color(1f, 0f, 0f, 1f); c2 = c1; noenemythere = false; }
                }
            }
        }

        if (noenemythere)
        {
            Vector3 gridpos = grid.instance.calcWorldCoord(new Vector2(PlayerManager.instance.intend_pos[0],
                                                                     PlayerManager.instance.intend_pos[1]));
            if ((transform.position.x == gridpos.x) && (transform.position.z == gridpos.z))
            {
                c1 = new Color(0.5f, 1f, 0.5f, 0.75f);
                c2 = new Color(0.5f, 1f, 0.5f, 1f);
            }
            else
            {
                c1 = new Color(1f, 1f, 1f, 0.75f);
                c2 = new Color(1f, 1f, 1f, 1f);
            }
        }
	
	
	}

	IEnumerator Sparkle() {
		while (true) {

			this.GetComponent<SpriteRenderer>().color = c2;
			yield return new WaitForSeconds(0.4f);
			this.GetComponent<SpriteRenderer>().color = c1;
			yield return new WaitForSeconds(0.4f);
		}
	}
}
