using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MapGenerator
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject roomObject;
        [SerializeField] private Transform roomParent;
        [SerializeField] private Vector2 roomSize = new Vector2(100, 100);
        [SerializeField] private int mapMaxSize = 19;
        [SerializeField] private int numBattleRoom = 5;
        [SerializeField] private int numShopRoom = 1;
        [SerializeField] private int numSecretRoom = 1;
        [SerializeField] private int numAdditionalBattleRoom = 5;

        private readonly int[] _dy = { -1, 0, 0, 1 };
        private readonly int[] _dx = { 0, -1, 1, 0 };

        private enum RoomTypes
        {
            Empty,
            Main,
            Battle,
            Shop,
            Secret,
            Boss
        }

        private ArrayList GenerateList()
        {
            ArrayList list = new();

            for (var i = 0; i < numBattleRoom; i++)
            {
                list.Add(RoomTypes.Battle);
            }

            for (var i = 0; i < numShopRoom; i++)
            {
                list.Add(RoomTypes.Shop);
            }

            for (var i = 0; i < numSecretRoom; i++)
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

        private RoomTypes[,] GenerateMap()
        {
            var roomsToGenerate = GenerateList();

            var mapBoard = new RoomTypes[mapMaxSize, mapMaxSize];

            var y = mapMaxSize / 2;
            var x = mapMaxSize / 2;

            foreach (RoomTypes room in roomsToGenerate)
            {
                mapBoard[y, x] = room;
                Debug.Log($"{y} {x} {room}");

                var betterValidDirections = new ArrayList();
                var validDirections = new ArrayList();

                for (var i = 0; i < 4; i++)
                {
                    var ny = y + _dy[i];
                    var nx = x + _dx[i];

                    if (ny == -1 || ny == mapMaxSize || nx == -1 || nx == mapMaxSize) continue;
                    if (mapBoard[ny, nx] != RoomTypes.Empty) continue;

                    validDirections.Add(i);

                    var numAdjacent = 0;
                    for (var j = 0; j < 4; j++)
                    {
                        var nny = ny + _dy[i];
                        var nnx = nx + _dx[j];
                        if (mapBoard[nny, nnx] == RoomTypes.Empty) continue;
                        numAdjacent++;
                    }

                    if (numAdjacent >= 2) continue;

                    betterValidDirections.Add(i);
                }

                if (betterValidDirections.Count == 0)
                {
                    var rand = Random.Range(0, validDirections.Count);
                    var direction = (int)validDirections[rand];

                    y += _dy[direction];
                    x += _dx[direction];
                }
                else
                {
                    var rand = Random.Range(0, betterValidDirections.Count);
                    var direction = (int)betterValidDirections[rand];

                    y += _dy[direction];
                    x += _dx[direction];
                }
            }

            var currentNumAdditionalBattleRoom = 0;

            for (var i = 0; i < mapMaxSize; i++)
            {
                for (var j = 0; j < mapMaxSize; j++)
                {
                    if (mapBoard[i, j] == RoomTypes.Empty) continue;

                    var numAdjacent = 0;
                    var validDirections = new ArrayList();

                    for (var k = 0; k < 4; k++)
                    {
                        var ny = i + _dy[k];
                        var nx = j + _dx[k];

                        if (mapBoard[ny, nx] == RoomTypes.Empty)
                        {
                            validDirections.Add(k);
                        }
                        else
                        {
                            numAdjacent++;
                        }
                    }

                    if (numAdjacent >= 2) continue;

                    var rand = Random.Range(0, validDirections.Count);
                    var direction = (int)validDirections[rand];

                    var targetY = i + _dy[direction];
                    var targetX = j + _dx[direction];

                    mapBoard[targetY, targetX] = RoomTypes.Battle;

                    currentNumAdditionalBattleRoom++;

                    if (currentNumAdditionalBattleRoom >= numAdditionalBattleRoom) goto END_OF_LOOP;
                }
            }

            END_OF_LOOP:

            return mapBoard;
        }

        private void PrintMap(RoomTypes[,] mapBoard)
        {
            for (var i = 0; i < mapMaxSize; i++)
            {
                // ReSharper disable once PossibleLossOfFraction
                var y = (i - mapMaxSize / 2) * roomSize.y;

                for (var j = 0; j < mapMaxSize; j++)
                {
                    if (mapBoard[i, j] == RoomTypes.Empty) continue;

                    // ReSharper disable once PossibleLossOfFraction
                    var x = (j - mapMaxSize / 2) * roomSize.x;

                    Instantiate(roomObject, new Vector3(y, x, 0), quaternion.identity, roomParent);
                }
            }
        }

        private void Start()
        {
            var map = GenerateMap();
            PrintMap(map);
        }
    }
}