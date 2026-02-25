using UnityEngine;


namespace RPGGame
{
    public class MonsterDeathEffectPlayer : MonoBehaviour
    {
        private readonly string cutOffParameterName = "_Cutoff";

        [SerializeField] private float startTime = 1.2f;

        [SerializeField] private float playTime = 2f;

        private Renderer[] renderers;

        private float elapsedTime = 0f;

        private bool isPlaying = false;

        private MaterialPropertyBlock propertyBlock;

        private void OnEnable()
        {
            renderers = transform.root.GetComponentsInChildren<Renderer>();
            propertyBlock = new MaterialPropertyBlock();
        }

        private void Update()
        {
            if (!isPlaying) { return; }

            foreach (Renderer renderer in renderers)
            {
                renderer.GetPropertyBlock(propertyBlock);
                propertyBlock.SetFloat(cutOffParameterName, elapsedTime / playTime);
                renderer.SetPropertyBlock(propertyBlock);
            }

            elapsedTime += Time.deltaTime;

            if (elapsedTime > playTime)
            {
                isPlaying = false;

                Destroy(transform.root.gameObject);
            }
        }

        public void Play()
        {
            Invoke("PlayDeathEffect", startTime);
        }

        public void PlayDeathEffect()
        {
            isPlaying = true;
            elapsedTime = 0f;
        }

    }

}
