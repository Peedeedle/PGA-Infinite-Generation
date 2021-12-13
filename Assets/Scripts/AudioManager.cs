////////////////////////////////////////////////////////////
// File: AudioManager.cs
// Author: Jack Peedle
// Date Created: 11/12/21
// Last Edited By: Jack Peedle
// Date Last Edited: 13/12/21
// Brief: Audio Manager
//////////////////////////////////////////////////////////// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    // reference to the audio source
    public AudioSource audioSource;

    // no audio clip
    public AudioClip noAudio;

    // normal biome audio clip
    public AudioClip normalAudio;

    // desert biome audio clip
    public AudioClip desertAudio;

    // ice biome audio clip
    public AudioClip iceAudio;

    // lava biome audio clip
    public AudioClip lavaAudio;

    // jungle biome audio clip
    public AudioClip jungleAudio;

    // cursed biome audio clip
    public AudioClip cursedAudio;

    // mushroom biome audio clip
    public AudioClip mushroomAudio;

    // farm biome audio clip
    public AudioClip farmAudio;

    // candy biome audio clip
    public AudioClip candyAudio;

    // Start is called before the first frame update
    void Start()
    {

        // set the audio source to the audio source
        audioSource = GetComponent<AudioSource>();

    }

    // set the audio source to none
    public void SetAudioToNone() {

        // set the audio source to none
        audioSource.clip = noAudio;


    }

    // set the audio source to normal
    public void PlayNormalSound() {

        // set the audio source to normal
        audioSource.clip = normalAudio;

        // Play the audio source
        audioSource.Play();

    }

    // set the audio source to desert
    public void PlayDesertSound() {

        // set the audio source to desert
        audioSource.clip = desertAudio;

        // Play the audio source
        audioSource.Play();

    }

    // set the audio source to ice
    public void PlayIceSound() {

        // set the audio source to ice
        audioSource.clip = iceAudio;

        // Play the audio source
        audioSource.Play();

    }

    // set the audio source to lava
    public void PlayLavaSound() {

        // set the audio source to lava
        audioSource.clip = lavaAudio;

        // Play the audio source
        audioSource.Play();

    }

    // set the audio source to jungle
    public void PlayJungleSound() {

        // set the audio source to jungle
        audioSource.clip = jungleAudio;

        // Play the audio source
        audioSource.Play();

    }

    // set the audio source to cursed
    public void PlayCursedSound() {

        // set the audio source to cursed
        audioSource.clip = cursedAudio;

        // Play the audio source
        audioSource.Play();

    }

    // set the audio source to mushroom
    public void PlayMushroomSound() {

        // set the audio source to mushroom
        audioSource.clip = mushroomAudio;

        // Play the audio source
        audioSource.Play();

    }

    // set the audio source to farm
    public void PlayFarmSound() {

        // set the audio source to farm
        audioSource.clip = farmAudio;

        // Play the audio source
        audioSource.Play();

    }

    // set the audio source to candy
    public void PlayCandySound() {

        // set the audio source to candy
        audioSource.clip = candyAudio;

        // Play the audio source
        audioSource.Play();

    }

}
