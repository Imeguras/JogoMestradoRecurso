using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Animations;

public class PlayerHandler : MonoBehaviour{
	[SerializeField] 
	private Rigidbody2D rb;
	[SerializeField]
	private float speed;
	[SerializeField]
	private float jumpForce;
	[SerializeField]
	private PlayerInput playerInput;
	[SerializeField]
	private Animator animator;
	void OnEnable(){
		
		playerInput.actions.Enable();	
	}
	void Awake(){
	}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	public void move(InputAction.CallbackContext context){
		//get the value of the input
		Vector2 input = context.ReadValue<Vector2>();
		
		//move player
		//animator
		if(input.y>0){
			animator.Play("Jump");
		
		}else if(input.x!=0){
			animator.Play("Walkin");
		}
		rb.velocity = new Vector2(input.x*speed, rb.velocity.y);
		

	}
	void OnDisable(){
		playerInput.actions.Disable();
	}

}
