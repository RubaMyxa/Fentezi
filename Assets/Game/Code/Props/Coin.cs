using UnityEngine;

namespace Assets.Game.Code.Props
{
    public class Coin : MonoBehaviour
    {
        [SerializeField]
        private GameObject coinEffect;

        public void Collect()
        {
            Instantiate(coinEffect, gameObject.transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}