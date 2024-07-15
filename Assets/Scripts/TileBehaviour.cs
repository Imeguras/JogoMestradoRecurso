using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TileBehaviour : MonoBehaviour{
    public float fallTime;
	public int lane;
	SpriteRenderer sr;
	
	private Rigidbody2D rb; 
	Vector3 moveState = Vector3.down;

	void Start(){
		rb = GetComponent<Rigidbody2D>();
		sr =gameObject.GetComponent<SpriteRenderer>();
	}
	void FixedUpdate(){
		//has to fall in the fallTime
	
		transform.position += (moveState * Time.deltaTime) / fallTime;
		//check if rb is colliding
		if(transform.position.y < -5){
			//TODO: add a miss
			StartCoroutine(Fail());
			
		}
	}
	IEnumerator Fail(){
		moveState = Vector3.zero;
		
		sr.color = Color.red;
		
		yield return new WaitForSeconds(1);
		

		
		SceneManager.LoadScene(0);
	}
	

}
