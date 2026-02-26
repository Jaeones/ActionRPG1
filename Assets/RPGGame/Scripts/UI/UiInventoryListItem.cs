using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


namespace RPGGame
{
    public class UiInventoryListItem : MonoBehaviour, IPointerClickHandler
    {
        public Item item;
        public Image image;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI countText;

        public void SetItem(Item item)
        {
            this.item = item;
        }

        public void SetSprite(Sprite sprite)
        {
            image.sprite = sprite;
        }

        public void SetName(string name)
        {
            nameText.text = name;
        }

        public void SetCount(int count)
        {
            countText.text = $"{count}";
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            item.Use();
        }
    }

}
