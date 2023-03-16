using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlaySound : MonoBehaviour
{
    public AudioSource Audio;

    private void Awake() 
    {
        Audio = GetComponent<AudioSource>();
        Audio.Play();
        Destroy(this, Audio.clip.length);
    } 
}
