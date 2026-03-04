using System;
using UnityEngine;


namespace RPGGame
{
    public class GrenadierRangeAttack : MonoBehaviour
    {
        [SerializeField] private float attackAmount = 0f;
        [SerializeField] private float radius = 1f;
        [SerializeField] private LayerMask attackLayerMask;

        private Transform refTransform;

        private void Awake()
        {
            if (refTransform == null)
                refTransform = transform;

            GrenadierAttackState attackState = GetComponentInParent<GrenadierAttackState>();
            if (attackState != null)
            {
                attackState.SubscribeOnRangeAttackStart(Attack);
            }
        }

        public void Attack()
        {
            Collider[] colliders = Physics.OverlapSphere(refTransform.position, radius, attackLayerMask);
            if (colliders == null) return;
            foreach (Collider collider in colliders)
            {
                Damageable damageable = collider.GetComponent<Damageable>();
                if (damageable == null)
                {
                    continue;
                }

                damageable.ReceiveDamage(attackAmount);
            }
        }

        public void SetAttack(float attack)
        {
            attackAmount = attack;
        }

        public void SetAttackRange(float range)
        {
            radius = range;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (refTransform == null)
                refTransform = transform;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(refTransform.position, radius);
        }
#endif
    }

}
