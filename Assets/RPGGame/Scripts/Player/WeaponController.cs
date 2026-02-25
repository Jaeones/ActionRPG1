using UnityEngine;


namespace RPGGame
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Transform weaponHolder;

        [SerializeField] private Weapon weapon;

        [SerializeField] private PlayerAttackEffect[] attackEffects;

        [SerializeField] private AudioPlayer swingSound;

        public bool IsWeaponAttached { get; private set; }

        public void AttachWeapon(Weapon weapon)
        {
            if (IsWeaponAttached)
            {
                Debug.LogWarning("이미 무기가 장착되어 있습니다.");
                return;
            }

            weapon.Attach(weaponHolder);
            swingSound = GetComponentInChildren<AudioPlayer>();
            this.weapon = weapon;
            IsWeaponAttached = true;

            PlayerAttackState playerAttackState = transform.root.GetComponentInChildren<PlayerAttackState>();
            if (playerAttackState != null)
            {
                playerAttackState.SubscribeOnAttackBegin(weapon.OnAttackBegin);

                playerAttackState.SubscribeOnAttackEnd(weapon.OnAttackEnd);
            }
        }

        public void PlayAttackComboEffect(int comboIndex)
        {
            if (comboIndex < 0 || comboIndex >= attackEffects.Length)
            {
                Debug.LogWarning("콤보 인덱스가 범위를 벗어났습니다.");
                return;
            }

            attackEffects[comboIndex].Activate();

            swingSound.Play();
        }
    }

}
