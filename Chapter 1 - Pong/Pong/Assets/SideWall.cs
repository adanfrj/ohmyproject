using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWall : MonoBehaviour
{
    public PlayerControl player; //pemain yang akan bertambah skornya jika bola menyentuh dinding
    // Akan dipanggil ketika objek lain ber-collider (bola) bersentuhan dengan dinding.

    //Inisialisasi game manager untuk mengakses max score
    [SerializeField]
    private GameManager gameManager;

    void OnTriggerEnter2D(Collider2D anotherCollider)
    {
        
        //Jika Objek tsb bernama Ball maka
        if (anotherCollider.name == "Ball")
        {
            //Menambah skor player
            player.IncrementScore();
        }

        //Jika skor pemain belum mencapai skor maksimal maka
        if (player.Score < gameManager.maxScore)
        {
            // restart game setelah bola kena dinding
            anotherCollider.gameObject.SendMessage("RestartGame", 2.0f, SendMessageOptions.RequireReceiver);
        }

    }
}
