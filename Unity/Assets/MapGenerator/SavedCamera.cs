using System;
using UnityEngine;

namespace MapGenerator
{
    public class SavedCamera : MonoBehaviour
    {
        private void Awake()
        {
            transform.position = MapGenerator.Instance.savedCameraPosition;
        }
    }
}