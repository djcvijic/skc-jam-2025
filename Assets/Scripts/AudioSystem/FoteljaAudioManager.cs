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

    public void Dash() => PlayAudio(dash);
}