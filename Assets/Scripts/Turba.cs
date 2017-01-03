using UnityEngine;
using System.Collections;

public class Turba : EnemyManager {
	
	float timeToIncrement,timeToLeave;
	bool isCatchPlayer;

	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		timeToIncrement += Time.deltaTime;
		if(timeToIncrement >= 5f){
			speedFollow += 0.005f;
			timeToIncrement = 0f;
		}
		
		FollowPlayer ();
		Debug.Log (Vector2.Distance (target.position, transform.position));
	}

	public override void FollowPlayer(){
		if (Vector2.Distance (target.position, transform.position) <= 0.3f || isCatchPlayer) {
			timeToLeave += Time.deltaTime;
			if(timeToLeave >= 2f){
				transform.position += Vector3.right * 0.05f;
				if(timeToLeave > 5f){
					Destroy (gameObject);
				}
			}
			isCatchPlayer = true;
		} else if(!isCatchPlayer) {
			transform.position = Vector2.Lerp(transform.position, new Vector2(target.position.x,transform.position.y),speedFollow);
		}
	}

}
