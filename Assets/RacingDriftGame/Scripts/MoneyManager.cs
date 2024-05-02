using UnityEngine;

namespace RacingDriftGame.Scripts
{
    public class MoneyManager : MonoBehaviour
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
                if (value >= 0)
                {
                    playerDollars = value;
                }
            }
        }
        public int PlayerGold
        {
            get => playerGold;
            set
            {
                if (value >= 0)
                {
                    playerGold = value;
                }
            }
        }

        private static MoneyManager _instance;
        private int playerDollars;
        private int playerGold;

        private void Awake()
        {
            MakeSingleton();
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
    }
}