using UnityEngine;
using UnityEngine.Events;


namespace RPGGame
{
    public class HpController : MonoBehaviour
    {
        [SerializeField] private float maxHp = 0f; // Maximum HP
        [SerializeField] private float currentHp = 0f; // Current HP
        [SerializeField] private float defense = 0f; // Defense value

        // Invoked when HP changes.
        [SerializeField] private UnityEvent<float, float> OnHpChanged;

        // Invoked when HP reaches zero.
        [SerializeField] private UnityEvent OnDead;

        public void SetMaxHp(float maxHp)
        {
            this.maxHp = maxHp;
            currentHp = maxHp; // Reset current HP to max HP.

            OnHpChanged?.Invoke(currentHp, maxHp);
        }

        public void SetDefense(float defense)
        {
            this.defense = defense;
        }

        // Heal HP and clamp to max HP.
        public virtual void OnHealed(float healAmount)
        {
            currentHp = Mathf.Min(currentHp + healAmount, maxHp);
            OnHpChanged?.Invoke(currentHp, maxHp);
        }

        public virtual void OnDamaged(float damage)
        {
            float finalDamage = Mathf.Max(0f, damage - defense);
            currentHp = Mathf.Max(0f, currentHp - finalDamage);
            OnHpChanged?.Invoke(currentHp, maxHp);
            if (currentHp <= 0f)
            {
                OnDead?.Invoke();
            }
        }

        public virtual void Die()
        {
            currentHp = 0f;
            OnHpChanged?.Invoke(currentHp, maxHp);
            OnDead?.Invoke();
        }

        public void SubscribeOnDead(UnityAction onDeadAction)
        {
            OnDead?.AddListener(onDeadAction);
        }
    }
}
