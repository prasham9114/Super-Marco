using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody2D myBody;
    private Animator anim;
    public Transform groundCheckPosition;
    public LayerMask groundLayer;

    // Jumping variables
    private bool isGrounded;
    private bool jumped;

    private float jumpPower = 12f;

    private void Awake() // get references and initialize here 
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfGrounded();
        PlayerJump();
    }

    // We do all the coding for physics here
    private void FixedUpdate()
    {
        PlayerWalk();
    }

    void PlayerWalk() {

        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal > 0)
        {
            changeDirection(1);
            myBody.velocity = new Vector2(speed, myBody.velocity.y);
        }
        else if(horizontal < 0)
        {
            changeDirection(-1);
            myBody.velocity = new Vector2(-speed , myBody.velocity.y); 
        }
        else
        {
            myBody.velocity = new Vector2 (0f, myBody.velocity.y);
        }

        // We set the players velocity based on the inputs and
        // then we selct which animation to paly based on the Speed 
        // parameter in the unity console

        anim.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x));


        // We are not directly reassigning variables because it is not allowed
        // So we therefore store them in a temporary variable and tehn 
        // reassign them

    }
    void changeDirection(int direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    void CheckIfGrounded()
    {

        // what raycast does is it draws a ray from the given position of the 
        // given length in the given direction and that ray checks for any collision

        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.05f, groundLayer);

        // when we are on thegroubnd and we jump we set the jump variable to the false
        // so that the player falls back towards the ground as he is no longer jumping 
        // and so that this process can be reapeated
        if (isGrounded)
        {
            if (jumped)
            {
                jumped = false;
                anim.SetBool("Jump", false); 
            }
        }
    }
    void PlayerJump()
    {
        if (isGrounded)
        {
            if(Input.GetKey(KeyCode.Space))
            {
                jumped = true;
                myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                anim.SetBool("Jump", true);
            }
        }
    }

    
} // Class
