using System.Collections.Generic;
using System.Linq;
using RacingDriftGame.Scripts.DataPersistenceSystem.Data;
using UnityEngine;

namespace RacingDriftGame.Scripts.DataPersistenceSystem
{
    public class DataPersistenceManager : MonoBehaviour, IDataPersistence
    {
        public static DataPersistenceManager Instance { get; private set; }

        public static bool isNewGame;
        [Header("File Storage Config")]
        [SerializeField] private string fileName;
        private GameData gameData;
        private List<IDataPersistence> dataPersistenceObjects;
        private FileDataHandler dataHandler;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Found more that one Data Persistence Manager in the Scene");
            }
            Instance = this;
            this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, false);
            this.dataPersistenceObjects = FindAllDataPersistenceObjects();
            LoadGame();
        }

        public void NewGame()
        {
            isNewGame = true;
            this.gameData = new GameData();
        }

        public void LoadGame()
        {
            this.gameData = dataHandler.Load();

            if (this.gameData == null)
            {
                Debug.Log("No data was found, initializing data to defaults");
                NewGame();
            }
            else
            {
                isNewGame = false;
            }

            foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
            {
                dataPersistenceObj.LoadData(gameData);
            }
        }

        public void SaveGame()
        {
            foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
            {
                dataPersistenceObj.SaveData(ref gameData);
            }

            dataHandler.Save(gameData);
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }

        private List<IDataPersistence> FindAllDataPersistenceObjects()
        {
            IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

            return new List<IDataPersistence>(dataPersistenceObjects);
        }

        public void LoadData(GameData gameData)
        {
            gameData.isNewGame = isNewGame;
        }

        public void SaveData(ref GameData gameData)
        {
            isNewGame = gameData.isNewGame;
        }
    }
}
