using System.Collections;
using UnityEngine;


namespace RPGGame
{
    public class PlayerShieldEffectPlayer : MonoBehaviour
    {
        [SerializeField] private float playTime = 0.5f;

        public void Play()
        {
            gameObject.SetActive(true);

            StartCoroutine(WaitAndTurnOff(playTime));
        }

        IEnumerator WaitAndTurnOff(float delay)
        {
            yield return new WaitForSeconds(delay);
            gameObject.SetActive(false);
        }
    }
}
