using System;
using RacingDriftGame.Scripts.DataPersistenceSystem;
using RacingDriftGame.Scripts.DataPersistenceSystem.Data;
using RacingDriftGame.Scripts.UI.StartMenuUI;
using UnityEngine;

namespace RacingDriftGame.Scripts
{
    public class MoneyManager : MonoBehaviour, IDataPersistence
    {
        
        public static MoneyManager Instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = FindObjectOfType<MoneyManager>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<MoneyManager>();
                    singletonObject.name = typeof(MoneyManager).ToString() + " (Singleton)";
                    DontDestroyOnLoad(singletonObject);
                }
                else
                {
                    DontDestroyOnLoad(_instance.gameObject);
                }

                return _instance;
            }
        }
        public int PlayerDollars
        {
            get => playerDollars;
            set
            {
                if (value < 0) return;
                playerDollars = value;
                MoneyHeader.OnUpdateMoneyText?.Invoke(playerDollars, playerGold);
                DataPersistenceManager.Instance.SaveGame();
            }
        }
        public int PlayerGold
        {
            get => playerGold;
            set
            {
                if (value < 0) return;
                playerGold = value;
                MoneyHeader.OnUpdateMoneyText?.Invoke(playerDollars, playerGold);
                DataPersistenceManager.Instance.SaveGame();
            }
        }

        private static MoneyManager _instance;
        private int playerDollars;
        private int playerGold;

        private void Awake()
        {
            MakeSingleton();
        }

        private void Start()
        {
            MoneyHeader.OnUpdateMoneyText?.Invoke(playerDollars, playerGold);
        }

        private void MakeSingleton()
        {
            if (_instance == null)
            {
                _instance = this as MoneyManager;
                DontDestroyOnLoad(this.gameObject);
            }
            else if (_instance != this)
            {
                Destroy(this.gameObject);
            }
        }

        public void FastSaveMoneyToJSON()
        {
            DataPersistenceManager.Instance.SaveGame();
        }

        public void LoadData(GameData gameData)
        {
            playerDollars = gameData.dollars;
            playerGold = gameData.coins;
        }

        public void SaveData(ref GameData gameData)
        {
            gameData.dollars = playerDollars;
            gameData.coins = playerGold;
        }
    }
}