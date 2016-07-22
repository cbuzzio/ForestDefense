using UnityEngine;
using System.Collections;

public class FlareEffect : MonoBehaviour {

	LensFlare fx;
	public Flare efecto;
	// Use this for initialization
	void Start () {
	
		fx = this.gameObject.AddComponent<LensFlare> ();
		fx.brightness = 3f;
		fx.flare = efecto;
		fx.color = Color.white;
	}
	
	// Update is called once per frame
	void Update () {
		if (fx != null) {
			fx.brightness = Mathf.Lerp (fx.brightness, 0, Time.deltaTime);

			if (fx.brightness < 0.1f) {

				Destroy (fx);
				Destroy (this);

			}
		}
	
	}
}
