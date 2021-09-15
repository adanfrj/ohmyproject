using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveController : MonoBehaviour
{
    [Header("Movement Setting")]
    public float moveAccel;
    public float maxSpeed;
    private Rigidbody2D rbchar;

    [Header("Jump")]
    public float jumpAccel;
    private bool isJumping;
    private bool isOnGround;

    [Header("Ground Raycast")]
    public float groundRaycastDistance;
    public LayerMask groundLayerMask;

    [Header("Scoring")]
    public ScoreController skor;
    public float rasioSkor;
    private float lastPositionX;

    [Header("GameOver")]
    public GameObject gameOverScreen;
    public float fallPositionY;

    [Header("Camera")]
    public CameraMoveController gameCamera;

    
    private Animator anim;
    private CharacterSoundController sound;

    // Start is called before the first frame update
    private void Start()
    {
        rbchar = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sound = GetComponent<CharacterSoundController>();
    }

    private void Update()
    {
        //read input
        if (Input.GetMouseButtonDown(0))
        {
            if (isOnGround)
            {
                isJumping = true;
                sound.PlayJump();
            }
        }

        // change animation
        anim.SetBool("isOnGround", isOnGround);

        //kalkulasi skor
        int distancePassed = Mathf.FloorToInt(transform.position.x - lastPositionX);
        int scoreIncrement = Mathf.FloorToInt(distancePassed / rasioSkor);

        if (scoreIncrement>0)
        {
            skor.TambahScore(scoreIncrement);
            lastPositionX += distancePassed;
        }

         // game over
        if (transform.position.y < fallPositionY)
        {
            GameOver();
        }

    }


    private void FixedUpdate()
    {
        //raycast ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundRaycastDistance, groundLayerMask);
        
        //jika menyentuh / berada di tanah
        if (hit)
        {
            if (!isOnGround && rbchar.velocity.y <= 0)
            {
                isOnGround = true;
            }
        }
        else 
        {
            isOnGround = false;
        }

        //kalkulasi velocity vector
        Vector2 velocityChar = rbchar.velocity;

        if (isJumping)
        {
            velocityChar.y += jumpAccel;
            isJumping = false;
        }
        velocityChar.x = Mathf.Clamp(velocityChar.x + moveAccel * Time.deltaTime, 0.0f, maxSpeed);
        rbchar.velocity = velocityChar;
    }

    private void GameOver()
    {
        // set high score
        skor.FinishScoring();

        // stop camera movement
        gameCamera.enabled = false;

        // show gameover
        gameOverScreen.SetActive(true);

        // disable this too
        this.enabled = false;
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy (other.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position + (Vector3.down * groundRaycastDistance), Color.white);
    }
    



}
