using UnityEngine;

namespace Assets.Game.Code.Test
{
    public class TestPlayer : MonoBehaviour
    {
        private Vector2 targetPosition;

        private void Awake()
        {
            targetPosition = transform.position;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("SnowMan"))
            {
                collision.GetComponent<SnowMan>().Teleport(targetPosition);
                collision.GetComponent<SnowMan>().Print();
                collision.GetComponent<SnowMan>().Scale(new Vector2(1.5f, 1.5f));
                collision.GetComponent<SnowMan>().SnowManDestroyTrigger();
                collision.GetComponent<SnowMan>().Spawn();
                //collision.GetComponent<SnowMan>().SnowManDestroy();
            }


        }
    }
}