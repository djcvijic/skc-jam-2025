using System;
using System.Collections;
using Audio;
using UnityEngine;

public class FoteljaAudioManager : AudioManager
{
    [Header("Music")]
    [SerializeField] private AudioClipSettings mainMenuMusic;
    [SerializeField] private AudioClipSettings musicStart;
    [SerializeField] private AudioClipSettings musicLoop;

    [Header("Sounds")]
    [SerializeField] private AudioClipSettings buttonClick;
    [SerializeField] private AudioClipSettings catFight;
    [SerializeField] private AudioClipSettings loseSound;
    [SerializeField] private AudioClipSettings meow;
    [SerializeField] private AudioClipSettings pee;
    [SerializeField] private AudioClipSettings scratch;
    [SerializeField] private AudioClipSettings shedding;
    [SerializeField] private AudioClipSettings vaseBreak;
    [SerializeField] private AudioClipSettings winSound;

    private bool mainMenuMusicPlaying;
    private bool levelMusicPlaying;

    public void StartMainMenuMusic()
    {
        StopAudio(musicStart);
        StopAudio(musicLoop);

        levelMusicPlaying = false;

        if (!mainMenuMusicPlaying)
        {
            mainMenuMusicPlaying = true;
            PlayAudio(mainMenuMusic);
        }
    }

    public void StartLevelMusic()
    {
        StopAudio(mainMenuMusic);
        StopAudio(musicLoop);

        mainMenuMusicPlaying = false;

        if (!levelMusicPlaying)
        {
            levelMusicPlaying = true;
            PlayAudio(musicStart);
            StartCoroutine(StartLevelMusicLoop());
        }
    }

    private IEnumerator StartLevelMusicLoop()
    {
        yield return new WaitForSeconds(musicStart.Variants[0].length);

        StopAudio(musicStart);
        PlayAudio(musicLoop);
    }

    public void ButtonClick() => PlayAudio(buttonClick);

    public void CatFight() => PlayAudio(catFight);

    public void LoseSound() => PlayAudio(loseSound);

    public void VaseBreak() => PlayAudio(vaseBreak);

    public void WinSound() => PlayAudio(winSound);

    public void StartInteraction(InteractionType type)
    {
        PlayAudio(meow);
        PlayAudio(type switch
        {
            InteractionType.Scratch => scratch,
            InteractionType.Piss => pee,
            InteractionType.Shed => shedding,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        });
    }

    public void FinishInteraction(InteractionType type)
    {
        StopAudio(type switch
        {
            InteractionType.Scratch => scratch,
            InteractionType.Piss => pee,
            InteractionType.Shed => shedding,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        });
    }
}