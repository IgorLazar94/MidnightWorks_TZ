using RacingDriftGame.Scripts.DataPersistenceSystem;
using RacingDriftGame.Scripts.DataPersistenceSystem.Data;
using UnityEngine;
using System;
using RacingDriftGame.Scripts.Collections;

namespace RacingDriftGame.Scripts.Car
{
    public class CarView : MonoBehaviour, IDataPersistence
    {
        [SerializeField] private MeshRenderer carMesh;
        [SerializeField] private TextureCollection textureCollection;
        private int saveTextureEnumValue;

        private void Start()
        {
            LoadCarTexture();
        }

        private void LoadCarTexture()
        {
            var typeOfCarButton = IntToEnum(saveTextureEnumValue);
            switch (typeOfCarButton)
            {
                case TypeOfCarButton.Blue:
                    carMesh.materials[0].mainTexture = textureCollection.BlueTexture;
                    break;
                case TypeOfCarButton.Grey:
                    carMesh.materials[0].mainTexture = textureCollection.GreyTexture;
                    break;
                case TypeOfCarButton.Red:
                    carMesh.materials[0].mainTexture = textureCollection.RedTexture;
                    break;
                case TypeOfCarButton.Violet:
                    carMesh.materials[0].mainTexture = textureCollection.VioletTexture;
                    break;
                case TypeOfCarButton.Yellow:
                    carMesh.materials[0].mainTexture = textureCollection.YellowTexture;
                    break;
            }
        }
        
        private TypeOfCarButton IntToEnum(int intValue)
        {
            if (Enum.IsDefined(typeof(TypeOfCarButton), intValue))
            {
                return (TypeOfCarButton) intValue;
            }

            throw new ArgumentException("Invalid integer value for TypeOfCarButton");
        }
        
        public void LoadData(GameData gameData)
        {
            saveTextureEnumValue = gameData.carTextureEnumNumber;
        }

        public void SaveData(ref GameData gameData)
        {
            
        }
    }
}
