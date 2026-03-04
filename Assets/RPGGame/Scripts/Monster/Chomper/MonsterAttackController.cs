using UnityEngine;

namespace RPGGame
{
    public class MonsterAttackController : MonoBehaviour
    {
        [SerializeField] private float attackAmount;
        [SerializeField] private bool isAttack;
        [SerializeField] private float radius = 0.5f;
        [SerializeField] private LayerMask attackTargetLayer;

        private Transform refTransform;

        private void Awake()
        {
            if (refTransform == null)
            {
                refTransform = transform;
            }
        }

        public void SetAttack(float attackAmount)
        {
            this.attackAmount = attackAmount;
        }

        private void FixedUpdate()
        {
            if (!isAttack)
            {
                return;
            }

            Collider[] hitColliders = Physics.OverlapSphere(refTransform.position, radius, attackTargetLayer);
            if (hitColliders.Length == 0)
            {
                return;
            }

            foreach (Collider hitCollider in hitColliders)
            {
                Damageable damageable = hitCollider.GetComponentInParent<Damageable>();

                if (damageable != null)
                {
                    damageable.ReceiveDamage(attackAmount);

                    isAttack = false;
                    return;
                }
            }
        }
        public void OnAttackBegin()
        {
            isAttack = true;
        }

        public void OnAttackEnd()
        {
            isAttack = false;
        }

        public void OnDrawGizmos()
        {
#if UNITY_EDITOR
            if(refTransform == null)
            {
                refTransform = transform;
            }

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(refTransform.position, radius);
#endif
        }
    }

}
