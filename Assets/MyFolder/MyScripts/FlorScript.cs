using UnityEngine;
using System.Collections;

public class FlorScript : MonoBehaviour {

	public enum ITipoFlor{
		Comun,
		Rango,
		Daño
	}

	float range = 0.5f;

	public Sprite[] spriteFlores;
	SpriteRenderer sprite;

	public ITipoFlor tipoFlor;

	float RangeBuff;
	float DamageBuff;
	// Use this for initialization

	float timerAttack;

	public void setTipo(ITipoFlor nuevoTipo){
		tipoFlor = nuevoTipo;
		switch (tipoFlor) {
		case ITipoFlor.Comun:
			RangeBuff = 0;
			DamageBuff = 0;
			range = 2f;
			sprite.sprite = spriteFlores [0];
			break;

		case ITipoFlor.Daño:
			RangeBuff = 0;
			DamageBuff = 0.25f;
			range = 2f;
			sprite.sprite = spriteFlores [2];
			break;

		case ITipoFlor.Rango:
			RangeBuff = 0.5f;
			DamageBuff = 0f;
			range = 2f;
			sprite.sprite = spriteFlores [1];
			break;
		}
	}

	void Start () {
		sprite = this.GetComponent<SpriteRenderer> ();
		this.setTipo (ITipoFlor.Comun);
	}

	void Update(){

		if (tipoFlor != ITipoFlor.Comun) {
			foreach (Collider2D i in Physics2D.OverlapCircleAll(this.gameObject.transform.position,0.5f,1<<LayerMask.NameToLayer("Plantas"))) {
		
				if (i.gameObject.GetComponent<Buff> () == null) {
//					Debug.Log (i);
					Buff mejora = i.gameObject.AddComponent<Buff> ();
					mejora.percentRange = RangeBuff;
					mejora.percentDamage = DamageBuff;
			
				}
		
			}
		}
	}



}
