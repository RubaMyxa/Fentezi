using UnityEngine;

namespace Assets.Game.Code.Test
{
    public class TestPlayer : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("SnowMan"))
            {
                collision.GetComponent<SnowMan>().Print();
                collision.GetComponent<SnowMan>().SnowManDestroy();
            }
        }
    }
}