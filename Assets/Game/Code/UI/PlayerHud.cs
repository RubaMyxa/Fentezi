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

        [Header("PlayerGoals")]
        [SerializeField]
        private TextMeshProUGUI enemysText;
        [SerializeField]
        private TextMeshProUGUI keysText;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            UpdateHp();
            UpdateCoins();
            UpdateEnemys();
            UpdateKeys();

            levelManager.ActivePlayer.OnHpUpdate += UpdateHp;
            levelManager.ActivePlayer.OnCoinsUpdate += UpdateCoins;
            levelManager.ActivePlayer.OnEnemyDefeat += UpdateEnemys;
            levelManager.ActivePlayer.OnKeyCollect += UpdateKeys;
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

        private void UpdateEnemys()
        {
            enemysText.text = $"{levelManager.ActivePlayer.GetDefeatEnemys}/{levelManager.GetEnemys}";
        }

        private void UpdateKeys()
        {
            keysText.text = $"{levelManager.ActivePlayer.GetKeys}/{levelManager.GetKeys}";
        }
    }
}