using UnityEngine;
using System.Collections;

public class explodeFX : MonoBehaviour {

	SpriteRenderer sprite;
	// Use this for initialization
	void Start () {
		sprite = this.GetComponent<SpriteRenderer> ();
		this.transform.localScale = new Vector3 (0.1f, 0.1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {

		sprite.color = new Color (1f, 1f, 1f, Mathf.Lerp (sprite.color.a, 1f, Time.deltaTime*5f));
		this.transform.localScale = Vector3.Lerp (this.transform.localScale, new Vector3 (0.8f, 0.8f, 1f), Time.deltaTime *20f);
		if (this.transform.localScale.x > 0.75f) {
			Destroy (this.gameObject);
		}
	
	}
}
