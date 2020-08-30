using System;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public struct SpawnerSettings
    {
        [SerializeField] public GameObject Entity;
        [SerializeField] public int Count;
        [SerializeField] public bool OnInit;
        [SerializeField] public bool OnTick;
        [SerializeField] public float SpawnMilliseconds;
    }
}