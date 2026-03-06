using UnityEngine;


namespace RPGGame
{
    public class GrenadierHitEffectPlayer : MonoBehaviour
    {
        [SerializeField] private ParticleSystem hitParticle;
        [SerializeField] private SkinnedMeshRenderer mainRender;
        [SerializeField] private float hitEffectTime = 1f;
        [SerializeField] private Color hitEffectColor = Color.red;

        private Material colorMaterial;
        private Color originalColor;

        private void Awake()
        {
            if (colorMaterial == null)
            {
                colorMaterial = mainRender.materials[1];
                originalColor = colorMaterial.GetColor("_Color2");
            }
        }

        public void PlayHitEffect()
        {
            hitParticle.Play();
            colorMaterial.SetColor("_Color2", hitEffectColor);

            Invoke("ReturnOriginalColor", hitEffectTime);
        }

        private void ReturnOriginalColor()
        {
            colorMaterial.SetColor("_Color2", originalColor);
        }
    }

}
