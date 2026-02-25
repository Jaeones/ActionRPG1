using UnityEngine;


namespace RPGGame
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] private bool isRandomPitch = false;
        [SerializeField] private float pitchRandomRange = 0.2f;
        [SerializeField] private float playDelay = 0f;

        [SerializeField] private AudioClip[] audioClips;
        private AudioSource audioPlayer;

        private void OnEnable()
        {
            if (audioPlayer == null)
            {
                audioPlayer = GetComponent<AudioSource>();
            }
        }

        public void Play()
        {
            AudioClip clip = audioClips[Random.Range(0, audioClips.Length)];
            audioPlayer.pitch = isRandomPitch ? Random.Range(1.0f - pitchRandomRange, 1.0f + pitchRandomRange) : 1f;

            audioPlayer.clip = clip;

            audioPlayer.PlayDelayed(playDelay);
        }
    }
}