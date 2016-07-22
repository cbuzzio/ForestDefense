using UnityEngine;
using System.Collections;

public class ArbolScript : MonoBehaviour {
	
	public enum ITipoArbol{
		Comun,
		DañoSimple,
		DañoArea,
		Magico,
	}


	CircleCollider2D alcance;

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

	[System.NonSerialized]
	public float cd = 0.3f;
	[System.NonSerialized]
	public float damage = 10f;

	public ITipoArbol tipoArbol;

	GameObject target;

	public GameObject[] frutas;
	public GameObject proyectil;
	// Use this for initialization

	float timerAttack;

	void Start () {
		alcance = this.GetComponent<CircleCollider2D> ();
		timerAttack = 0;
		target = null;
		this.setTipo (ITipoArbol.Comun);
	}

	void attack(GameObject enemy){
		GameObject disparo = (GameObject)GameObject.Instantiate (proyectil, new Vector3 (this.transform.position.x, this.transform.position.y, -1f), Quaternion.identity);
		if (tipoArbol == ITipoArbol.DañoSimple) {
			disparo.GetComponent<Hit> ().tipoProy = Hit.ITipoProyectil.Simple;
			disparo.GetComponent<SpriteRenderer> ().color = Color.red;
		} else if(tipoArbol == ITipoArbol.DañoArea){
			disparo.GetComponent<Hit> ().tipoProy = Hit.ITipoProyectil.Area;
			disparo.GetComponent<SpriteRenderer> ().color = Color.green;
		}

		disparo.GetComponent<Hit> ().target = target;
		disparo.GetComponent<Hit> ().damage = damage;
	}

//
	void OnTriggerEnter2D(Collider2D coll){

		if (coll.tag == "Enemy" && target == null) {
			target = coll.gameObject;
		}

	}

	void OnTriggerExit2D(Collider2D coll){
	
		if (coll.gameObject.Equals (target) == true) {
			target = null;
		}
	
	}

	public void setTipo(ITipoArbol nuevoTipo){
		tipoArbol = nuevoTipo;
		switch (tipoArbol) {
		case ITipoArbol.Comun:
			damage = 0;
			range = 0;
			cd = 10f;
			foreach (GameObject i in frutas) {
				i.SetActive (false);
			}
			break;

		case ITipoArbol.DañoSimple:
			damage = 40f;
			range = 2f;
			cd = 2f;
			foreach (GameObject i in frutas) {
				i.SetActive (true);
				i.GetComponent<SpriteRenderer> ().color = Color.red;
			}
			break;

		case ITipoArbol.DañoArea:
			damage = 25f;
			range = 1.5f;
			cd = 1.5f;
			foreach (GameObject i in frutas) {
				i.SetActive (true);
				i.GetComponent<SpriteRenderer> ().color = Color.green;
			}
			break;
		}


			
			
	}
	// Update is called once per frame

	void Update () {

		if (target != null) {
			if (timerAttack <= 0) {
				timerAttack = cd;
				attack (target);
//			Debug.Log(target);
			} else {
				timerAttack -= Time.deltaTime;
			}
		}

	}
}
