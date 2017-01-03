using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountDown : MonoBehaviour {
	public static float countDown = 9;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		countDown -= Time.deltaTime;
		if(countDown >= 0){
			GetComponent<Text> ().text = Mathf.RoundToInt(countDown).ToString();
		}
	}
}
