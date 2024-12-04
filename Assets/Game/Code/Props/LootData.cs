using Assets.Game.Code.Containers;
using System;
using UnityEngine;

namespace Assets.Game.Code.Props
{
    [Serializable]
    public class LootData
    {
        public Loot loot;
        [Range(1, 10)]
        public int count;

        public GameObject SpawnPickup { get; private set; }

        public void Init(PropsContainer propsContainer)
        {
            SpawnPickup = propsContainer.GetPickup(loot);
        }
    }
}
