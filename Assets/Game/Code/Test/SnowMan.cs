using UnityEngine;

namespace Assets.Game.Code.Test
{
    public class SnowMan : MonoBehaviour
    {
        public void Print()
        {
            print(transform.position);
        }

        public void SnowManDestroy()
        {
            Destroy(gameObject);
        }
    }
}