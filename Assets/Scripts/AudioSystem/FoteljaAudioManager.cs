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
    [SerializeField] private AudioClipSettings dash;

    private bool mainMenuMusicPlaying;
    private bool musicPlaying;

    public void StartMainMenuMusic()
    {
        StopAudio(musicStart);
        StopAudio(musicLoop);

        musicPlaying = false;

        if (!mainMenuMusicPlaying)
        {
            mainMenuMusicPlaying = true;
            PlayAudio(mainMenuMusic);
        }
    }

    public void StartMusic()
    {
        StopAudio(mainMenuMusic);
        StopAudio(musicLoop);

        mainMenuMusicPlaying = false;

        if (!musicPlaying)
        {
            musicPlaying = true;
            PlayAudio(musicStart);
            StartCoroutine(StartMusicLoop());
        }
    }

    private IEnumerator StartMusicLoop()
    {
        yield return new WaitForSeconds(musicStart.Variants[0].length);

        StopAudio(musicStart);
        PlayAudio(musicLoop);
    }

    public void Dash() => PlayAudio(dash);
}