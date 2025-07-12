using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource sfxAudioSource;
    [Header("Audio Clips")]
    [SerializeField] private AudioClip cardFlip;
    [SerializeField] private AudioClip cardMatch;
    [SerializeField] private AudioClip cardMismatch;
    [SerializeField] private AudioClip gameEnd;
    public static AudioManager Instance{ get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlayCardFlip()
    {
        sfxAudioSource.PlayOneShot(cardFlip);
    }
    public void PlayCardMatch()
    {
        sfxAudioSource.PlayOneShot(cardMatch);
    }
    public void PlayCardMismatch()
    {
        sfxAudioSource.PlayOneShot(cardMismatch);
    }
    public void PlayGameEnd()
    {
        sfxAudioSource.PlayOneShot(gameEnd);
    }
}
