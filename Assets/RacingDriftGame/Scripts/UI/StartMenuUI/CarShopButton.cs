using System.Collections;
using RacingDriftGame.Scripts.Car;
using RacingDriftGame.Scripts.Collections;
using RacingDriftGame.Scripts.DataPersistenceSystem;
using RacingDriftGame.Scripts.DataPersistenceSystem.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum TypeOfCarButton
{
    Yellow,
    Blue,
    Red,
    Grey,
    Violet
}

public enum TypeOfPayment
{
    Dollars,
    Gold
}

namespace RacingDriftGame.Scripts.UI.StartMenuUI
{
    public class CarShopButton : MonoBehaviour, IDataPersistence
    {
        [SerializeField] private MenuCarConstructor carConstructor;
        [SerializeField] private TypeOfPayment typeOfPayment;
        [SerializeField] private TypeOfCarButton typeOfCarButton;
        [SerializeField] private TextureCollection textureCollection;
        [SerializeField] private Sprite coinSprite, dollarSprite;
        [SerializeField] private Transform priceObject;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private Image priceIcon;
        [SerializeField] private int price;
        private Button carButton;
        private Image wrapperImage;
        private bool isAvailable;
        private bool isBoughtRedCar;
        private bool isBoughtBlueCar;
        private bool isBoughtYellowCar;
        private bool isBoughtGreyCar;
        private bool isBoughtVioletCar;

        private void Start()
        {
            wrapperImage = GetComponent<Image>();
            carButton = GetComponentInChildren<Button>();
            carButton.onClick.AddListener(BuyNewCar);
        }

        private void UpdatePrice()
        {
            priceText.text = price.ToString();
            switch (typeOfPayment)
            {
                case TypeOfPayment.Dollars:
                    priceIcon.sprite = dollarSprite;
                    break;
                case TypeOfPayment.Gold:
                    priceIcon.sprite = coinSprite;
                    break;
            }
        }

        private IEnumerator SwitchWrapperAndPriceWithDelay(bool isBought, float time)
        {
            yield return new WaitForSeconds(time);
            if (isBought)
            {
                wrapperImage.color = Color.green;
                priceObject.gameObject.SetActive(false);
            }
            else
            {
                wrapperImage.color = Color.red;
                priceObject.gameObject.SetActive(true);
                UpdatePrice();
            }
        }

