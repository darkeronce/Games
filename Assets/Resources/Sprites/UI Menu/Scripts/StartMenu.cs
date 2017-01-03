using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

	public enum Menu {menuStart, menuGame};
	public Menu menu;


	public void StartScene(){
		if(menu == Menu.menuStart){
			SceneManager.LoadScene ("01");
		}
	}

	public void Restart(){
		SceneManager.LoadScene ("01");
		Time.timeScale = 1f;
		UIManager.LifeCount = 100;

	}

	public void Option(){
		
	}

	public void Credits(){
		
	}

	public void Exit(){
		if(menu == Menu.menuStart){
			Application.Quit();
		}else if(menu == Menu.menuGame){
			SceneManager.LoadScene ("StartMenu");
			Time.timeScale = 1f;
		}
	}
}
