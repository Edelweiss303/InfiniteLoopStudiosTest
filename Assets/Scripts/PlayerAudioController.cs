///-------------------------------------------------------------------------------------------------
// file: JimAudioController.cs
//
// author: Rishi Barnwal
// date: 24/06/2020
//
// summary: Implements the animation events for the player character
///-------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private AudioClip[] dirtStepSounds;

    private AudioSource audioSource;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void StepEvent()
    {
        if (!playerController.IsGrounded)
        {
            return;
        }

        // Choose a random step sound and play it
        int index = UnityEngine.Random.Range(0, dirtStepSounds.Length);
        audioSource.clip = dirtStepSounds[index];

        audioSource.Play();
    }
}
