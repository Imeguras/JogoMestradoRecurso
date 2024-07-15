using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class porta : MonoBehaviour{
	private static bool interacted=false; 
	[SerializeField]
	private GameObject game_manager; 
	public int level{get; private set;}
	[SerializeField] private int __level; 
	[SerializeField]
	private Animator animator;
	private InputMap playerInput;
	[SerializeField]
	private Rigidbody2D r;
	void OnEnable(){
		playerInput.Enable();

	}
    // Start is called before the first frame update
	void Awake(){
		level = __level;
		playerInput = new InputMap();
	
		
	}
    void Start(){
		
		
		game_manager.GetComponent<GameManager>().addLevelAvailable(level);
		playerInput.Player.Interact.performed += ctx => StartCoroutine(Interact());
    }

    // Update is called once per frame
    void Update()
    {
        
    }	
	public IEnumerator Interact(){
		
		Debug.Log("Interacted");
		if(interacted){
			Debug.Log("Interacted is true, stop spamming");
			yield return null;
		}else{
			interacted = true;
			//check if anything with tag player is in range
			//if not return
			if(r.IsTouchingLayers(LayerMask.GetMask("Player")) == false){
				interacted = false;
				Debug.Log("Player is not in range");
				yield return null;
				
			}
			int available = game_manager.GetComponent<GameManager>().isLevelAvailable(level);
			if(available == 0){
				//TODO make a buble
				Debug.Log("Level is zero");
				yield return new WaitForSeconds(0.1f);
				interacted= false; 
			}else if (available >= 1){
				//load level async
				Debug.Log("Level is loading");
				var k = SceneManager.LoadSceneAsync(level);

				this.animator.SetBool("IsOpening", true);
				while(!k.isDone){
					//play animation
					yield return new WaitForSeconds(1f);
				}
			
				interacted=false ; 
				k.allowSceneActivation = true;
				
				
				

				//play animation



			
			}else{
				Debug.Log("Something went seriously wrong");
				interacted = false;
			}
		}
		
		yield return null;
	}
	void OnDisable(){
		playerInput.Disable();

	}
}