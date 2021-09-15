using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacksoundControl : MonoBehaviour
{
    private AudioSource audioPlayer;
    void Awake()
    {
         Destroy(audioPlayer);
    }
    
}
