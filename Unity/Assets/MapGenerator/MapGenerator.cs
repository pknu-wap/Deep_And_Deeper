using System.Collections;
using System.Collections.Generic;
using Hero;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MapGenerator
{
    public class MapGenerator : MonoBehaviour
    {
        private static MapGenerator _instance;

        public static MapGenerator Instance
        {
            get
            {
                if (_instance) return _instance;
                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                _instance = new GameObject("Map Generator").AddComponent<MapGenerator>();
                DontDestroyOnLoad(_instance.gameObject);

                return _instance;
            }
        }

        private class Room
        {
            public Room(RoomTypes roomType)
            {
                RoomType = roomType;
            }

            public RoomTypes RoomType;
        }

        private class Map
        {
            public Map(int r, int c)
            {
                Rooms = new Room[r, c];

                for (var i = 0; i < r; i++)
                {
                    for (var j = 0; j < c; j++)
                    {
                        Rooms[i, j] = new Room(RoomTypes.Empty);
                    }
                }
            }

            public readonly Room[,] Rooms;
        }

        public bool needUpdate = true;

        [SerializeField] private GameObject roomObject;
        [SerializeField] private GameObject portalObject;
        [SerializeField] private GameObject holeObject;
        [SerializeField] private Transform roomParent;
        [SerializeField] private Vector2 roomSize = new Vector2(19.2f, 10.8f);
        [SerializeField] private int mapMaxSize = 19;
        [SerializeField] private int numBattleRoom = 9;
        [SerializeField] private int numShopRoom = 1;
        [SerializeField] private int numSecretRoom = 1;
        [SerializeField] private int numAdditionalBattleRoom = 6;
        [SerializeField] private Sprite doorMainSprite;
        [SerializeField] private Sprite doorBattleSprite;
        [SerializeField] private Sprite doorShopSprite;
        [SerializeField] private Sprite doorSecretSprite;
        [SerializeField] private Sprite doorBossSprite;

        private readonly Dictionary<RoomTypes, Sprite> _doorSprites = new();
        private readonly int[] _dy = { -1, 0, 1, 0 };
        private readonly int[] _dx = { 0, -1, 0, 1 };

        private Map _map;
        private bool _initialized;

        public Vector3 savedPosition;

        private enum RoomTypes
        {
            Empty,
            Main,
            Battle,
            Shop,
            Secret,
            Boss,
            Cleared
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

        private void GenerateMap()
        {
            var roomsToGenerate = GenerateList();

            _map = new Map(mapMaxSize, mapMaxSize);

            var y = mapMaxSize / 2;
            var x = mapMaxSize / 2;

            foreach (RoomTypes roomType in roomsToGenerate)
            {
                _map.Rooms[y, x].RoomType = roomType;

                var validDirections = new ArrayList();
                var betterValidDirections = new ArrayList();

                for (var i = 0; i < 4; i++)
                {
                    var ny = y + _dy[i];
                    var nx = x + _dx[i];

                    if (ny <= -1 || ny >= mapMaxSize || nx <= -1 || nx >= mapMaxSize) continue;
                    if (_map.Rooms[ny, nx].RoomType != RoomTypes.Empty) continue;

                    validDirections.Add(i);

                    var numAdjacent = 0;

                    for (var j = 0; j < 4; j++)
                    {
                        var nny = ny + _dy[j];
                        var nnx = nx + _dx[j];
                        if (nny <= -1 || nny >= mapMaxSize || nnx <= -1 || nnx >= mapMaxSize) continue;
                        if (_map.Rooms[nny, nnx].RoomType == RoomTypes.Empty) continue;
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
                    if (_map.Rooms[i, j].RoomType == RoomTypes.Empty) continue;

                    var validDirections = new ArrayList();

                    for (var k = 0; k < 4; k++)
                    {
                        var ny = i + _dy[k];
                        var nx = j + _dx[k];

                        var numAdjacent = 0;

                        for (var l = 0; l < 4; l++)
                        {
                            var nny = ny + _dy[k];
                            var nnx = nx + _dx[k];

                            if (nny <= -1 || nny >= mapMaxSize || nnx <= -1 || nnx >= mapMaxSize) continue;
                            if (_map.Rooms[nny, nnx].RoomType == RoomTypes.Empty) continue;

                            numAdjacent++;
                        }

                        if (numAdjacent >= 2) continue;

                        validDirections.Add(k);
                    }

                    if (validDirections.Count == 0) continue;

                    var rand = Random.Range(0, validDirections.Count);
                    var direction = (int)validDirections[rand];

                    var targetY = i + _dy[direction];
                    var targetX = j + _dx[direction];

                    _map.Rooms[targetY, targetX].RoomType = RoomTypes.Battle;

                    currentNumAdditionalBattleRoom++;

                    if (currentNumAdditionalBattleRoom >= numAdditionalBattleRoom) goto END_OF_LOOP;
                }
            }

            END_OF_LOOP: ;
        }

        private void PrintMap()
        {
            for (var i = 0; i < mapMaxSize; i++)
            {
                // ReSharper disable once PossibleLossOfFraction
                var y = (i - mapMaxSize / 2) * roomSize.y;

                for (var j = 0; j < mapMaxSize; j++)
                {
                    if (_map.Rooms[i, j].RoomType == RoomTypes.Empty) continue;

                    // ReSharper disable once PossibleLossOfFraction
                    var x = (j - mapMaxSize / 2) * roomSize.x;

                    var room = Instantiate(roomObject, new Vector3(x, y, 0), quaternion.identity, roomParent);

                    // 문 생성

                    for (var k = 0; k < 4; k++)
                    {
                        var ny = i + _dy[k];
                        var nx = j + _dx[k];
                        if (ny <= -1 || ny >= mapMaxSize || nx <= -1 || nx >= mapMaxSize) continue;
                        if (_map.Rooms[ny, nx].RoomType == RoomTypes.Empty) continue;

                        var door = room.transform.GetChild(k);

                        // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                        door.GetComponent<SpriteRenderer>().sprite = _doorSprites[_map.Rooms[ny, nx].RoomType];

                        // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                        door.GetComponent<BoxCollider2D>().isTrigger = true;
                    }

                    // 포탈 생성

                    if (_map.Rooms[i, j].RoomType == RoomTypes.Battle)
                    {
                        var portal = Instantiate(portalObject, new Vector3(x, y, 0), quaternion.identity, roomParent)
                            .GetComponent<Portal>();
                        portal.y = i;
                        portal.x = j;
                        return;
                    }


                    // 클리어 홀 생성

                    if (_map.Rooms[i, j].RoomType == RoomTypes.Boss)
                    {
                        var hole = Instantiate(holeObject, new Vector3(x, y, 0), quaternion.identity, roomParent)
                            .GetComponent<Hole>();
                        hole.nextStage = HeroManager.Instance._stage + 1;
                    }
                }
            }
        }

        public void Clear(int y, int x)
        {
            _map.Rooms[y, x].RoomType = RoomTypes.Main;
        }

        private void CleanMap()
        {
            if (roomParent == null) return;

            foreach (Transform child in roomParent.transform)
            {
                Destroy(child.gameObject);
            }
        }

        private void Init()
        {
            roomObject = Resources.Load<GameObject>("Room");
            portalObject = Resources.Load<GameObject>("Portal");
            holeObject = Resources.Load<GameObject>("Hole");
            doorMainSprite = Resources.Load<Sprite>("Door/DoorMain");
            doorBattleSprite = Resources.Load<Sprite>("Door/DoorBattle");
            doorShopSprite = Resources.Load<Sprite>("Door/DoorShop");
            doorSecretSprite = Resources.Load<Sprite>("Door/DoorSecret");
            doorBossSprite = Resources.Load<Sprite>("Door/DoorBoss");

            roomParent = new GameObject("Rooms").transform;

            _doorSprites[RoomTypes.Main] = doorMainSprite;
            _doorSprites[RoomTypes.Battle] = doorBattleSprite;
            _doorSprites[RoomTypes.Shop] = doorShopSprite;
            _doorSprites[RoomTypes.Secret] = doorSecretSprite;
            _doorSprites[RoomTypes.Boss] = doorBossSprite;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void CreateMap()
        {
            needUpdate = false;

            if (!_initialized)
            {
                _initialized = true;
                Init();
            }

            CleanMap();
            GenerateMap();
            PrintMap();
        }

        public void RefreshMap()
        {
            CleanMap();
            PrintMap();
        }
    }
}