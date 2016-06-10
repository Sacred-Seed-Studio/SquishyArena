using UnityEngine;
using System.Collections;

public enum SoundType
{
    Attack1,
    Attack2,
    Attack3,
    EnemyAttack,
    Death,
    Boost,
    Background,
    Title
}
public class AudioController : MonoBehaviour
{
    public static AudioController controller;

    void Awake()
    {
        if (controller == null)
        {
            DontDestroyOnLoad(gameObject);
            controller = this;
        }
        else if (controller != this)
        {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
        PlaySound(SoundType.Background);
    }
    AudioSource audioSource;

    public AudioClip attack1Clip, attack2Clip, attack3Clip, attackEnemyClip, boostClip, deathClip, titleSong, backgroundSong;
    public void PlaySound(SoundType sound)
    {
        switch (sound)
        {
            case SoundType.Attack1:
                audioSource.PlayOneShot(attack1Clip);
                break;
            case SoundType.Attack2:
                audioSource.PlayOneShot(attack2Clip);
                break;
            case SoundType.Attack3:
                audioSource.PlayOneShot(attack3Clip);
                break;
            case SoundType.EnemyAttack:
                audioSource.PlayOneShot(attackEnemyClip);
                break;
            case SoundType.Boost:
                audioSource.PlayOneShot(boostClip);
                break;
            case SoundType.Death:
                audioSource.PlayOneShot(deathClip);
                break;
            case SoundType.Background:
                audioSource.clip = backgroundSong;
                audioSource.Play();
                break;
            case SoundType.Title:
                audioSource.clip = titleSong;
                audioSource.Play();
                break;
            default:
                break;
        }
    }
}
