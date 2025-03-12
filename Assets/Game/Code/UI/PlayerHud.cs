using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Game.Code.Infrastacture;

namespace Assets.Game.Code.UI
{
    public class PlayerHud : MonoBehaviour
    {
        [SerializeField]
        private LevelManager levelManager;

        [Header("PlayerStats")]
        [SerializeField]
        private Image hpBarFill;
        [SerializeField]
        private TextMeshProUGUI hpBarText;
        [SerializeField]
        private TextMeshProUGUI coinsText;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            UpdateHp();
            UpdateCoins();

            levelManager.ActivePlayer.OnHpUpdate += UpdateHp;
            levelManager.ActivePlayer.OnCoinsUpdate += UpdateCoins;
        }

        private void UpdateHp()
        {
            hpBarFill.fillAmount = levelManager.ActivePlayer.GetHp / 100f;
            hpBarText.text = $"{levelManager.ActivePlayer.GetHp} / 100";
        }

        private void UpdateCoins()
        {
            coinsText.text = $"{levelManager.ActivePlayer.GetCoin}";
        }
    }
}