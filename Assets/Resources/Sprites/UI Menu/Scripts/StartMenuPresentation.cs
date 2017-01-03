using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartMenuPresentation : MonoBehaviour {

	Camera camerasize;
	float timeDesapear = 0f;

	// Use this for initialization
	void Start () {
		camerasize = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		timeDesapear += Time.deltaTime;
		if (timeDesapear <= 5f) {
			camerasize.orthographicSize = timeDesapear;
		} else if(timeDesapear >= 8f){
			GetComponentInChildren<SpriteRenderer> ().enabled = false;
			SceneManager.LoadScene ("StartMenu");
		}
	}
}
