////////////////////////////////////////////////////////////
// File: AudioManager.cs
// Author: Jack Peedle
// Date Created: 11/12/21
// Last Edited By: Jack Peedle
// Date Last Edited: 11/12/21
// Brief: 
//////////////////////////////////////////////////////////// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    //
    public AudioSource audioSource;

    //
    public AudioClip noAudio;

    //
    public AudioClip normalAudio;

    //
    public AudioClip desertAudio;

    //
    public AudioClip iceAudio;

    //
    public AudioClip lavaAudio;

    //
    public AudioClip jungleAudio;

    //
    public AudioClip cursedAudio;

    //
    public AudioClip mushroomAudio;

    //
    public AudioClip farmAudio;

    //
    public AudioClip candyAudio;

    // Start is called before the first frame update
    void Start()
    {

        //
        audioSource = GetComponent<AudioSource>();

    }

    //
    public void SetAudioToNone() {

        //
        audioSource.clip = noAudio;


    }

    //
    public void PlayNormalSound() {

        //
        audioSource.clip = normalAudio;

        //
        audioSource.Play();

    }

    //
    public void PlayDesertSound() {

        //
        audioSource.clip = desertAudio;

        //
        audioSource.Play();

    }

    //
    public void PlayIceSound() {

        //
        audioSource.clip = iceAudio;

        //
        audioSource.Play();

    }

    //
    public void PlayLavaSound() {

        //
        audioSource.clip = lavaAudio;

        //
        audioSource.Play();

    }

    //
    public void PlayJungleSound() {

        //
        audioSource.clip = jungleAudio;

        //
        audioSource.Play();

    }

    //
    public void PlayCursedSound() {

        //
        audioSource.clip = cursedAudio;

        //
        audioSource.Play();

    }

    //
    public void PlayMushroomSound() {

        //
        audioSource.clip = mushroomAudio;

        //
        audioSource.Play();

    }

    //
    public void PlayFarmSound() {

        //
        audioSource.clip = farmAudio;

        //
        audioSource.Play();

    }

    //
    public void PlayCandySound() {

        //
        audioSource.clip = candyAudio;

        //
        audioSource.Play();

    }

}
