using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public KeyCode upButton = KeyCode.W; // Tombol untuk menggerakkan raket ke atas
    public KeyCode downButton = KeyCode.S; // Tombol untuk menggerakkan raket ke bawah
    public float speed = 10.0f; // Tombol untuk menggerakkan ke bawah
    public float yBoundary = 9.0f; // Setting batas atas dan bawah game scene
    private Rigidbody2D rigidBody2D; // Deklarasi variabel raket Rigidbody 2D
    private int score; // Skor pemain
    private ContactPoint2D lastContactPoint; //menampilkan variabel fisika terkait titik tumbukan terakhir dengan bola
    
    public ContactPoint2D LastContactPoint //Untuk mengakses informasi titik kontak dari kelas lain
    {
        get {return lastContactPoint; }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Ball"))
        {
            lastContactPoint = collision.GetContact(0);
        }
    }
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Menentukan kecepatan raket sekarang.
        Vector2 velocity = rigidBody2D.velocity;
 
        // Jika player menekan tombol ke atas, beri kecepatan positif(atas) komponen y
        if (Input.GetKey(upButton))
        {
            velocity.y = speed;
        }
 
         // Jika player menekan tombol ke bawah, beri kecepatan negatif(bawah) komponen y
        else if (Input.GetKey(downButton))
        {
            velocity.y = -speed;
        }
 
        // Jika player tidak menekan tombol, raket diam (kecepatannya nol)
        else
        {
            velocity.y = 0.0f;
        }
 
        // Input kecepatannya ke rigidBody2D.
        rigidBody2D.velocity = velocity;

        // Menentukan posisi raket sekarang.
        Vector3 position = transform.position;
 
        // Jika posisi raket melewati batas atas (yBoundary), kembalikan ke batas atas tersebut.
        if (position.y > yBoundary)
        {
            position.y = yBoundary;
        }
 
        // Jika posisi raket melewati batas bawah (-yBoundary), kembalikan ke batas atas tersebut.
        else if (position.y < -yBoundary)
        {
            position.y = -yBoundary;
        }
 
        // Input posisinya ke position.
        transform.position = position;
    }

    // menambah skor sebanyak 1 poin
    public void IncrementScore()
    {
        score++;
    }
    
    // reset skor menjadi 0
    public void ResetScore()
    {
        score = 0;
    }
 
    // Mendapatkan nilai skor
    public int Score
    {
        get { return score; }
    }
}
