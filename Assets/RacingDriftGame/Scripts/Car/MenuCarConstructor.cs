using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

namespace RacingDriftGame.Scripts.Car
{
    public class MenuCarConstructor : MonoBehaviour, IDragHandler
    {
        private float rotationDuration = 30f;
        private int rotationDirection = -1;
        private Tween rotationTween;

        private void Start()
        {
            RotateCar();
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
    }
}
