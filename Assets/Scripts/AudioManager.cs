﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    static AudioManager current;
    // Start is called before the first frame update

    [Header("Audio CLips ambiente")]
    public AudioClip ambientClip;
    public AudioClip musicClip;
    public AudioClip musicDeathClip;
    public AudioClip deadShockClip;

    [Header("Audio Clips Personaje")]
    public AudioClip playerJumpClip;
    public AudioClip[] playerRunClips;
    public AudioClip playerRespawnClip;
    public AudioClip playerHitClip;
    public AudioClip playerDeathClip;

    [Header("Audio Clips siting")]
    public AudioClip groundDestroyClip;
    public AudioClip groundKickClip;
    public AudioClip enemyKickClip;
    public AudioClip groundMoveClip;
    public AudioClip finalEnemyDead;
    public AudioClip finalEnemyAttack;
    public AudioClip finalEnemyFly;

    [Header("Audio Clips items")]
    public AudioClip potionitemClip;
    public AudioClip keyitemClip;

    [Header("Mixer groups")]
    public AudioMixerGroup ambientGroup;
    public AudioMixerGroup musicGroup;
    public AudioMixerGroup playerGroup;
    public AudioMixerGroup itemGroup;
    public AudioMixerGroup sitingGroup;


    AudioSource ambientSource, musicSource, playerSource, sitingSource,itemsSource;

    private void Awake()
    {
        //Si el audioManager existe y no es este
        if (current != null && current != this)
        {
            //Destroy(gameObject);
            return;
        }
        //este es el audioManager y deberia persistir entre escenas
        current = this;
        //DontDestroyOnLoad(gameObject);

        //Inciar canales que se ejecutan simultaneamente cuando se conecta el juego
        ambientSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        musicSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        playerSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        sitingSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        itemsSource = gameObject.AddComponent<AudioSource>() as AudioSource;

        //se asigna el respectivo audiosource a su mixer group
        ambientSource.outputAudioMixerGroup = ambientGroup;
        musicSource.outputAudioMixerGroup = musicGroup;
        sitingSource.outputAudioMixerGroup = sitingGroup;
        playerSource.outputAudioMixerGroup = playerGroup;
        itemsSource.outputAudioMixerGroup = itemGroup;


        //llama al start
        StartLevelAudio();
    }
    void StartLevelAudio()
    {
        //parametros sonidos ambiente
        ambientSource.clip = current.ambientClip;
        ambientSource.loop = true;
        ambientSource.Play();
        //parametros sonido musica
        musicSource.clip = current.musicClip;
        musicSource.loop = true;
        musicSource.Play();

    }

    public static void RestartAudio()
    {
        current.StartLevelAudio();
    }

    public static void PlayShockAudio()
    {
        if (current == null) return;
        current.sitingSource.clip = current.deadShockClip;
        current.sitingSource.Play();
    }
    public static void PlaySceneRestartAudio()
    {
        if (current == null)return;
        current.sitingSource.clip = current.ambientClip;

        
    }
    //metodos Player
    public static void PlayRunAudio()
    {
        //si se está reproduciendo un sonido en el jugador sale
        if (current != null && current.playerSource.isPlaying) return;
        var step = Random.Range(0, current.playerRunClips.Length);
        current.playerSource.clip = current.playerRunClips[step];
        current.playerSource.Play();
    }
    public static void PlayJumpAudio()
    {
        if (current == null) return;
        current.playerSource.clip = current.playerJumpClip;
        current.playerSource.Play();
    }
    public static void PlayRespawnAudio()
    {
        if (current == null) return;
        current.sitingSource.clip = current.playerRespawnClip;
        current.sitingSource.Play();
    }
    public static void PlayHitAudio()
    {
        if (current == null) return;
        current.playerSource.clip = current.playerHitClip;
        current.playerSource.Play();
    }
    public static void PlayDeadAudio()
    {
        if (current == null) return;
        current.sitingSource.clip = current.playerDeathClip;
        current.sitingSource.PlayDelayed(.3f);
        current.musicSource.clip = current.musicDeathClip;
        current.musicSource.Play();
    }


    //metodos siting
    public static void PlayGroundKickAudio()
    {
        if (current == null) return;
        current.sitingSource.clip = current.groundKickClip;
        current.sitingSource.Play();
    }
    public static void PlayGroundDestroyAudio()
    {
        if (current == null) return;
        current.sitingSource.clip = current.groundDestroyClip;
        current.sitingSource.Play();
    }
    public static void PlayEnemyKick()
    {
        if (current == null) return;
        current.sitingSource.clip = current.enemyKickClip;
        current.sitingSource.Play();
    }
    public static void PlayGroundMove()
    {
        if (current == null) return;
        current.sitingSource.clip = current.groundMoveClip;
        current.sitingSource.Play();
    }
    public static void PlayFinalEnemyDead()
    {
        if (current == null) return;
        current.sitingSource.clip = current.finalEnemyDead;
        current.sitingSource.Play();
    }
    public static void PlayFinalEnemyAttack()
    {
        if (current == null) return;
        current.sitingSource.clip = current.finalEnemyAttack;
        current.sitingSource.Play();
    }
    public static void PlayFinalEnemyFly()
    {
        if (current == null) return;
        current.sitingSource.clip = current.finalEnemyFly;
        current.sitingSource.Play();
    }

    //metodos condicion
    public static void PlayWonAudio()
    {
        if (current == null) return;
        current.ambientSource.Stop();
    }

    //metodos items
    public static void PlayKeyAudio()
    {
        if (current == null) return;
        current.itemsSource.clip = current.keyitemClip;
        current.itemsSource.Play();
    }
    public static void PlayPotionCollectedAudio()
    {
        if (current == null) return;
        current.itemsSource.clip = current.potionitemClip;
        current.itemsSource.Play();
    }

}
