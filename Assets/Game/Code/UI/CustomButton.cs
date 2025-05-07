using Assets.Game.Code.Character;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Game.Code.UI
{
    public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private ControllerHudButton controllerHudButton;

        private Player player;

        public void OnPointerDown(PointerEventData eventData)
        {
            LeftDown();
            RightDown();
            Jump();
            Attack();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            LeftUp();
            RightUp();
        }

        public void Construct(Player player)
        {
            this.player = player;
        }

        private void LeftDown()
        {
            if (controllerHudButton == ControllerHudButton.Left)
            {
                player.Horizontal = -1;
            }
        }

        private void LeftUp()
        {
            if (controllerHudButton == ControllerHudButton.Left)
            {
                player.Horizontal = 0;
            }
        }

        private void RightDown()
        {
            if (controllerHudButton == ControllerHudButton.Right)
            {
                player.Horizontal = 1;
            }
        }

        private void RightUp()
        {
            if (controllerHudButton == ControllerHudButton.Right)
            {
                player.Horizontal = 0;
            }
        }

        private void Jump()
        {
            if (controllerHudButton == ControllerHudButton.Jump)
            {
                player.Jump = true;
            }
        }

        private void Attack()
        {
            if (controllerHudButton == ControllerHudButton.Attack)
            {
                player.AttackAction = true;
            }
        }
    }
}