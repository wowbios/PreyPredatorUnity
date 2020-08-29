using System;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public struct AnimalsSettings
    {
        [SerializeField] public GameObject Prefab;
        [SerializeField] public int Count;
    }
}