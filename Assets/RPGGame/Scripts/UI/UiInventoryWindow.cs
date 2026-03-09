using System.Collections.Generic;
using UnityEngine;

namespace RPGGame
{
    public class UiInventoryWindow : MonoBehaviour
    {
        private static UiInventoryWindow instance;

        [SerializeField] private GameObject window;
        [SerializeField] private RectTransform contentTransform;
        [SerializeField] private UiInventoryListItem itemPrefab;
        [SerializeField] private float itemWidth;
        [SerializeField] private float itemHeight;
        [SerializeField] private List<UiInventoryListItem> items = new List<UiInventoryListItem>();

        public static bool IsOn { get { return instance.window.activeSelf; } }

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);
        }

        private void OnEnable()
        {
            InventoryManager.Instance.SubscribeOnItemListChanged(OnItemListChanged);
        }

        public static void OnItemListChanged()
        {
            if (instance.items.Count == InventoryManager.Instance.ItemCount)
            {
                foreach (var item in instance.items)
                {
                    ItemSlot itemSlot = InventoryManager.Instance.GetItemSlot(item.item);
                    item.SetCount(itemSlot.count);
                }
                return;
            }

            foreach (var item in instance.items)
            {
                Destroy(item.gameObject);
            }

            instance.items.Clear();

            const int maxXCount = 4;
            List<ItemSlot> itemList = InventoryManager.Instance.GetItems();

            foreach (var itemSlot in itemList)
            {
                UiInventoryListItem newItem = Instantiate(instance.itemPrefab, instance.contentTransform);
                newItem.SetItem(itemSlot.item);
                newItem.SetSprite(itemSlot.item.itemIcon);
                newItem.SetName(itemSlot.item.itemName);
                newItem.SetCount(itemSlot.count);
                instance.items.Add(newItem);
            }

            Vector2 contentSize = instance.contentTransform.sizeDelta;
            float lineCount = itemList.Count / maxXCount + 1;
            contentSize.y = lineCount * instance.itemHeight + (lineCount - 1) * 10f + 20f;
            instance.contentTransform.sizeDelta = contentSize;
        }

        public static void ShowWindow()
        {
            instance.window.SetActive(true);
        }

        public static void CloseWindow()
        {
            instance.window.SetActive(false);
        }
    }
}
