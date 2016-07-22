using UnityEngine;
using System.Collections;

public class EnemyCreator : MonoBehaviour {

	public GameObject enemy;
	float timer;
	float cd = 1.5f;
	public GameObject inicio;
	// Use this for initialization
	void Start () {
	
		timer = cd;

	}
	
	// Update is called once per frame
	void Update () {

		if (timer <= 0) {
			timer = cd;
			Instantiate (enemy, inicio.transform.position, Quaternion.LookRotation(inicio.transform.forward));
		} else {

			timer -= Time.deltaTime;
		
		}
	
	}
}
