using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FrankManager : MonoBehaviour {

	Rigidbody2D rbFrank;
	AudioSource aux;
	Animator anim;
	public BoxCollider2D triggerDead;
	public float radiousFit;
	public float forceJump;
	public bool checkFloor;
	public bool isSwype;
	public GameObject fit;
	public bool isDead;
	bool isHurt;
	public float speed;
	float timeForHurt;
	public float counter,counterReload;
	public Vector2 sizeNormal,sizeSwype;
	public PhysicsMaterial2D iceMaterial;
	public Vector2 offsetNormal, offsetSwype;
	// mobile atributes
	bool swypeGesture;
	float fingerStartTime;
	Vector2 fingerStartPos;
	float swypeGestureMinDist = 1000.0f;
	float swypeGestureMaxTime = 0.5f;
	public GameObject startCount;

	// Use this for initialization
	void Start () {
		rbFrank = GetComponent<Rigidbody2D> ();
		anim = GetComponentInChildren<Animator> ();
		aux = GetComponent<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update () {

		if (!isDead) {
			Movement ();
			if(checkFloor && Input.GetKeyDown(KeyCode.Z)){
				Swype();
			}
			// Swype mobile
			if (Input.touchCount > 0 && Input.touchCount < 2) {
				foreach (Touch touch in Input.touches) {
					switch (touch.phase) {
					case TouchPhase.Began:
						swypeGesture = true;
						fingerStartTime = Time.time;
						fingerStartPos = touch.position;
						break;
					case TouchPhase.Canceled:
						swypeGesture = false;
						break;
					case TouchPhase.Ended:
						float gestureTime = Time.time - fingerStartTime;
						float gestureDist = (touch.position - fingerStartPos).magnitude;

						if (swypeGesture && gestureTime < swypeGestureMaxTime && gestureDist < swypeGestureMinDist) {
							Vector2 direction = touch.position - fingerStartPos;
							Vector2 swypeType = Vector2.zero;

							if (Mathf.Abs (direction.x) > Mathf.Abs (direction.y)) {
								swypeType = Vector2.right * Mathf.Sign (direction.x);
							} else {
								swypeType = Vector2.up * Mathf.Sign (direction.y);
							}

							if (swypeType.x != 0f && !isHurt) {
								if (swypeType.x > 0f && checkFloor) {
									Swype ();
								}
							}else if (swypeType.y != 0f) {
								if (checkFloor && !isSwype && !isHurt && swypeType.y > 0f) {
									Jump ();
								}
							}
						
						}
						break;
					}
				}

			}

			if (isSwype) {
				counter += Time.deltaTime;
			}

			if (counter >= 0.7f) {
				isSwype = false;
				anim.SetBool ("IsSwype", isSwype);
				GetComponent<BoxCollider2D> ().enabled = true;
				GetComponent<CircleCollider2D> ().enabled = true;
				transform.GetChild (0).GetComponent<CircleCollider2D> ().enabled = false;
				transform.GetChild (0).GetComponent<BoxCollider2D> ().size = sizeNormal;
				transform.GetChild (0).GetComponent<BoxCollider2D> ().offset = offsetNormal;
				counter = 0f;
			}

			if (checkFloor && Input.GetKeyDown (KeyCode.Space) && !isSwype && !isHurt) {
				Jump ();
			}
//			for(int i = 0;i < Input.touchCount;i++){
//				if(Input.GetTouch(i).phase == TouchPhase.Began && checkFloor && !isSwype && !swypeGesture){
//					Jump ();
//				}
//			}
			Lating();
			anim.SetBool("IsHurt",isHurt);
			CheckFloor ();


		} else {
			Dead ();
		}
		Restart ();
	}
		

	void Movement(){
		rbFrank.velocity = new Vector2 (speed,rbFrank.velocity.y);
		anim.SetFloat ("Movement", rbFrank.velocity.x);
	}

	void CheckFloor(){
		checkFloor = Physics2D.OverlapCircle (fit.transform.position,radiousFit,1 << LayerMask.NameToLayer("Floor"));
		if (!checkFloor) {
			anim.SetFloat ("FallVelocity", rbFrank.velocity.y);
		} else {
			anim.SetFloat ("FallVelocity", 0f);
			forceJump = 150f;
		}
		anim.SetBool ("IsJumping", !checkFloor);
	}

	void Jump(){
		rbFrank.AddForce (new Vector2(0f,forceJump));
		aux.Play ();
	}

	void Swype(){
		GetComponent<BoxCollider2D> ().enabled = false;
		GetComponent<CircleCollider2D> ().enabled = false;
		isSwype = true;
		anim.SetBool ("IsSwype", isSwype);
		transform.GetChild (0).GetComponent<CircleCollider2D> ().enabled = true;
		transform.GetChild (0).GetComponent<BoxCollider2D> ().size = sizeSwype;
		transform.GetChild (0).GetComponent<BoxCollider2D> ().offset = offsetSwype;
	}

	void Dead(){
			anim.SetBool ("IsDead", isDead);
			speed = 0f;
			rbFrank.gravityScale = 1f;
			GetComponent<BoxCollider2D>().sharedMaterial = iceMaterial;
			triggerDead.enabled = false;
		startCount.SetActive (true);
	}

	public void Hurt(int damage){
		 if (UIManager.LifeCount >= 10) {
			UIManager.LifeCount -= damage;
			isHurt = true;
			speed = 0.5f;
			if(UIManager.LifeCount < 10){
				isDead = true;
			}
		}
	}

	void Lating(){
		if(isHurt){
			rbFrank.AddForce (-Vector2.right * 50);

			timeForHurt += Time.deltaTime;
			if(timeForHurt >= 0.5f){
				speed += 0.035f;
				if(speed >= 1f){
					isHurt = false;
					timeForHurt = 0f;
				}
			}
		}
	}

	void Restart(){
		if(isDead){
			if(CountDown.countDown <= 0f){
				SceneManager.LoadScene ("01");
				UIManager.LifeCount = 100;
				CountDown.countDown = 9f;
				startCount.SetActive (false);


			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Box" || other.tag == "Ceiling" || other.tag == "People" && !isSwype){	
			Hurt (10);
		}
	}
		

	void OnDrawGizmos(){
		//Gizmos.DrawWireSphere (fit.transform.position,radiousFit);
	}
}
