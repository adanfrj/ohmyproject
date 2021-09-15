using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinValue : MonoBehaviour
{
    private AudioSource audioPlayer;
    public AudioClip coinTouch;
    public int nilaiCoin = 1;
    
    
    private void Start()    
    {
        audioPlayer = GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CoinManager.instance.TambahSkorKoin(nilaiCoin);
        }
    }


}
