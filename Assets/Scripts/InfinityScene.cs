using UnityEngine;
using System.Collections;

public class InfinityScene : MonoBehaviour {

	public GameObject[] scenariosBasic;
	public GameObject[] scenariosMedium;
	public GameObject[] scenariosHard;
	public GameObject startSceneario;
	public GameObject turba;
	public bool isInstantiateTurba;
	public Transform posTurba;
	public int counToAppear = 0;
	GameObject scenario;
	public GameObject newPos;
	public int random;
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "MainCamera" && gameObject.tag == "Create"){
			counToAppear++;

			random = Random.Range (0,scenariosBasic.Length);

			scenario = Instantiate (scenariosBasic[random],newPos.transform.position,Quaternion.identity) as GameObject;

			scenario.name = "infinite";
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "MainCamera"){
			Destroy (startSceneario,5f);
			GetComponent<BoxCollider2D> ().enabled = false;
			if(counToAppear >= 10){
				isInstantiateTurba = true;
				if(isInstantiateTurba)
				Instantiate (turba, posTurba.position, Quaternion.identity);
				isInstantiateTurba = false;
				counToAppear = 0;
			}
		}
	}
}
