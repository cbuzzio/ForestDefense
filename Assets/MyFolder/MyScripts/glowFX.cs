using UnityEngine;
using System.Collections;

public class glowFX : MonoBehaviour {

	SpriteRenderer sprite;
	bool sube = true;
	float alcance;
	public float range{
		get{
			return alcance;
		}
		set{
			alcance = value;
			this.transform.localScale = new Vector3 (value, value, 1f);
			Debug.Log (value);
		
		}
	
	}
	// Use this for initialization
	void Start () {
	
		sprite = this.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (sprite.color.a > 0.6f) {
			sube = false;
		}

		if (sprite.color.a < 0.2f) {
			sube = true;
		}

		if (!sube) {
			sprite.color = new Color (sprite.color.r, sprite.color.g, sprite.color.b, Mathf.Lerp (sprite.color.a, 0f, Time.deltaTime*1.5f));
		} else {
			sprite.color = new Color (sprite.color.r, sprite.color.g, sprite.color.b, Mathf.Lerp (sprite.color.a, 1f, Time.deltaTime));
		}
	
	}
}
