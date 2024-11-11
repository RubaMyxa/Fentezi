using UnityEngine;

namespace Assets.Game.Code.Props
{
    public class CoinEffect : MonoBehaviour
    {
        private void EffectEnd()
        {
            Debug.Log("EffectEnd");
            Destroy(gameObject);
        }
    }
}