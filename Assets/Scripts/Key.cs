using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Key : MonoBehaviour
{
	[SerializeField]
    private GameObject door;
	int level =0;
	[SerializeField]
	private Animator keyAnimator;
	[SerializeField]
	private GameObject gameManager;
	
	
	//public  UnityEvent keyCollected; Apparently animator already does this for us
	void Awake()
    {
        level = door.GetComponent<porta>().level;
		/*if(keyCollected == null){
			keyCollected = new UnityEvent();
		}
		keyCollected.AddListener(CollectKey);*/


    }
	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player")){
			Debug.Log("Player has collected the key");
			keyAnimator.SetBool("isCollected", true);
		}
	}
	public IEnumerator CollectKey(){
		Debug.Log("Key has been collected");
		gameManager.GetComponent<GameManager>().setLevel(level, 1);
		//get ready to play
		this.GetComponent<AudioSource>().PlayOneShot(this.GetComponent<AudioSource>().clip);

		//wait for the sound to finish
		while(this.GetComponent<AudioSource>().isPlaying){
			yield return new WaitForSeconds(0.1f);
		}
		this.gameObject.SetActive(false);
		
		
	}
}
