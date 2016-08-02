using UnityEngine;
using System.Collections;

public class explodeFX_Flare : MonoBehaviour {

	LensFlare flare;
	// Use this for initialization
	void Start () {
		flare = this.GetComponent<LensFlare> ();
		flare.brightness = 0.2f;
		StartCoroutine (grow ());
	}
	
	// Update is called once per frame
	void Update () {
	
//		flare.brightness = Mathf.Lerp (flare.brightness, 1f, Time.deltaTime * 5f);
//		if (flare.brightness > 0.4f) {
//			Destroy (this.gameObject);
//		}
//	
	}

	IEnumerator grow(){
	
		while (flare.brightness < 0.5f) {
			flare.brightness += 0.05f;
			flare.color = new Color (flare.color.r - 0.1f, 0, 0);
			yield return null;
		}

		yield return new WaitForSeconds (0.1f);
		Destroy (this.gameObject);
	
	}
}
