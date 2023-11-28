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

        private bool _playerTriggers;

        private void Start()
        {
            _itemText = GameObject.FindGameObjectWithTag("ItemText").GetComponent<TextMeshPro>();
        }

        private void ItemEffect()
        {
            switch (index)
            {
                case 0:
                    HeroManager.Instance.AddHealth(100);
                    break;
                case 1:
                    HeroManager.Instance.AddHealth(200);
                    break;
                case 2:
                    HeroManager.Instance.AddMaxStamina(10);
                    break;
                case 3:
                    HeroManager.Instance.AddMaxStamina(20);
                    break;
            }
        }

        private void Update()
        {
            if (!_playerTriggers) return;
            if (!Input.GetKeyDown(KeyCode.Space)) return;
            if (HeroManager.Instance.Money < price) return;

            _playerTriggers = false;
            HeroManager.Instance.AddMoney(-price);
            ItemEffect();
            _itemText.text = "고마워!";
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var additionalDescription = HeroManager.Instance.Money >= price ? HasEnoughGold : NotEnoughGold;
            _itemText.text = $"{itemName}\n{description}\n{additionalDescription}";
            _playerTriggers = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _itemText.text = "";
            _playerTriggers = false;
        }
    }
}