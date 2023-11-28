using UnityEngine;

namespace Shop.Items
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private int index;
        [SerializeField] private int price;
        [SerializeField] private string itemName;
        [SerializeField] private string description;
    }
}