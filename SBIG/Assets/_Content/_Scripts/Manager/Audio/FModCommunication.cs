using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Audio
{
    public class FModCommunication
    {
        private Bus _musicBus;
        private Bus _narratorBus;
        private Bus _sfxBus;
        
        public void PlayOneShot(EventReference eventReference)
        {
            RuntimeManager.PlayOneShot(eventReference);
        }
        public void SetInstance(ref EventInstance eventInstance, EventReference eventReference)
        {
            RelaeseInstance(ref eventInstance);
            eventInstance = RuntimeManager.CreateInstance(eventReference);
            
            // Fetch the Bus for overall volume control
            _musicBus = RuntimeManager.GetBus("bus:/Music");
            _narratorBus = RuntimeManager.GetBus("bus:/Narration");
            _sfxBus = RuntimeManager.GetBus("bus:/SFX");
        }
        
        public void SetInstanceAndPlay(ref EventInstance eventInstance, EventReference eventReference)
        {
            SetInstance(ref eventInstance, eventReference);
            PlayInstance(ref eventInstance);
        }

        public void PlayInstanceIfNotPlaying(ref EventInstance eventInstance, EventReference eventReference)
        {
            if (eventInstance.isValid())
            {
                if (!IsPlaying(ref eventInstance))
                {
                    SetInstanceAndPlay(ref eventInstance, eventReference);
                }
            }
            else
            {
                SetInstanceAndPlay(ref eventInstance, eventReference);
            }
        }
        public void PlayInstance(ref EventInstance eventInstance)
        {
            if (eventInstance.isValid())
                eventInstance.start();
        }

        public void ContinueInstance(ref EventInstance eventInstance)
        {
            if (!eventInstance.isValid())
            {
                Debug.Log("trying to play unvalid sound instance");
                return;
            }
            if (!IsPlaying(ref eventInstance))
            {
                eventInstance.start();
            }

        }

        public void StopInstance(ref EventInstance eventInstance)
        {
            if (eventInstance.isValid())
                eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
        public void RelaeseInstance(ref EventInstance eventInstance)
        {
            if (eventInstance.isValid())
            {
                eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                eventInstance.release();
            }
        }
        public void SetParameter(ref EventInstance eventInstance, string parameterName, float value)
        {
            if (eventInstance.isValid())
            {
                eventInstance.setParameterByName(parameterName, value);
            }
        }
        public void SetGlobalParameter(string parameterName, float value)
        {
            RuntimeManager.StudioSystem.setParameterByName(parameterName, value);
        }

        public bool IsPlaying(ref EventInstance eventInstance)
        {
            if (eventInstance.isValid())
            {
                PLAYBACK_STATE playbackState;
                eventInstance.getPlaybackState(out playbackState);
                return playbackState == PLAYBACK_STATE.PLAYING;
            }
            return false;
        }
        
        public void SetMusicVolume(float volume)
        {
            _musicBus.setVolume(volume);
        }
        
        public void SetNarratorVolume(float volume)
        {
            _narratorBus.setVolume(volume);
        }
        
        public void SetSfxVolume(float volume)
        {
            _sfxBus.setVolume(volume);
        }
        
        public float GetMusicVolume()
        {
            _musicBus.getVolume(out float volume);
            return volume;
        }
        
        public float GetNarratorVolume()
        {
            _narratorBus.getVolume(out float volume);
            return volume;
        }

        public float GetSfxVolume()
        {
            _sfxBus.getVolume(out float volume);
            return volume;
        }
    }
}