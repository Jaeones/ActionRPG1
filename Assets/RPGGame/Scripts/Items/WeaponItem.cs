using UnityEngine;


namespace RPGGame
{
    [CreateAssetMenu(fileName = "New Weapon Item", menuName = "Inventory/Item/WeaponItem")]

    public class WeaponItem : Item
    {
        public float attack; //무기의 공격력

        public void Awake()
        {
            itemName = "무기";
        }
    }

}

