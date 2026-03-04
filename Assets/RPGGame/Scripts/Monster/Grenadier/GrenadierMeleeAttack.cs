using System;
using UnityEngine;


namespace RPGGame
{
    public class GrenadierMeleeAttack : MonoBehaviour
    {
        [SerializeField] private float attackAmount = 0f;
        [SerializeField] private float radius = 0.2f;
        [SerializeField] private Transform[] attackPoints;
        [SerializeField] private LayerMask attackTargetLayer;

        [SerializeField] private Transform parent;

        private bool isInAttack = false;

        private void Awake()
        {
            GrenadierAttackState attackState = GetComponentInParent<GrenadierAttackState>();

            if (attackState != null)
            {
                attackState.SubscribeOnMeleeAttackCheckStart(OnAttackBegin);
                attackState.SubscribeOnAttackEnd(OnAttackEnd);
            }

            Transform refTransform = transform;
            refTransform.SetParent(parent);
            refTransform.localPosition = Vector3.zero;
            refTransform.localRotation = Quaternion.identity;
        }

        public void SetAttack(float attack)
        {
            attackAmount = attack;
        }

        private void FixedUpdate()
        {
            if(!isInAttack) return;

            Collider[] colliders = Physics.OverlapCapsule(attackPoints[0].position, attackPoints[1].position, radius, attackTargetLayer);
            if(colliders.Length == 0) return;

            foreach (Collider collider in colliders)
            {
                // Apply damage or effects to the target
                Damageable damageable = collider.GetComponent<Damageable>();
                if (damageable != null)
                {
                    damageable.ReceiveDamage(attackAmount);
                }
            }
        }

        private void OnAttackBegin()
        {
            isInAttack = true;
        }

        private void OnAttackEnd()
        {
            isInAttack = false;
        }

        // Debugging purposes: visualize the attack area in the editor
#if UNITY_EDITOR    
        private void OnDrawGizmosSelected()
        {
            if (attackPoints.Length < 2) return;
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(attackPoints[0].position, radius);
            Gizmos.DrawWireSphere(attackPoints[1].position, radius);
            Gizmos.DrawLine(attackPoints[0].position, attackPoints[1].position);
        }
#endif
    }

}
