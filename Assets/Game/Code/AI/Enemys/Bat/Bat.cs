using UnityEngine;

namespace Assets.Game.Code.AI.Enemys.Bat
{
    public class Bat : MonoBehaviour
    {
        [SerializeField]
        private Transform[] points;

        private int currentTargetIndex = 0;
        private Transform currentTarget;

        private float speed = 2;
        private int direction = 1; // 1 or -1

        private void Awake()
        {
            currentTarget = points[currentTargetIndex];

            foreach (Transform point in points)
            {
                point.parent = null;
            }
        }

        private void Update()
        {
            Movement();
        }

        private void Movement()
        {
            transform.position += transform.right * Time.deltaTime * speed * direction;

            if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
            {
                currentTargetIndex += 1;

                if(currentTargetIndex == points.Length)
                {
                    currentTargetIndex = 0;
                }

                currentTarget = points[currentTargetIndex];
                direction *= -1;
                transform.localScale = new Vector3(direction, 1, 1);
            }
        }
    }
}