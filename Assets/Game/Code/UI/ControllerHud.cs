using Assets.Game.Code.Character;
using UnityEngine;

namespace Assets.Game.Code.UI
{
    public class ControllerHud : MonoBehaviour
    {
        [SerializeField]
        private CustomButton leftButton;
        [SerializeField]
        private CustomButton rightButton;
        [SerializeField]
        private CustomButton jumpButton;
        [SerializeField]
        private CustomButton attackButton;

        public void Construct(Player player)
        {
            leftButton.Construct(player);
            rightButton.Construct(player);
            jumpButton.Construct(player);
            attackButton.Construct(player);
        }
    }
}