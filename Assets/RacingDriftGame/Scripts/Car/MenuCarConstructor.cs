using UnityEngine;
using DG.Tweening;
using RacingDriftGame.Scripts.DataPersistenceSystem;
using RacingDriftGame.Scripts.DataPersistenceSystem.Data;
using UnityEngine.EventSystems;
using System;
using RacingDriftGame.Scripts.Collections;

namespace RacingDriftGame.Scripts.Car
{
    public class MenuCarConstructor : MonoBehaviour, IDragHandler, IDataPersistence
    {
        [SerializeField] private MeshRenderer carMesh;
        [SerializeField] private TextureCollection textureCollection;
        private float rotationDuration = 30f;
        private int rotationDirection = -1;
        private Tween rotationTween;
        private IDataPersistence _dataPersistenceImplementation;
        private int saveTextureEnumValue;

        private void Start()
        {
            LoadCarTexture();
            RotateCar();
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

        private void RotateCar()
        {
            rotationTween = transform.DORotate(new Vector3(0, -360, 0), rotationDuration, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Incremental)
                .SetEase(Ease.Linear);
        }


        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.delta.x > 0)
            {
                SetRotationDirection(-1);
            }
            else if (eventData.delta.x < 0)
            {
                SetRotationDirection(1);
            }
        }

        private void SetRotationDirection(int direction)
        {
            if (rotationDirection != direction)
            {
                rotationDirection = direction;
                rotationTween.Kill();
                RotateCar();
            }
        }

        public void SetNewTexture(Texture newTexture, TypeOfCarButton typeOfCarButton)
        {
            carMesh.materials[0].mainTexture = newTexture;
            saveTextureEnumValue = EnumToInt(typeOfCarButton);
            DataPersistenceManager.Instance.SaveGame();
        }

        private int EnumToInt(TypeOfCarButton enumValue)
        {
            return (int) enumValue;
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
            gameData.carTextureEnumNumber = saveTextureEnumValue;
        }
    }
}