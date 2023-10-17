using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "item_Data", menuName = "Scriptable Object/item_Data", 
    order = int.MaxValue)]

    public class item_data : ScriptableObject
    {
        private int idx;
        public int IDX
        {
            get { return idx;  }
        }

        private string item_name;
        public string Item_name
        {
            get {
                return item_name;
            }
        }
        private int price;
        public int Price
        {
            get { return price;  }
        }

        private int hp;
        private int Hp
        {
            get { return hp; }
        }

        private float moveSpeed;
        public float MoveSpeed
        {
            get { return moveSpeed; }
        }

    }
