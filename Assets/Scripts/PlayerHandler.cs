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
	private float maxSpeed;
	[SerializeField]
	private float jumpForce;
	[SerializeField]
	private PlayerInput playerInput;
	[SerializeField]
	private Animator animator;
	[SerializeField]
	private LayerMask groundCheckLayers;
	[SerializeField]
	private Transform groundDetectionPoint = null;
	private bool canMove = true;
	private int horizontalDirection=1; 
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
    void Update(){
        
    }
	void FixedUpdate(){
		//clamp horizontal to max speed
		//Vector2 velocity = rb.velocity;
		//velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
		//rb.velocity = velocity;
		if(this.transform.position.y < -20){
			SceneManager.LoadScene(0); 
		}
		
	}
	public void move(InputAction.CallbackContext context){
		//get the value of the input
		Vector2 input = context.ReadValue<Vector2>();
		//if its a leave event, set the input to 0 and animation should end
		if(context.phase == InputActionPhase.Canceled ){
			input.x = 0;
			input.y = 0;
			
		}

		
		
		//move player
		//animator
		if(input.y>0){
			if(!IsOnGround()){
				input.y=0;

			}else{
				//animator.Play("Jump");

			}

		}else if(input.x!=0){
			if(shouldFlip(input)){
				Flip();
			}
			animator.SetFloat("HorizontalSpeed", Mathf.Abs(input.x));
			
			
		}
		rb.AddForce(new Vector2(input.x * speed, input.y*jumpForce), ForceMode2D.Impulse);

		
		
		

	}
	bool shouldFlip(Vector2 target){

		return (target.x > 0 && horizontalDirection < 0) || (target.x < 0 && horizontalDirection > 0);
	}
	void OnDisable(){
		playerInput.actions.Disable();
	}
	private void Flip () {
		Vector2 targetRotation = transform.localEulerAngles;
		targetRotation.y += 180f;
		horizontalDirection *= -1;
		transform.localEulerAngles = targetRotation;
	}
	private bool IsOnGround () {
		Collider2D colliderFound = Physics2D.OverlapPoint(
		groundDetectionPoint.position, groundCheckLayers);
		
		
		return colliderFound != null;
	}

}
