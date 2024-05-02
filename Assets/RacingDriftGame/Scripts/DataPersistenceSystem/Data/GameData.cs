namespace RacingDriftGame.Scripts.DataPersistenceSystem.Data
{
    [System.Serializable]
    public class GameData
    {
        public bool isNewGame;
        public int dollars;
        public int coins;
        public bool isBoughtRedCar;
        public bool isBoughtBlueCar;
        public bool isBoughtYellowCar;
        public bool isBoughtGreyCar;
        public bool isBoughtVioletCar;

        public GameData()
        {
            // Default settings
            isNewGame = false;
            dollars = 0;
            coins = 0;
            isBoughtRedCar = false;
            isBoughtBlueCar = false;
            isBoughtYellowCar = true;
            isBoughtGreyCar = false;
            isBoughtVioletCar = false;
        }
    }
}
