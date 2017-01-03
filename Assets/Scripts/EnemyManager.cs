using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	public Rigidbody2D target;
	public float speedFollow;


	void  Update(){
		FollowPlayer ();
	}
		
	public virtual void FollowPlayer(){
		transform.position = Vector2.Lerp(transform.position, new Vector2(target.position.x,transform.position.y),speedFollow);
	}
}
