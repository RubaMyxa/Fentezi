using UnityEngine;

namespace Assets.Game.Code.AI.Enemys.Skeleton
{
    public class SkeletoArcher : SkeletonBase
    {
        [SerializeField]
        private GameObject arrow;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Update()
        {
            base.Update();

            CalculateDirection();
        }

        protected override void CalculateDirection()
        {
            base.CalculateDirection();

            if (!player || !IsAlive)
            {
                return;
            }

            if (player.transform.position.x - transform.position.x < 0)
            {
                direction = -1;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                direction = 1;
                transform.localScale = new Vector3(1, 1, 1);
            }
        }

        private void AttackArrow()
        {
            GameObject a = Instantiate(arrow, attackPoint.position, Quaternion.identity);
            a.transform.localScale = transform.localScale;

            Invoke("AttackEnd", attackCooldown);
        }
    }
}