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
	
	private InputMap playerInput;
	[SerializeField]
	private Animator animator;
	[SerializeField]
	private LayerMask groundCheckLayers;
	[SerializeField]
	private Transform groundDetectionPoint = null;
	private int horizontalDirection=1; 
	private Vector2 input;
	

	
	
	void Awake(){
		playerInput = new InputMap();
	}
	void OnEnable(){
		playerInput.Enable();

	}
	
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }
	void FixedUpdate(){
		
		Movement(); 
		handleAnimations();
		OutOfBounds();
		
		
	}
	void handleAnimations(){
		animator.SetFloat("HorizontalSpeed", Mathf.Abs(input.x));
		animator.SetBool("isGround", IsOnGround());
		animator.SetBool("isFalling", rb.velocity.y < 0);
	}	
	bool shouldFlip(Vector2 target){

		return (target.x > 0 && horizontalDirection < 0) || (target.x < 0 && horizontalDirection > 0);
	}
	void OnDisable(){
		playerInput.Disable();
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
	void Movement(){
		
		var inputVector = playerInput.Player.Move.ReadValue<Vector2>();
		input=inputVector;
		//flip
		if(shouldFlip(inputVector)){
			Flip();
		}
		if(!IsOnGround()){
			inputVector.y = 0;
		}

		//Vector3 movement = new Vector3(inputVector.x* speed, 0, 0)  * Time.deltaTime;
		
        //transform.position += movement;
		rb.AddForce(new Vector2(inputVector.x* speed, jumpForce*inputVector.y), ForceMode2D.Impulse);
	

	}
	void OutOfBounds(){
		if(this.transform.position.y < -20){
			SceneManager.LoadScene(0); 
		}
	}

}
