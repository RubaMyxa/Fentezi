using UnityEngine;

namespace Assets.Game.Code.Pickup
{
    public class Pickup : MonoBehaviour
    {
        [SerializeField]
        private GameObject realProp;

        private float timer = 0.1f;

        private Rigidbody2D rb;

        private void Awake()
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
            rb.AddRelativeForce(new Vector2(Random.Range(-100, 100), 300));
        }

        private void Update()
        {
            timer -= Time.deltaTime;
            if(timer > 0)
            {
                return;
            }

            if (rb.linearVelocity.magnitude == 0)
            {
                GameObject prop = Instantiate(realProp);
                prop.transform.position = transform.position;

                Destroy(gameObject);
            }
        }
    }
}