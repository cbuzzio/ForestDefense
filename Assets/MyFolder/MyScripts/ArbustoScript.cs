using UnityEngine;
using System.Collections;

public class ArbustoScript : MonoBehaviour {

	public enum ITipoArbusto{
		Comun,
		Debilitador,
		Ralentizador
	}

	CircleCollider2D alcance;
	public GameObject area;
	public float range{
		get{
			return alcance.radius;
		}

		set{
			if (value > 0) {
				alcance.radius = value;
				alcance.enabled = true;
			} else {
				alcance.enabled = false;
			}
		}
	}

	public ITipoArbusto tipoArbusto;


	public GameObject[] bayas;
	[System.NonSerialized]
	public float SpeedDebuff = 0.4f;
	[System.NonSerialized]
	public float ArmorDebuff = 0.25f;
	float duration = 2f;
	// Use this for initialization

	float timerAttack;

	public void setTipo(ITipoArbusto nuevoTipo){
		tipoArbusto = nuevoTipo;
		switch (tipoArbusto) {
		case ITipoArbusto.Comun:
			SpeedDebuff = 0;
			ArmorDebuff = 0;
			range = 1.5f;
			foreach (GameObject i in bayas) {
				i.SetActive (false);
			}
			area.SetActive (false);
			break;

		case ITipoArbusto.Debilitador:
			SpeedDebuff = 0;
			ArmorDebuff = 0.25f;
			range = 1.5f;
			duration = 2f;
			foreach (GameObject i in bayas) {
				i.SetActive (true);
				i.GetComponent<SpriteRenderer> ().color = new Color (1f, 0, 1f, 1f);
			}
			area.SetActive (true);
			break;

		case ITipoArbusto.Ralentizador:
			SpeedDebuff = 0.4f;
			ArmorDebuff = 0f;
			range = 1.5f;
			duration = 2f;
			foreach (GameObject i in bayas) {
				i.SetActive (true);
				i.GetComponent<SpriteRenderer> ().color = Color.blue;
			}
			area.SetActive (true);
			break;
		}
	}

	void Start () {
		alcance = this.GetComponent<CircleCollider2D> ();
		this.setTipo (ITipoArbusto.Comun);
		area.GetComponent<glowFX> ().range = range;
	}

	//
	void OnTriggerEnter2D(Collider2D coll){


		if (coll.tag == "Enemy") {
//			Debug.Log ("DebuffAplicado");
			Debuff actualDebuff = coll.gameObject.AddComponent<Debuff> ();
			actualDebuff.percentArmor = ArmorDebuff;
			actualDebuff.percentSpeed = SpeedDebuff;
			actualDebuff.setDuration(duration);
		}


	}

	void OnTriggerExit2D(Collider2D coll){

		if (coll.tag == "Enemy") {
			Debuff actualDebuff = coll.gameObject.GetComponent<Debuff> ();
			if (actualDebuff != null) {
				actualDebuff.initiate ();
			}
		}

	}


}

