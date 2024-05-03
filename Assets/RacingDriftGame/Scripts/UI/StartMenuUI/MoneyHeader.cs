using System;
using TMPro;
using UnityEngine;

namespace RacingDriftGame.Scripts.UI.StartMenuUI
{
    public class MoneyHeader : MonoBehaviour
    {
        public static Action<int, int> OnUpdateMoneyText;
        [SerializeField] private TextMeshProUGUI playerDollarText;
        [SerializeField] private TextMeshProUGUI playerGoldText;

        private void Start()
        {
            OnUpdateMoneyText.Invoke(MoneyManager.Instance.PlayerDollars, MoneyManager.Instance.PlayerGold);
        }

        private void OnEnable()
        {
            OnUpdateMoneyText += UpdateMoneyText;
        }

        private void OnDisable()
        {
            OnUpdateMoneyText -= UpdateMoneyText;
        }

        private void UpdateMoneyText(int playerDollars, int playerGold)
        {
            playerDollarText.text = playerDollars.ToString();
            playerGoldText.text = playerGold.ToString();
        }
    }
}