        private void BuyNewCar()
        {
            switch (typeOfCarButton)
            {
                case TypeOfCarButton.Red:
                    if (isBoughtRedCar)
                    {
                        carConstructor.SetNewTexture(textureCollection.RedTexture);
                    }
                    else
                    {
                        if (typeOfPayment == TypeOfPayment.Dollars && MoneyManager.Instance.PlayerDollars >= price)
                        {
                            MoneyManager.Instance.PlayerDollars -= price;
                            isBoughtRedCar = true;
                            StartCoroutine(SwitchWrapperAndPriceWithDelay(isBoughtRedCar, 0f));
                            carConstructor.SetNewTexture(textureCollection.RedTexture);
                            DataPersistenceManager.Instance.SaveGame();
                        }
                        else if (typeOfPayment == TypeOfPayment.Gold && MoneyManager.Instance.PlayerGold >= price)
                        {
                            MoneyManager.Instance.PlayerGold -= price;
                            isBoughtRedCar = true;
                            StartCoroutine(SwitchWrapperAndPriceWithDelay(isBoughtRedCar, 0f));
                            carConstructor.SetNewTexture(textureCollection.RedTexture);
                            DataPersistenceManager.Instance.SaveGame();
                        }
                    }
                    break;
                case TypeOfCarButton.Blue:
                    if (isBoughtBlueCar)
                    {
                        carConstructor.SetNewTexture(textureCollection.BlueTexture);
                    }
                    else
                    {
                        if (typeOfPayment == TypeOfPayment.Dollars && MoneyManager.Instance.PlayerDollars >= price)
                        {
                            MoneyManager.Instance.PlayerDollars -= price;
                            isBoughtBlueCar = true;
                            StartCoroutine(SwitchWrapperAndPriceWithDelay(isBoughtBlueCar, 0f));
                            carConstructor.SetNewTexture(textureCollection.BlueTexture);
                            DataPersistenceManager.Instance.SaveGame();
                        }
                        else if (typeOfPayment == TypeOfPayment.Gold && MoneyManager.Instance.PlayerGold >= price)
                        {
                            MoneyManager.Instance.PlayerGold -= price;
                            isBoughtBlueCar = true;
                            StartCoroutine(SwitchWrapperAndPriceWithDelay(isBoughtBlueCar, 0f));
                            carConstructor.SetNewTexture(textureCollection.BlueTexture);
                            DataPersistenceManager.Instance.SaveGame();
                        }
                    }
                    break;
                case TypeOfCarButton.Yellow:
                    if (isBoughtYellowCar)
                    {
                        carConstructor.SetNewTexture(textureCollection.YellowTexture);
                    }
                    else
                    {
                        if (typeOfPayment == TypeOfPayment.Dollars && MoneyManager.Instance.PlayerDollars >= price)
                        {
                            MoneyManager.Instance.PlayerDollars -= price;
                            isBoughtYellowCar = true;
                            StartCoroutine(SwitchWrapperAndPriceWithDelay(isBoughtYellowCar, 0f));
                            carConstructor.SetNewTexture(textureCollection.YellowTexture);
                            DataPersistenceManager.Instance.SaveGame();
                        }
                        else if (typeOfPayment == TypeOfPayment.Gold && MoneyManager.Instance.PlayerGold >= price)
                        {
                            MoneyManager.Instance.PlayerGold -= price;
                            isBoughtYellowCar = true;
                            StartCoroutine(SwitchWrapperAndPriceWithDelay(isBoughtYellowCar, 0f));
                            carConstructor.SetNewTexture(textureCollection.YellowTexture);
                            DataPersistenceManager.Instance.SaveGame();
                        }
                    }
                    break;
                case TypeOfCarButton.Grey:
                    if (isBoughtGreyCar)
                    {
                        carConstructor.SetNewTexture(textureCollection.GreyTexture);
                    }
                    else
                    {
                        if (typeOfPayment == TypeOfPayment.Dollars && MoneyManager.Instance.PlayerDollars >= price)
                        {
                            MoneyManager.Instance.PlayerDollars -= price;
                            isBoughtGreyCar = true;
                            StartCoroutine(SwitchWrapperAndPriceWithDelay(isBoughtGreyCar, 0f));
                            carConstructor.SetNewTexture(textureCollection.GreyTexture);
                            DataPersistenceManager.Instance.SaveGame();
                        }
                        else if (typeOfPayment == TypeOfPayment.Gold && MoneyManager.Instance.PlayerGold >= price)
                        {
                            MoneyManager.Instance.PlayerGold -= price;
                            isBoughtGreyCar = true;
                            StartCoroutine(SwitchWrapperAndPriceWithDelay(isBoughtGreyCar, 0f));
                            carConstructor.SetNewTexture(textureCollection.GreyTexture);
                            DataPersistenceManager.Instance.SaveGame();
                        }
                    }
                    break;
                case TypeOfCarButton.Violet:
                    if (isBoughtVioletCar)
                    {
                        carConstructor.SetNewTexture(textureCollection.VioletTexture);
                    }
                    else
                    {
                        if (typeOfPayment == TypeOfPayment.Dollars && MoneyManager.Instance.PlayerDollars >= price)
                        {
                            MoneyManager.Instance.PlayerDollars -= price;
                            isBoughtVioletCar = true;
                            StartCoroutine(SwitchWrapperAndPriceWithDelay(isBoughtVioletCar, 0f));
                            carConstructor.SetNewTexture(textureCollection.VioletTexture);
                            DataPersistenceManager.Instance.SaveGame();
                        }
                        else if (typeOfPayment == TypeOfPayment.Gold && MoneyManager.Instance.PlayerGold >= price)
                        {
                            MoneyManager.Instance.PlayerGold -= price;
                            isBoughtVioletCar = true;
                            StartCoroutine(SwitchWrapperAndPriceWithDelay(isBoughtVioletCar, 0f));
                            carConstructor.SetNewTexture(textureCollection.VioletTexture);
                            DataPersistenceManager.Instance.SaveGame();
                        }
                    }
                    break;
            }
        }

        public void LoadData(GameData gameData)
        {
            switch (typeOfCarButton)
            {
                case TypeOfCarButton.Red:
                    isBoughtRedCar = gameData.isBoughtRedCar;
                    StartCoroutine(SwitchWrapperAndPriceWithDelay(isBoughtRedCar, 0.5f));
                    break;
                case TypeOfCarButton.Blue:
                    isBoughtBlueCar = gameData.isBoughtBlueCar;
                    StartCoroutine(SwitchWrapperAndPriceWithDelay(isBoughtBlueCar, 0.5f));
                    break;
                case TypeOfCarButton.Yellow:
                    isBoughtYellowCar = gameData.isBoughtYellowCar;
                    StartCoroutine(SwitchWrapperAndPriceWithDelay(isBoughtYellowCar, 0.5f));
                    break;
                case TypeOfCarButton.Grey:
                    isBoughtGreyCar = gameData.isBoughtGreyCar;
                    StartCoroutine(SwitchWrapperAndPriceWithDelay(isBoughtGreyCar, 0.5f));
                    break;
                case TypeOfCarButton.Violet:
                    isBoughtVioletCar = gameData.isBoughtVioletCar;
                    StartCoroutine(SwitchWrapperAndPriceWithDelay(isBoughtVioletCar, 0.5f));
                    break;
            }
        }

        public void SaveData(ref GameData gameData)
        {
            switch (typeOfCarButton)
            {
                case TypeOfCarButton.Red:
                    gameData.isBoughtRedCar = isBoughtRedCar;
                    break;
                case TypeOfCarButton.Blue:
                    gameData.isBoughtBlueCar = isBoughtBlueCar;
                    break;
                case TypeOfCarButton.Yellow:
                    gameData.isBoughtYellowCar = isBoughtYellowCar;
                    break;
                case TypeOfCarButton.Grey:
                    gameData.isBoughtGreyCar = isBoughtGreyCar;
                    break;
                case TypeOfCarButton.Violet:
                    gameData.isBoughtVioletCar = isBoughtVioletCar;
                    break;
            }
        }
    }
}