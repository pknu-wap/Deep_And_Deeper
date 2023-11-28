using Hero;
using TMPro;
using UnityEngine;

namespace Shop.Items
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private int index;
        [SerializeField] private int price;
        [SerializeField] private string itemName;
        [SerializeField] private string description;

        private const string HasEnoughGold = "구매하려면 스페이스바를 눌러.";
        private const string NotEnoughGold = "골드가 부족한 것 같은데?";
        private TextMeshPro _itemText;

        private void Start()
        {
            _itemText = GameObject.FindGameObjectWithTag("ItemText").GetComponent<TextMeshPro>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var additionalDescription = HeroManager.Instance.Money >= price ? HasEnoughGold : NotEnoughGold;
            _itemText.text = $"{itemName}\n{description}\n{additionalDescription}";
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _itemText.text = "";
        }
    }
}