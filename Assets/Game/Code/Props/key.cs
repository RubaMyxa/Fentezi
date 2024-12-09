using UnityEngine;

namespace Assets.Game.Code.Props
{
    public class Key : MonoBehaviour
    {
        [SerializeField]
        private GameObject keyEffect;

        public void Collect()
        {
            Instantiate(keyEffect, gameObject.transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}