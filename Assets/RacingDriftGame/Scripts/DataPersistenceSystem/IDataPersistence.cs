using RacingDriftGame.Scripts.DataPersistenceSystem.Data;

namespace RacingDriftGame.Scripts.DataPersistenceSystem
{
    public interface IDataPersistence
    {
        void LoadData(GameData gameData);
        void SaveData(ref GameData gameData);

    }
}
