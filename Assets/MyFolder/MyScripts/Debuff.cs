using UnityEngine;
using System.Collections;

public class Debuff : MonoBehaviour {

	float duration;

	public void setDuration(float time){
		duration = time;
	}
	float timer;
	public float percentSpeed;
	public float percentArmor;
	bool timeRunning = false;
	float disminucionVel;
	float disminucionArmadura;

	public void initiate(){
		if (duration != 0) {
			timeRunning = true;
			timer = duration;
		}
	}

	EnemyScript control;
	// Use this for initialization
	void Start () {
		control = this.gameObject.GetComponent<EnemyScript> ();
		disminucionVel = control.velocity * percentSpeed;
		disminucionArmadura = control.armor * percentArmor;
		control.armor -= disminucionArmadura;
		control.velocity -= disminucionVel;
	}
	
	// Update is called once per frame
	void Update () {

		if (timeRunning == true) {
		
			if (timer <= 0) {

				control.armor += disminucionArmadura;
				control.velocity += disminucionVel;
				Destroy (this);
			} else {
				timer -= Time.deltaTime;
			}
		}
	}
}
