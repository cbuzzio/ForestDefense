using UnityEngine;
using System.Collections;

public class Buff : MonoBehaviour {

	public float percentDamage;
	public float percentRange;
	float aumentoDaño;
	float aumentoRango;
	float aumentoVel;
	float aumentoDeb;
	bool tipo;
	// Use this for initialization
	void Start () {

		if (this.gameObject.GetComponent<ArbolScript> () != null) {
			tipo = false;
		} else {
			tipo = true;
//			Debug.Log ("Es un arbusto");
		}
		this.initiate ();

	}

	public void initiate(){
//		Debug.Log (percentRange + " " + percentDamage);
		if (percentRange != 0 || percentDamage != 0) {
			if (tipo == false) {
				ArbolScript control = this.gameObject.GetComponent<ArbolScript> ();
				aumentoDaño = control.damage * percentDamage;
				aumentoRango = control.range * percentRange;
				control.damage += aumentoDaño;
//				Debug.Log (control.damage);
				control.range += aumentoRango;
			} else {
				ArbustoScript control = this.gameObject.GetComponent<ArbustoScript> ();
				aumentoVel = control.SpeedDebuff * percentDamage;
				aumentoDeb = control.ArmorDebuff * percentDamage;
				aumentoRango = control.range * percentRange;
				control.SpeedDebuff += aumentoVel;
				control.ArmorDebuff += aumentoDeb;
//				Debug.Log (control.SpeedDebuff + " " + control.ArmorDebuff);
				control.range += aumentoRango;
			}
		}
	
	}

	public void terminate(){
		if (tipo) {
			ArbolScript control = this.gameObject.GetComponent<ArbolScript> ();
			control.damage -= aumentoDaño;
			control.range -= aumentoRango;
		} else  {
			ArbustoScript control = this.gameObject.GetComponent<ArbustoScript> ();
			control.SpeedDebuff -= aumentoVel;
			control.ArmorDebuff -= aumentoDeb;
			control.range -= aumentoRango;
		}
		Destroy (this);
	
	}
}
