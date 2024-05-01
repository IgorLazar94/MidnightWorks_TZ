using System.Collections;
using TMPro;
using UnityEngine;

namespace RacingDriftGame.Scripts
{
    public class LevelTimerController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI remainingTimeText;
        private float remainingTime = 120f;
        
        private void Start()
        {
            ChangeRemainingTimeTextColor(Color.green);
            StartCoroutine(CountdownTimerCoroutine());
        }

        private IEnumerator CountdownTimerCoroutine()
        {
            while (remainingTime > 0)
            {
                remainingTimeText.text = string.Format("{0:00}:{1:00}", 
                    Mathf.FloorToInt(remainingTime / 60), 
                    Mathf.FloorToInt(remainingTime % 60));
                
                if (remainingTime <= 10)
                {
                    ChangeRemainingTimeTextColor(Color.red);
                }
                else if (remainingTime <= 60)
                {
                    ChangeRemainingTimeTextColor(Color.yellow);
                }
                
                yield return new WaitForSeconds(1);

                remainingTime -= 1f;
            }
            remainingTimeText.text ="00:00";
        }
        
        private void ChangeRemainingTimeTextColor(Color newColor)
        {
            remainingTimeText.color = newColor;
        }
    }
}
