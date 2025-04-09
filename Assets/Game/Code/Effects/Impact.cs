using UnityEngine;

namespace Assets.Game.Code.Effects
{
    public class Impact : MonoBehaviour
    {
        private void ImpactEnd()
        {
            Destroy(gameObject);
        }
    }
}