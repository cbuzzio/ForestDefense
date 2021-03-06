﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MouseManager : MonoBehaviour {

	public GameObject arbolBase;
	public GameObject arbustoBase;
	public GameObject florBase;
	public GameObject seleccionPanel;
	public GameObject mejoraPanel;
	public GameObject[] botones;
	Collider2D selectedTile;
	Collider2D mejoraTile;
	resourcesManager semillas;
	public GameObject bosque;
	SpriteRenderer[] sprites;
	public GameObject Druida;
	public GameObject flareFX_crear;
	public GameObject flareFX_mejora;

	// Use this for initialization
	void Start () {
		semillas =	this.gameObject.GetComponent<resourcesManager> ();
		sprites = bosque.GetComponentsInChildren<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		if (Input.GetMouseButtonDown (0)) {
			if (mejoraPanel.activeSelf == false) {
				if (seleccionPanel.activeSelf == false) {
					selectedTile = Physics2D.OverlapPoint (new Vector2 (mousePos.x, mousePos.y), 1 << LayerMask.NameToLayer ("GreenTiles"));
				}

				Collider2D isUI = Physics2D.OverlapPoint (new Vector2 (Input.mousePosition.x, Input.mousePosition.y), 1 << LayerMask.NameToLayer ("UI"));

				if (isUI == null) {
					if (seleccionPanel.activeSelf == false) {
						if (selectedTile != null) {
							botones [0].GetComponent<Button> ().interactable = semillas.SemillasArbol > 0;
							botones [1].GetComponent<Button> ().interactable = semillas.SemillasFlor > 0;
							botones [2].GetComponent<Button> ().interactable = semillas.SemillasArbusto > 0;
							seleccionPanel.SetActive (true);
							Vector3 panelPos = Camera.main.WorldToScreenPoint (selectedTile.transform.position + new Vector3 (0.7f, 0.5f, 0));
							seleccionPanel.GetComponent<RectTransform> ().position = new Vector2 (panelPos.x, panelPos.y);
						
						}
					} else {
						seleccionPanel.SetActive (false);
					}
				}
			}
		}

		if (Input.GetMouseButtonDown (1)) {

			if (mejoraPanel.activeSelf == false) {
				mejoraTile = Physics2D.OverlapPoint (new Vector2 (mousePos.x, mousePos.y), 1 << LayerMask.NameToLayer ("GreenTiles"));
				if (mejoraTile != null) {
					if (mejoraTile.GetComponent<tileScript> ().ocupada == true) {
						mejoraPanel.SetActive (true);
					}
				}
			} else {
				mejoraPanel.SetActive (false);
			}

		}


		if (Physics2D.OverlapPoint (new Vector2 (mousePos.x, mousePos.y), 1 << LayerMask.NameToLayer ("Forest")) != null) {
			if (sprites[0].color.a > 0.2f) {
				foreach (SpriteRenderer i in sprites) {
					i.color = new Color (i.color.r, i.color.g, i.color.b, Mathf.Lerp(i.color.a, 0f,Time.deltaTime*5f));
				}
			}


		} else {
			if (sprites[0].color.a < 1f) {
				foreach (SpriteRenderer i in sprites) {
					i.color = new Color (i.color.r, i.color.g, i.color.b, Mathf.Lerp (i.color.a, 1.5f, Time.deltaTime*5f));
				}
			}
		}
			

	}

	IEnumerator aparecer(GameObject original, Collider2D selectedTile){
		GameObject flareGO = (GameObject)Instantiate(flareFX_crear,selectedTile.transform.position,Quaternion.identity);
		LensFlare flare = flareGO.GetComponent<LensFlare> ();
		while(flare.brightness < 1.5f){
			flare.brightness = flare.brightness + 0.05f;
			yield return null;
		}
		yield return new WaitForSeconds (0.5f);
		selectedTile.GetComponent<tileScript>().planta = (GameObject)Instantiate (original, selectedTile.transform.position, original.transform.rotation);
		while (flare.brightness > 0) {
			flare.brightness = flare.brightness - 0.15f;
			yield return null;
		}

	}

	IEnumerator mejorar(Collider2D mejoraTile, int mejora){
		GameObject flareGO = (GameObject)Instantiate(flareFX_mejora,mejoraTile.transform.position,Quaternion.identity);
		LensFlare flare = flareGO.GetComponent<LensFlare> ();
		while(flare.brightness < 3f){
			flare.brightness = flare.brightness + 0.1f;
			yield return null;
		}
		yield return new WaitForSeconds (0.5f);
		switch (mejora) {
		case 0:
			mejoraTile.GetComponent<tileScript> ().planta.GetComponent<ArbolScript> ().setTipo (ArbolScript.ITipoArbol.DañoSimple);
			break;
		case 1:
			mejoraTile.GetComponent<tileScript> ().planta.GetComponent<ArbolScript> ().setTipo (ArbolScript.ITipoArbol.DañoArea);
			break;
		case 2:
			mejoraTile.GetComponent<tileScript> ().planta.GetComponent<FlorScript> ().setTipo (FlorScript.ITipoFlor.Rango);
			break;
		case 3:
			mejoraTile.GetComponent<tileScript> ().planta.GetComponent<FlorScript> ().setTipo (FlorScript.ITipoFlor.Daño);
			break;
		case 4:
			mejoraTile.GetComponent<tileScript> ().planta.GetComponent<ArbustoScript> ().setTipo (ArbustoScript.ITipoArbusto.Ralentizador);
			break;
		case 5:
			mejoraTile.GetComponent<tileScript> ().planta.GetComponent<ArbustoScript> ().setTipo (ArbustoScript.ITipoArbusto.Debilitador);
			break;
		}
		while (flare.brightness > 0) {
			flare.brightness = flare.brightness - 0.15f;
			yield return null;
		}

	}

	public void crearArbol(){
		
		if (selectedTile.GetComponent<tileScript> ().ocupada == false) {
//			selectedTile.GetComponent<tileScript>().planta = (GameObject)Instantiate (arbolBase, selectedTile.transform.position, arbolBase.transform.rotation);
			StartCoroutine(aparecer(arbolBase,selectedTile));
			selectedTile.GetComponent<tileScript> ().ocupada = true;
			selectedTile.GetComponent<tileScript> ().tipo = tileScript.ITipoPlanta.Arbol;
			semillas.SemillasArbol--;
			Druida.GetComponent<Animator> ().SetBool ("Magia", true);
		}
	}

	public void crearFlor(){

		if (selectedTile.GetComponent<tileScript> ().ocupada == false) {
//			selectedTile.GetComponent<tileScript>().planta = (GameObject)Instantiate (florBase, selectedTile.transform.position, florBase.transform.rotation);
			StartCoroutine(aparecer(florBase,selectedTile));
			selectedTile.GetComponent<tileScript> ().ocupada = true;
			selectedTile.GetComponent<tileScript> ().tipo = tileScript.ITipoPlanta.Flor;
			Druida.GetComponent<Animator> ().SetBool ("Magia", true);

			semillas.SemillasFlor--;
		}

	}

	public void crearArbusto(){

		if (selectedTile.GetComponent<tileScript> ().ocupada == false) {
//			selectedTile.GetComponent<tileScript>().planta = (GameObject)Instantiate (arbustoBase, selectedTile.transform.position, arbustoBase.transform.rotation);
			StartCoroutine(aparecer(arbustoBase,selectedTile));
			selectedTile.GetComponent<tileScript> ().ocupada = true;
			selectedTile.GetComponent<tileScript> ().tipo = tileScript.ITipoPlanta.Arbusto;
			Druida.GetComponent<Animator> ().SetBool ("Magia", true);

			semillas.SemillasArbusto--;
		}

	}

	//TODO: terminar el script
	public void mejorarArbolSimple(){
		if (mejoraTile != null) {
			if (mejoraTile.GetComponent<tileScript> ().tipo == tileScript.ITipoPlanta.Arbol) {
				if (mejoraTile.GetComponent<tileScript> ().planta.GetComponent<ArbolScript> ().tipoArbol == ArbolScript.ITipoArbol.Comun) {
					StartCoroutine (mejorar (mejoraTile,0));
//					mejoraTile.GetComponent<tileScript> ().planta.GetComponent<ArbolScript> ().setTipo (ArbolScript.ITipoArbol.DañoSimple);
					Druida.GetComponent<Animator> ().SetBool ("Magia", true);
				}
			}
		}
		mejoraPanel.SetActive (false);
			
	}

	public void mejorarArbolArea(){
		if (mejoraTile != null) {
			if (mejoraTile.GetComponent<tileScript> ().tipo == tileScript.ITipoPlanta.Arbol) {
				if (mejoraTile.GetComponent<tileScript> ().planta.GetComponent<ArbolScript> ().tipoArbol == ArbolScript.ITipoArbol.Comun) {
					StartCoroutine (mejorar (mejoraTile,1));
//					mejoraTile.GetComponent<tileScript> ().planta.GetComponent<ArbolScript> ().setTipo (ArbolScript.ITipoArbol.DañoArea);
					Druida.GetComponent<Animator> ().SetBool ("Magia", true);

				}
			}
		}
		mejoraPanel.SetActive (false);
	}
	public void mejorarFlorRango(){
		if (mejoraTile != null) {
			if (mejoraTile.GetComponent<tileScript> ().tipo == tileScript.ITipoPlanta.Flor) {
				if (mejoraTile.GetComponent<tileScript> ().planta.GetComponent<FlorScript> ().tipoFlor == FlorScript.ITipoFlor.Comun) {
					StartCoroutine (mejorar (mejoraTile,2));
//					mejoraTile.GetComponent<tileScript> ().planta.GetComponent<FlorScript> ().setTipo (FlorScript.ITipoFlor.Rango);
					Druida.GetComponent<Animator> ().SetBool ("Magia", true);
				}
			}
		}
		mejoraPanel.SetActive (false);
	}
	public void mejorarFlorDaño(){
		if (mejoraTile != null) {
			if (mejoraTile.GetComponent<tileScript> ().tipo == tileScript.ITipoPlanta.Flor) {
				if (mejoraTile.GetComponent<tileScript> ().planta.GetComponent<FlorScript> ().tipoFlor == FlorScript.ITipoFlor.Comun) {
					StartCoroutine (mejorar (mejoraTile,3));
//					mejoraTile.GetComponent<tileScript> ().planta.GetComponent<FlorScript> ().setTipo (FlorScript.ITipoFlor.Daño);
					Druida.GetComponent<Animator> ().SetBool ("Magia", true);
				}
			}
		}
		mejoraPanel.SetActive (false);
	}
	public void mejorarArbRalent(){
		if (mejoraTile != null) {
			if (mejoraTile.GetComponent<tileScript> ().tipo == tileScript.ITipoPlanta.Arbusto) {
				if (mejoraTile.GetComponent<tileScript> ().planta.GetComponent<ArbustoScript> ().tipoArbusto == ArbustoScript.ITipoArbusto.Comun) {
					StartCoroutine (mejorar (mejoraTile,4));
//					mejoraTile.GetComponent<tileScript> ().planta.GetComponent<ArbustoScript> ().setTipo (ArbustoScript.ITipoArbusto.Ralentizador);
					Druida.GetComponent<Animator> ().SetBool ("Magia", true);

				}
			}
		}
		mejoraPanel.SetActive (false);
	}
	public void mejorarArbDeb(){
		if (mejoraTile != null) {
			if (mejoraTile.GetComponent<tileScript> ().tipo == tileScript.ITipoPlanta.Arbusto) {
				if (mejoraTile.GetComponent<tileScript> ().planta.GetComponent<ArbustoScript> ().tipoArbusto == ArbustoScript.ITipoArbusto.Comun) {
					StartCoroutine (mejorar (mejoraTile,5));
//					mejoraTile.GetComponent<tileScript> ().planta.GetComponent<ArbustoScript> ().setTipo (ArbustoScript.ITipoArbusto.Debilitador);
					Druida.GetComponent<Animator> ().SetBool ("Magia", true);

				}
			}
		}
		mejoraPanel.SetActive (false);
	}

}
