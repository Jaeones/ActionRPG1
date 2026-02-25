using System.Collections;
using UnityEngine;


namespace RPGGame
{
    public class PlayerAttackEffect : MonoBehaviour
    {
        private Animation refAnimation;

        private void Awake()
        {
            if (refAnimation == null)
            {
                refAnimation = GetComponent<Animation>();
            }

            gameObject.SetActive(false);

        }

        public void Activate()
        {
            gameObject.SetActive(true);
            refAnimation.Play();
            StartCoroutine(DisableAtEndOfAnimation());
        }

        IEnumerator DisableAtEndOfAnimation()
        {
            yield return new WaitForSeconds(refAnimation.clip.length);
            gameObject.SetActive(false);
        }
    }
}