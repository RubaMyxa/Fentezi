using Assets.Game.Code.Character;
using UnityEngine;

public class DangerousZone : MonoBehaviour
{
    [SerializeField]
    private int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(damage, transform.position);
        }
    }
}
