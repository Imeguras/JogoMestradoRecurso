using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerHandler : MonoBehaviour{
	[SerializeField] 
	private Rigidbody2D rb;
	[SerializeField]
	private float speed;
	[SerializeField]
	private float jumpForce;
	[SerializeField]
	private PlayerInput playerInput;

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
	public void walkLeft(){
		//move player left
		rb.AddForce(new Vector2(-speed, 0), ForceMode2D.Impulse);
	}
	public void walkRight(){
		//move player right
		rb.AddForce(new Vector2(speed, 0), ForceMode2D.Impulse);

	}
	public void jump(){
		//make player jump
		rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
	}
	public void croutch(){
		//make player croutch
		rb.AddForce(new Vector2(0, -jumpForce), ForceMode2D.Impulse);
	}
	void OnDisable(){
		playerInput.actions.Disable();
	}

}
