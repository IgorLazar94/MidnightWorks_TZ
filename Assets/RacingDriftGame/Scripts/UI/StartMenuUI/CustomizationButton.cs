using System;
using RacingDriftGame.Scripts.Car;
using RacingDriftGame.Scripts.DataPersistenceSystem;
using RacingDriftGame.Scripts.DataPersistenceSystem.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RacingDriftGame.Scripts.UI.StartMenuUI
{
    public class CustomizationButton : MonoBehaviour, IDataPersistence
    {
        public static Action OnUpdateCustomizationButtons;
        [SerializeField] private MenuCarConstructor carConstructor;
        [SerializeField] private Transform priceObject;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private int price;
        private Button carButton;
        private Image wrapperImage;
        private bool isBought;
        private bool isHasSpoiler;

        private void OnEnable()
        {
            OnUpdateCustomizationButtons += UpdateButtonInfo;
        }

        private void OnDisable()
        {
            OnUpdateCustomizationButtons -= UpdateButtonInfo;
        }

        private void Start()
        {
            UpdateButtonInfo();
        }

        private void UpdateButtonInfo()
        {
            if (isBought)
            {
                priceObject.gameObject.SetActive(false);
                
            }
            else
            {
                priceObject.gameObject.SetActive(true);
                priceText.text = price.ToString();
            }
        }

        public void BuyActivateSpoiler() // OnCLickEvent
        {
            if (isBought)
            {
                carConstructor.ActivateSwitchSpoiler();
                    
            }
            if (price <= MoneyManager.Instance.PlayerDollars && !isBought)
            {
                MoneyManager.Instance.PlayerDollars -= price;
                isBought = true;
                DataPersistenceManager.Instance.SaveGame();
                carConstructor.ActivateSwitchSpoiler();
                UpdateButtonInfo();
            }
        }

        public void LoadData(GameData gameData)
        {
            isBought = gameData.isSpoilerBought;
        }

        public void SaveData(ref GameData gameData)
        {
            gameData.isSpoilerBought = isBought;
        }
    }
}