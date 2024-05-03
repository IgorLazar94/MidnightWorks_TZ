using System;
using System.Collections.Generic;
using System.Linq;
using RacingDriftGame.Scripts.Collections;
using RacingDriftGame.Scripts.Photon;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RacingDriftGame.Scripts.UI.StartMenuUI
{
    public enum TypeOfPanel
    {
        IAP,
        CarShop,
        Customization,
        Settings,
        Levels,
        CurrencyExchange
    }

    public class MenuUI : MonoBehaviour
    {
        public static Action OnUpdateCarShopPrices;
        [SerializeField] private RectTransform iAP, carShop, customization, settings, levels, currencyExchange;
        [SerializeField] private ConnectToServer connectToServer;
        private RectTransform lastOpenPanel;
        private List<MenuNavigationButton> menuNavigationButtons;

        private void Start()
        {
            menuNavigationButtons = GetComponentsInChildren<MenuNavigationButton>().ToList();
            lastOpenPanel = carShop;
            foreach (var button in menuNavigationButtons)
            {
                button.SetMainUI(this);
            }
        }

        public void OpenNewPanel(TypeOfPanel actualPanel)
        {
            if (lastOpenPanel != null)
            {
                lastOpenPanel.gameObject.SetActive(false);
            }
            switch (actualPanel)
            {
                case TypeOfPanel.IAP:
                    iAP.gameObject.SetActive(true);
                    lastOpenPanel = iAP;
                    break;
                case TypeOfPanel.CarShop:
                    carShop.gameObject.SetActive(true);
                    OnUpdateCarShopPrices.Invoke();
                    lastOpenPanel = carShop;
                    break;
                case TypeOfPanel.Customization:
                    customization.gameObject.SetActive(true);
                    CustomizationButton.OnUpdateCustomizationButtons.Invoke();
                    lastOpenPanel = customization;
                    break;
                case TypeOfPanel.Settings:
                    settings.gameObject.SetActive(true);
                    lastOpenPanel = settings;
                    break;
                case TypeOfPanel.Levels:
                    levels.gameObject.SetActive(true);
                    lastOpenPanel = levels;
                    break;
                case TypeOfPanel.CurrencyExchange:
                    currencyExchange.gameObject.SetActive(true);
                    lastOpenPanel = currencyExchange;
                    break;
            }
        }

        public void MoveToLobby()
        {
            connectToServer.gameObject.SetActive(true);
        }
    }
}