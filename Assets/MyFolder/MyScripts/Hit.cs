using UnityEngine;
using System.Collections;

public class Hit : MonoBehaviour {

	public enum ITipoProyectil{
		Simple,
		Area
	}

	public GameObject glowFX;
	public float damage;
	public GameObject target;
	Rigidbody2D body;
	public ITipoProyectil tipoProy;

	// Use this for initialization
	void Start () {

		body = this.GetComponent<Rigidbody2D> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
//			Debug.Log (target.transform.position.x);
//			this.transform.position = new Vector3 (Mathf.Lerp (this.transform.position.x, target.transform.position.x, Time.deltaTime * 1.5f),
//				Mathf.Lerp (this.transform.position.y, target.transform.position.y, Time.deltaTime * 1.5f), -1f);
			Vector3 dir = target.transform.position - this.transform.position ;
			body.velocity = new Vector2 (dir.x, dir.y).normalized * 6f;
		}
	}

	void OnTriggerEnter2D(Collider2D coll){

		if (coll.tag == "Enemy") {
			if (tipoProy == ITipoProyectil.Simple) {
				coll.gameObject.GetComponent<EnemyScript> ().takeDamage (damage);
			} else {
				Instantiate (glowFX, coll.transform.position, coll.transform.rotation);
				Collider2D[] others = Physics2D.OverlapCircleAll (new Vector2 (coll.gameObject.transform.position.x, coll.transform.position.y), 2f,1<<LayerMask.NameToLayer("Shootable"));
				foreach (Collider2D i in others) {
					i.gameObject.GetComponent<EnemyScript> ().takeDamage (damage);
				}
			}
			Destroy (this.gameObject);
		}
	}
//

}
