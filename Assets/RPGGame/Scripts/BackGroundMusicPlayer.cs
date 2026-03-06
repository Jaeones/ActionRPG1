using System;
using System.Collections;
using UnityEngine;


namespace RPGGame
{
    public class BackGroundMusicPlayer : MonoBehaviour
    {
        [Serializable]
        public class AudioInfo
        {
            public AudioClip clip;

            public float defaultVolume = 1.0f;
        }

        [SerializeField] private AudioSource audioPlayer;
        [SerializeField] private AudioInfo[] musics;
        [SerializeField] private float transitionTime = 1.0f;

        private void OnEnable()
        {
            if (audioPlayer == null)
            {
                audioPlayer = GetComponent<AudioSource>();

                PlayNormalMusic();
            }
        }

        public void PlayNormalMusic()
        {
            StartCoroutine(TransitionMusic(musics[0]));
        }

        public void PlayBattleMusic()
        {
            StartCoroutine(TransitionMusic(musics[1]));
        }

        IEnumerator TransitionMusic(AudioInfo targetAudio)
        {
            float time = 0;
            float volume = audioPlayer.volume;

            while (time < transitionTime)
            {
                audioPlayer.volume = Mathf.Lerp(volume, 0, time / transitionTime);
                time += Time.deltaTime;
            }
            audioPlayer.clip = targetAudio.clip;
            audioPlayer.pitch = UnityEngine.Random.Range(0.98f, 1.02f);
            audioPlayer.Play();

            time = 0;

            while (time < transitionTime)
            {
                audioPlayer.volume = Mathf.Lerp(0f, targetAudio.defaultVolume, time / transitionTime);
                time += Time.deltaTime;
                yield return null;
            }

            audioPlayer.volume = targetAudio.defaultVolume;
        }
    }

}
