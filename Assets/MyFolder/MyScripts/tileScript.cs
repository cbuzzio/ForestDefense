using UnityEngine;
using System.Collections;

public class tileScript : MonoBehaviour {


	public bool ocupada = false;
	public GameObject planta = null;
	public enum ITipoPlanta
	{
		Arbol,
		Arbusto,
		Flor
	}
	public ITipoPlanta tipo;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
