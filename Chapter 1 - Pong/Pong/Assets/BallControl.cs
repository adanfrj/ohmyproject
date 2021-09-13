using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D rigidBody2D; //deklarasi variabel bola rigidbody2d
    // besar gaya awal dorongan bola
    public float xInitialForce;
    public float yInitialForce;
    public float speed;
    private Vector2 trajectoryOrigin; // Titik asal lintasan bola saat ini


    void ResetBall () 
    {
        transform.position = Vector2.zero; // Reset posisi menjadi (0,0)
        rigidBody2D.velocity = Vector2.zero; // Reset kecepatan menjadi (0,0)
    }

    void PushBall ()
    {
        // Menentukan nilai komponen y dari gaya dorong antara -yInitialForce dan yInitialForce
        rigidBody2D.velocity = rigidBody2D.velocity.normalized*speed;

        // Menentukan nilai acak antara 0(inklusif) dan 2 (eklusif)
        float randomDirection = Random.Range(0,2);

        // nilai < 1 = bola gerak ke kiri
        if (randomDirection < 1.0f ) 
        {
            //gunakan gaya untuk menggerakkan bola
            rigidBody2D.AddForce(new Vector2(-xInitialForce, yInitialForce));
        }
        
        else
        {
            rigidBody2D.AddForce(new Vector2(xInitialForce, yInitialForce));
        }
    }

    void RestartGame()
    {
        //Kembalikan bola ke posisi semula
        ResetBall();
        // Setelah 2s, berikan gaya pada bola
        Invoke("PushBall", 2);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D =GetComponent<Rigidbody2D>();
        RestartGame(); //Mulai game
        trajectoryOrigin = transform.position;
    }

     // Ketika bola beranjak dari sebuah tumbukan, rekam titik tumbukan tersebut
        void OnCollisionExit2D(Collision2D collision)
        {
        trajectoryOrigin = transform.position;
        }

        // Untuk mengakses informasi titik asal lintasan
        public Vector2 TrajectoryOrigin
        {
        get { return trajectoryOrigin;}
        }
        
    // Update is called once per frame
    void FixedUpdate()
    {
        rigidBody2D.velocity = Vector2.ClampMagnitude(rigidBody2D.velocity, speed);
    }
}
