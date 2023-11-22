using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MapGenerator
{
    public class MapGenerator : MonoBehaviour
    {
        private enum RoomTypes
        {
            Main,
            Battle,
            Shop,
            Secret,
            Boss
        }

        private static ArrayList GenerateList()
        {
            ArrayList list = new() { };

            for (var i = 0; i < 5; i++)
            {
                list.Add(RoomTypes.Battle);
            }

            for (var i = 0; i < 1; i++)
            {
                list.Add(RoomTypes.Shop);
            }

            for (var i = 0; i < 1; i++)
            {
                list.Add(RoomTypes.Secret);
            }

            for (var i = 0; i < list.Count; i++)
            {
                var rand1 = Random.Range(0, list.Count);
                var rand2 = Random.Range(0, list.Count);
                (list[rand1], list[rand2]) = (list[rand2], list[rand1]);
            }

            list.Insert(0, RoomTypes.Main);
            list.Add(RoomTypes.Boss);

            return list;
        }

        private static void GenerateMap()
        {
            var roomsToGenerate = GenerateList();

            foreach (var room in roomsToGenerate)
            {
                Debug.Log(room);
            }
        }

        private void Start()
        {
            GenerateMap();
        }
    }
}