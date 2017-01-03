using UnityEngine;
using System.Collections;

public class ItemManager : MonoBehaviour {

	Animator anim;
	ScoreManager managerScore;
	public float impulse;


	void Start(){
		anim = GetComponent<Animator> ();
		managerScore = GameObject.FindGameObjectWithTag("UI").GetComponent<ScoreManager> ();
	}

	public enum TypeItem{
		coin = 20,
		box,
		ceiling,
		woodCeiling
	}

	public TypeItem itemType;

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player" && itemType == TypeItem.coin){
			GetComponent<AudioSource> ().Play ();
			GetComponent<CircleCollider2D> ().enabled = false;
			anim.SetFloat ("GetIt", 1f);
			Destroy (gameObject, 0.4f);
			managerScore.score += TypeItem.coin;
		}
	}

	void OnCollisionStay2D(Collision2D other){
		if(other.gameObject.tag == "Player" && itemType == TypeItem.box && other != null){
			other.gameObject.GetComponent<FrankManager> ().forceJump = impulse;
		}
	}

}
