using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundcardPlayer : MonoBehaviour
{
    //------------------------------------------
    //             Made By Thomas
    //------------------------------------------
    //This object maintains an audio object, and
    //let's it play audiocards, much like a juk-
    //ebox. 
    //Use this only for bigger objects that need
    //to play multiple sounds.

    [Header("Debug")]
    [SerializeField] private List<Soundcard> playingAudioSources = new List<Soundcard>();
    [SerializeField] private List<Soundcard> rampOnAudioSources = new List<Soundcard>();
    [SerializeField] private List<Soundcard> rampOffAudioSources = new List<Soundcard>();
    [SerializeField] private List<Soundcard> pausedAudioSources = new List<Soundcard>();

    private void FixedUpdate()
    {
        RampOnFixedUpdate();
        RampOffFixedUpdate();
        PlayingFixedUpdate();
    }

    private void OnDestroy()
    {
        foreach (Soundcard soundcard in CombineSoundcardLists(playingAudioSources, rampOffAudioSources, rampOnAudioSources))
        {
            DestroyAudioSource(soundcard);
        }
    }

    //Public
    public void StopPlaying(Soundcard soundcard)
    {
        playingAudioSources.Remove(soundcard);
        soundcard.state = State.rampOff;
        rampOffAudioSources.Add(soundcard);
    }

    public void StartPlaying1(Soundcard soundcard)
    {
        if (soundcard.state == State.off && soundcard.volume != 0)
        {
            if (SoundCategory.CategoryStateOff(soundcard))
            {
                AudioSource audio = gameObject.AddComponent<AudioSource>();
                audio.clip = soundcard.clip;
                audio.loop = soundcard.loop;
                audio.volume = 0;
                audio.spatialBlend = 1;
                audio.Play();
                soundcard.audioSource = audio;
                soundcard.state = State.rampOn;
                rampOnAudioSources.Add(soundcard);
                SoundCategory.SetCategoryStateOn(soundcard);
                return;
            }
            foreach (Soundcard soundcard1 in playingAudioSources)
            {
                if (soundcard.category == soundcard1.category)
                {
                    soundcard1.playAfterThis = soundcard;
                    soundcard1.timePlayAfterThis = -1;
                }
            }
            Debug.Log("Couldn't start playing sound because of Catagory State being turned On.");
        }
    }

    public void StartPlaying(Soundcard soundcard, bool overrideCategory = false)
    {
        if (soundcard.state == State.off && soundcard.volume != 0)
        {
            if (SoundCategory.CategoryStateOff(soundcard) || overrideCategory)
            {
                AudioSource audio = gameObject.AddComponent<AudioSource>();
                audio.clip = soundcard.clip;
                audio.loop = soundcard.loop;
                audio.volume = 0;
                audio.spatialBlend = 1;
                audio.Play();
                soundcard.audioSource = audio;
                soundcard.state = State.rampOn;
                rampOnAudioSources.Add(soundcard);
                SoundCategory.SetCategoryStateOn(soundcard);
                return;
            }
            foreach (Soundcard soundcard1 in playingAudioSources)
            {
                if (soundcard.category == soundcard1.category)
                {
                    soundcard1.playAfterThis = soundcard;
                    soundcard1.timePlayAfterThis = -1;
                }
            }
            Debug.Log("Couldn't start playing sound because of Catagory State being turned On.");
        }
    }

    public void StopAllSound(bool skipRampOff)
    {
        if (skipRampOff)
        {
            foreach (Soundcard soundcard in CombineSoundcardLists(playingAudioSources, rampOffAudioSources, rampOnAudioSources))
            {
                DestroyAudioSource(soundcard);
            }
            return;
        }
        foreach (Soundcard soundcard in CombineSoundcardLists(playingAudioSources, rampOffAudioSources, rampOnAudioSources))
        {
            StopPlaying(soundcard);
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
        soundcard.audioSource.Pause();
    }

    public void Unpause(Soundcard soundcard)
    {
        AddToList(soundcard);
        soundcard.paused = false;
        pausedAudioSources.Add(soundcard);
        soundcard.audioSource.UnPause();
    }


    //Private
    private bool DestroyAudioSource(Soundcard soundcard)
    {
        soundcard.state = State.off;
        SoundCategory.SetCategoryStateOff(soundcard);
        if (soundcard.audioSource != null)
        {
            Destroy(soundcard.audioSource);
            return true;
        }
        return false;
    }

    private void RemoveFromList(Soundcard soundcard)
    {
        switch (soundcard.state)
        {
            case State.playing:
                playingAudioSources.Remove(soundcard);
                break;
            case State.rampOn:
                rampOnAudioSources.Remove(soundcard);
                break;
            case State.rampOff:
                rampOffAudioSources.Remove(soundcard);
                break;
        }
    }

    private void AddToList(Soundcard soundcard)
    {
        switch (soundcard.state)
        {
            case State.playing:
                playingAudioSources.Add(soundcard);
                break;
            case State.rampOn:
                rampOnAudioSources.Add(soundcard);
                break;
            case State.rampOff:
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
                soundcard.state = State.playing;
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
        List<Soundcard> destroySoundcards = new List<Soundcard>();
        foreach (Soundcard soundcard in rampOffAudioSources)
        {
            if (soundcard.audioSource.volume + soundcard.rampOffStep <= 0) destroySoundcards.Add(soundcard);
            else soundcard.audioSource.volume += soundcard.rampOffStep;
        }
        foreach (Soundcard soundcard in destroySoundcards)
        {
            rampOffAudioSources.Remove(soundcard);
            DestroyAudioSource(soundcard);
        }
    }

    private void PlayingFixedUpdate()
    {
        if (playingAudioSources.Count == 0) return;
        List<Soundcard> rampOffSoundcards = new List<Soundcard>();
        foreach (Soundcard soundcard in playingAudioSources)
        {
            if (soundcard.audioSource == null || soundcard.audioSource.isPlaying == false)
            {
                if (soundcard.playAfterThis != null)
                {
                    StartPlaying(soundcard.playAfterThis);
                    soundcard.playAfterThis = null;
                }
                rampOffSoundcards.Add(soundcard);
            } else if (soundcard.audioSource.time >= soundcard.timePlayAfterThis && soundcard.timePlayAfterThis != -1)
            {
                if (soundcard.playAfterThis != null)
                {
                    StartPlaying(soundcard.playAfterThis, true);
                    soundcard.playAfterThis = null;
                }
            }
        }
        foreach (Soundcard soundcard in rampOffSoundcards) StopPlaying(soundcard);
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
