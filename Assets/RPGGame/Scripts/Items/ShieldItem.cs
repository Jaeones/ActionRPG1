using UnityEngine;

namespace RPGGame
{
    [CreateAssetMenu(fileName = "New Shield Item", menuName = "Inventory/Item/ShieldItem")]
    public class ShieldItem : Item
    {
        public float shieldAmount; //회복량

        private void Awake()
        {
            itemName = "방어";
        }

        public override void Use()
        {
            base.Use();

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Damageable damageable = player.transform.root.GetComponentInChildren<Damageable>();
                if (damageable != null)
                {
                    damageable.SetDefense(shieldAmount);
                }
            }
        }
    }
}
