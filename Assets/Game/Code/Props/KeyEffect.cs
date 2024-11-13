using UnityEngine;

namespace Assets.Game.Code.Props
{
    public class KeyEffect : MonoBehaviour
    {
        private void EffectEnd()
        {
            Debug.Log("EffectEnd");
            Destroy(gameObject);
        }
    }
}
