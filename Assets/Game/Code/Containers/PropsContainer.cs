using UnityEngine;

namespace Assets.Game.Code.Containers
{
    [CreateAssetMenu(fileName = "Container", menuName = "SO/Container")]
    public class PropsContainer : ScriptableObject
    {
        public GameObject pickupCoin;
        public GameObject pickupKey;

        public GameObject GetPickup(Loot loot)
        {
            if(loot == Loot.Coin)
                return pickupCoin;
            else if (loot == Loot.Key)
                return pickupKey;
            else
                return null;
        }
    }
}