using Assets.Game.Code.AI.Enemys.Skeleton;
using Assets.Game.Code.Character;
using UnityEngine;

namespace Assets.Game.Code.Infrastacture
{
    public class BossLevelEscape : MonoBehaviour
    {
        [SerializeField]
        private GameObject mineEnd;
        [SerializeField]
        private Boss boss;
        [Space]
        [SerializeField]
        private Transform pointA;
        [SerializeField]
        private Transform pointB;

        private Player player;

        private void OnEnable()
        {
            boss.OnDie += BossDie;
        }

        private void Start()
        {
            player = FindFirstObjectByType<Player>();
        }

        private void BossDie()
        {
            float distanceA = Vector2.Distance(player.transform.position, pointA.position);
            float distanceB = Vector2.Distance(player.transform.position, pointB.position);

            if (distanceA < distanceB) // B
            {
                mineEnd.transform.position = pointB.position;
                mineEnd.transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else // A
            {
                mineEnd.transform.position = pointA.position;
                mineEnd.transform.eulerAngles = new Vector3(0, 0, 0);
            }

            mineEnd.SetActive(true);
        }
    }
}