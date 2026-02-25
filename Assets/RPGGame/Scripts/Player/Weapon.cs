using UnityEngine;


namespace RPGGame
{
    public class Weapon : CollectableItem
    {
        [SerializeField] private float attackAmount = 0f;
        [SerializeField] private float radius = 0.1f;
        [SerializeField] private ParticleSystem hitParticle;
        [SerializeField] private Transform[] attackPoints;
        [SerializeField] private LayerMask attackTargetLayer;

        private bool isInAttack = false;

        protected override void Awake()
        {
            base.Awake();

            WeaponItem weaponItem = item as WeaponItem;
            if (weaponItem != null)
            {
                attackAmount = weaponItem.attack;
            }
        }

        protected override void OnCollect(Collider other)
        {
            //base.OnCollect(other);
            if (!HasCollected && other.CompareTag("Player"))
            {
                WeaponController weaponController = other.GetComponentInChildren<WeaponController>();
                if (weaponController != null)
                {
                    weaponController.AttachWeapon(this);
                }




                // 무기를 수집했을 때 플레이어의 공격력을 증가시키는 로직을 여기에 추가할 수 있습니다.
                OnItemCollected?.Invoke();  // 아이템이 수집됐을 때 실행할 이벤트
            }
        }

        public void Attach(Transform parentTransform)
        {
            refTransform.SetParent(parentTransform);
            refTransform.localPosition = Vector3.zero;
            refTransform.localRotation = Quaternion.identity;

            HasCollected = true;
        }

        // 공격 판정을 시작할 때 호출되는 메서드
        public void OnAttackBegin()
        {
            isInAttack = true;
        }


        // 공격 판정을 종료할 때 호출되는 메서드
        public void OnAttackEnd()
        {
            isInAttack = false;
        }

        private void FixedUpdate()
        {
            if(!isInAttack)
            {
                return;
            }

            Collider[] colliders = Physics.OverlapCapsule(attackPoints[0].position, attackPoints[1].position, radius, attackTargetLayer);

            if (colliders.Length == 0) { return; }

            foreach(Collider collider in colliders)
            {
                Util.LogRed("무기에 맞음");

                Damageable damageable = collider.GetComponent<Damageable>();

                if (damageable != null)
                {
                    damageable.ReceiveDamage(attackAmount);
                }


                if (hitParticle != null)
                {
                    hitParticle.transform.position = collider.transform.position;

                    hitParticle.Play();
                }
            }

            

        }

    }

}
