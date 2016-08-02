using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public float vida;
	public GameObject[] path;
	public GameObject next;
	Rigidbody2D body;
	Animator anim;
	public float armor = 2f;
	public float velocity = 1f;
	int i = 0;
	bool llego = false;
	public SpriteRenderer[] parts;
	// Use this for initialization


	void Start () {
		vida = 100f;
		body = this.GetComponent<Rigidbody2D> ();
		anim = this.GetComponent<Animator> ();
		next = path [i];
		parts = this.GetComponentsInChildren<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (vida <= 0) {
			Destroy (this.gameObject);
		}

		if (parts [0].color.b < 1f) {
		
			foreach (SpriteRenderer i in parts) {
				i.color = Color.Lerp (i.color, Color.white, Time.deltaTime * 5f);
				if (i.color.b > 0.9f) {
					i.color = Color.white;
				}
			}
		
		}

		Vector3 dir = next.transform.position - this.transform.position ;
		if (!llego) {
			body.velocity = new Vector2 (dir.x, dir.y).normalized * velocity;

			this.transform.eulerAngles =  new Vector3 (0, 0,Mathf.LerpAngle(this.transform.eulerAngles.z, Mathf.Rad2Deg * Mathf.Atan2(dir.x,-dir.y),Time.deltaTime*7f));
			if (dir.magnitude < 0.1f) {
				if (i + 1 < path.Length) {
					i++;
					next = path [i];

				} else {
					llego = true;
					anim.SetBool ("Pegar", true);
					body.velocity = Vector2.zero;

				}
			}
		}



	}

	public void takeDamage(float damage){
//		Debug.Log (damage);
		vida -= damage / armor;
		foreach (SpriteRenderer i in parts) {
		
			i.color = Color.red;
		
		}
	}
}
