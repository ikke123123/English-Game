using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundcardPlayer : MonoBehaviour
{
    [HideInInspector] private List<Soundcard> playingAudioSources = new List<Soundcard>();
    [HideInInspector] private List<Soundcard> rampOnAudioSources = new List<Soundcard>();
    [HideInInspector] private List<Soundcard> rampOffAudioSources = new List<Soundcard>();
    [HideInInspector] private List<Soundcard> pausedAudioSources = new List<Soundcard>();

    private void FixedUpdate()
    {
        RampOnFixedUpdate();
        RampOffFixedUpdate();
        PlayingFixedUpdate();
    }


    //Public
    public void StopPlaying(Soundcard soundcard, bool skipRampOff = false)
    {
        playingAudioSources.Remove(soundcard);
        if (skipRampOff == false)
        {
            soundcard.state = Soundcard.State.rampOff;
            rampOffAudioSources.Add(soundcard);
            return;
        }
        DestroyAudioSource(soundcard);
    }

    public void StartPlaying(Soundcard soundcard)
    {
        CreateAudioSource(soundcard);
    }

    public void StopAllSound(bool skipRampOff)
    {
        foreach (Soundcard soundcard in CombineSoundcardLists(playingAudioSources, rampOffAudioSources, rampOnAudioSources))
        {
            StopPlaying(soundcard, skipRampOff);
        }
    }

    public void PauseAllSound()
    {
        foreach (Soundcard soundcard in CombineSoundcardLists(playingAudioSources, rampOffAudioSources, rampOnAudioSources))
        {
            Pause(soundcard);
        }
    }

    public void ResumeAllSound()
    {
        foreach (Soundcard soundcard in pausedAudioSources)
        {
            Unpause(soundcard);
        }
    }

    public void Pause(Soundcard soundcard)
    {
        RemoveFromList(soundcard);
        soundcard.paused = true;
        pausedAudioSources.Add(soundcard);
        soundcard.audioSource.Stop();
    }

    public void Unpause(Soundcard soundcard)
    {
        AddToList(soundcard);
        soundcard.paused = false;
        pausedAudioSources.Add(soundcard);
        soundcard.audioSource.Play();
    }


    //Private
    private void CreateAudioSource(Soundcard soundcard)
    {
        if (soundcard.state == Soundcard.State.off)
        {
            soundcard.audioSource = gameObject.AddComponent<AudioSource>();
            soundcard.audioSource.clip = soundcard.clip;
            soundcard.audioSource.volume = 0;
            soundcard.audioSource.spatialBlend = 1;
            soundcard.audioSource.Play();
            soundcard.state = Soundcard.State.rampOn;
            rampOnAudioSources.Add(soundcard);
        }
    }

    private bool DestroyAudioSource(Soundcard soundcard)
    {
        if (soundcard.audioSource != null)
        {
            soundcard.state = Soundcard.State.off;
            Destroy(soundcard.audioSource);
            return true;
        }
        return false;
    }

    private void RemoveFromList(Soundcard soundcard)
    {
        switch (soundcard.state)
        {
            case Soundcard.State.playing:
                playingAudioSources.Remove(soundcard);
                break;
            case Soundcard.State.rampOn:
                rampOnAudioSources.Remove(soundcard);
                break;
            case Soundcard.State.rampOff:
                rampOffAudioSources.Remove(soundcard);
                break;
        }
    }

    private void AddToList(Soundcard soundcard)
    {
        switch (soundcard.state)
        {
            case Soundcard.State.playing:
                playingAudioSources.Add(soundcard);
                break;
            case Soundcard.State.rampOn:
                rampOnAudioSources.Add(soundcard);
                break;
            case Soundcard.State.rampOff:
                rampOffAudioSources.Add(soundcard);
                break;
        }
    }

    private void RampOnFixedUpdate()
    {
        if (rampOnAudioSources.Count == 0) return;
        List<Soundcard> toRemove = new List<Soundcard>();
        foreach (Soundcard soundcard in rampOnAudioSources)
        {
            if (soundcard.rampOnStep > soundcard.volume || soundcard.audioSource.volume + soundcard.rampOnStep > soundcard.volume)
            {
                soundcard.audioSource.volume = soundcard.volume;
                soundcard.state = Soundcard.State.playing;
                toRemove.Add(soundcard);
                playingAudioSources.Add(soundcard);
            } else soundcard.audioSource.volume += soundcard.rampOnStep;
        }
        if (toRemove.Count == 0) return;
        foreach (Soundcard soundcard in toRemove) rampOnAudioSources.Remove(soundcard);
    }

    private void RampOffFixedUpdate()
    {
        if (rampOffAudioSources.Count == 0) return;
        foreach (Soundcard soundcard in rampOffAudioSources)
        {
            if (soundcard.audioSource.volume - soundcard.rampOffStep < 0) DestroyAudioSource(soundcard);
            else soundcard.audioSource.volume += soundcard.rampOffStep;
        }
    }

    private void PlayingFixedUpdate()
    {
        if (playingAudioSources.Count == 0) return;
        foreach (Soundcard soundcard in playingAudioSources)
        {
            if (soundcard.audioSource.isPlaying == false)
            {
                DestroyAudioSource(soundcard);
            }
        }
    }

    private List<Soundcard> CombineSoundcardLists(List<Soundcard> list1, List<Soundcard> list2, List<Soundcard> list3)
    {
        List<Soundcard> output = new List<Soundcard>();
        output.AddRange(list1);
        output.AddRange(list2);
        output.AddRange(list3);
        return output;
    }
}
