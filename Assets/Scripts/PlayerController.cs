using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    
    public float moveSpeed;
    private float moveSpeedStore;
    public float jumpForce;
    public float jumpTime;
    public float speedMultiplier;
    public float speedIncreaseMilestone;
    private float speedIncreaseMilestoneStore;

    private float speedMilestoneCount;
    private float speedMilestoneCountStore;

    private bool stoppedJumping;
    private bool canDoubleJump;
     

    private float jumpTimeCounter;
    public bool grounded;
    public bool killBoxTouch;

    public LayerMask whatIsGround;
    public LayerMask whatIsKillBox;

    public Transform groundCheck;
    public float groundCheckRadius;

    private Collider2D myCollider;
    private Rigidbody2D myRigidbody;
    private Animator myAnimator;

    public GameManager theGameManager;

    public AudioSource jumpSound;
    public AudioSource deathSound;

    // Start is called before the first frame update
    void Start()
    {
       
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();
        jumpTimeCounter = jumpTime;
        speedMilestoneCountStore = speedMilestoneCount;
        moveSpeedStore = moveSpeed;
        speedIncreaseMilestoneStore = speedIncreaseMilestone;
        stoppedJumping = true;
    }

    // Update is called once per frame
    void Update()
    {

        // grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        killBoxTouch = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsKillBox);

     

        if(transform.position.x>speedMilestoneCount)
        {
            speedMilestoneCount += speedIncreaseMilestone;
            speedIncreaseMilestone = speedIncreaseMilestone * speedMultiplier;
            moveSpeed = moveSpeed * speedMultiplier;
        }


            myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);
       
        if (killBoxTouch)
        {
            myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);
        }


        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            foreach (Touch touch in Input.touches)
            {
                int id = touch.fingerId;
                if (EventSystem.current.IsPointerOverGameObject(id))
                {
                    return;
                }
            }

            if (grounded)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                stoppedJumping = false;
                jumpSound.Play();
            }
            if(!grounded && canDoubleJump)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                jumpTimeCounter = jumpTime;
                stoppedJumping = false;
                canDoubleJump = false;
                jumpSound.Play();
            }
        }
        if ((Input.GetKey(KeyCode.Space)|| Input.GetMouseButton(0)) && !stoppedJumping)
        {
            if(jumpTimeCounter >0)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
        }
        if(Input.GetKeyUp(KeyCode.Space)||Input.GetMouseButtonUp(0))
        {
            jumpTimeCounter = 0;
            stoppedJumping = true;
        }
        if(grounded)
        {
            jumpTimeCounter = jumpTime;
            canDoubleJump = true;
        }
            myAnimator.SetFloat("Speed", myRigidbody.velocity.x);
            myAnimator.SetBool("Grounded", grounded);
             myAnimator.SetBool("KillBoxTouch", killBoxTouch);
        

    }
    private void OnCollisionEnter2D (Collision2D other)
    {
        if(other.gameObject.tag =="killbox")
        {
            deathSound.Play();
            canDoubleJump = false;
            stoppedJumping = true;
            theGameManager.RestartGame();
            
            moveSpeed = moveSpeedStore;
            speedIncreaseMilestone = speedIncreaseMilestoneStore;
            speedMilestoneCount = speedMilestoneCountStore;
            
        }
        if (other.gameObject.tag == "spikebox")
        {
            deathSound.Play();
            canDoubleJump = false;
            stoppedJumping = true;
            theGameManager.RestartGame();

            moveSpeed = moveSpeedStore;
            speedIncreaseMilestone = speedIncreaseMilestoneStore;
            speedMilestoneCount = speedMilestoneCountStore;

        }
    }
}
