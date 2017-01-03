using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public bool isPause;
	public GameObject panel;
	public GameObject panel2;
	public float timeWaitPause;

	public static int LifeCount = 100;
	public Slider life;


	void Update(){
		life.value = LifeCount;


		if(isPause){
			timeWaitPause += Time.deltaTime;

			if (timeWaitPause >= 0.7f && isPause) {
				Time.timeScale = 0f;
				timeWaitPause = 0f;
			}
		} else if(timeWaitPause <= 0.7f && !isPause){
			timeWaitPause = 0f;
		}
	
	}

	public void Pause(){
		isPause = !isPause;
		if(!isPause){
			Time.timeScale = 1f;
		}
		panel.SetActive (isPause);
		panel2.SetActive (!isPause);
	}
		
}
