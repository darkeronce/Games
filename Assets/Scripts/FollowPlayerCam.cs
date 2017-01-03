using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;
public class FollowPlayerCam : MonoBehaviour {

	[System.Serializable]
	public class ClampValues{
		public float xMin,xMax,yMin,yMax;
	}

	public GameObject target;
	public float lerpingFollow;
	public ClampValues clamp;

	void Start(){
		float xFactor = Screen.width / 800f;
		float yFactor = Screen.height  / 1280f;

		Camera.main.rect=new Rect(0,0,1,xFactor/yFactor);
	}
	
	void LateUpdate(){
		Vector2 newPos = Vector2.Lerp (new Vector2(Mathf.Clamp(target.transform.position.x,clamp.xMin,clamp.xMax),
											Mathf.Clamp(target.transform.position.y,clamp.yMin,clamp.yMax)),
											transform.position,
											lerpingFollow);
		
		transform.position = new Vector3(newPos.x,newPos.y,-10f);
	}

}
