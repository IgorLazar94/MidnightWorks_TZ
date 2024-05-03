using System.Collections;
using System.Globalization;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using DG.Tweening;
using RacingDriftGame.Scripts.Collections;
using UnityEngine.SceneManagement;

namespace RacingDriftGame.Scripts.Car
{
    public class ScoresDriftManager : MonoBehaviour
    {
        [SerializeField] private TypeOfGame typeOfGame;
        [SerializeField] private CarController player;

        [SerializeField]
        private TextMeshProUGUI totalScoreText, currentScoreText, factorText, driftAngleText, finishScoreText;

        [SerializeField] GameObject driftingPanel;
        [SerializeField] private Color normalDriftColor;
        [SerializeField] private Color nearStopColor;
        [SerializeField] private Color driftEndedColor;
        [SerializeField] private GameObject HUDPanel, finishScorePanel;

        private Rigidbody playerBody;
        private float speed = 0;
        private float driftAngle = 0;
        private float driftFactor = 1;
        private float currentScore;
        private float totalScore;
        private float minSpeed = 5;
        private float minAngle = 10;
        private float driftingDelay = 0.2f;
        private bool isDrifting = false;
        private IEnumerator stopDriftingCoroutine;
        private bool finishScene = false;

        private void Start()
        {
            driftingPanel.SetActive(false);
            if (typeOfGame == TypeOfGame.Single)
            {
                playerBody = player.GetComponent<Rigidbody>();
            }
        }

        private void Update()
        {
            ManageDrift();
            ManageUI();
        }

        public void SetPlayer(CarController player)
        {
            this.player = player;
            playerBody = this.player.GetComponent<Rigidbody>();
        }

        private void ManageDrift()
        {
            speed = playerBody.velocity.magnitude;
            driftAngle = Vector3.Angle(playerBody.transform.forward,
                (playerBody.velocity + playerBody.transform.forward).normalized);
            if (driftAngle > 120)
            {
                driftAngle = 0;
            }

            if (driftAngle >= minAngle && speed > minSpeed)
            {
                if (!isDrifting || stopDriftingCoroutine != null && !finishScene)
                {
                    StartDrift();
                }
                else
                {
                    if (isDrifting && stopDriftingCoroutine == null)
                    {
                        StopDrift();
                    }
                }

                if (isDrifting)
                {
                    currentScore += Time.deltaTime * driftAngle * driftFactor;
                    driftFactor += Time.deltaTime;
                    driftingPanel.SetActive(true);
                }
            }
        }

        private void ManageUI()
        {
            totalScoreText.text = "Total: " + totalScore.ToString("###,###,000");
            factorText.text = driftFactor.ToString("###,###,##0.0") + "X";
            currentScoreText.text = currentScore.ToString("###,###,000");
            driftAngleText.text = driftAngle.ToString("###,##0") + "°";
        }

        private async void StartDrift()
        {
            if (!isDrifting)
            {
                await Task.Delay(Mathf.RoundToInt(1000 * driftingDelay));
                driftFactor = 1;
            }

            if (stopDriftingCoroutine != null)
            {
                StopCoroutine(stopDriftingCoroutine);
                stopDriftingCoroutine = null;
            }

            currentScoreText.color = normalDriftColor;
            isDrifting = true;
        }

        private void StopDrift()
        {
            stopDriftingCoroutine = StoppingDrift();
            StartCoroutine(stopDriftingCoroutine);
        }

        private IEnumerator StoppingDrift()
        {
            yield return new WaitForSeconds(0.1f);
            currentScoreText.color = nearStopColor;
            yield return new WaitForSeconds(driftingDelay * 4f);
            totalScore += currentScore;
            isDrifting = false;
            currentScoreText.color = driftEndedColor;
            yield return new WaitForSeconds(0.5f);
            currentScore = 0;
            driftingPanel.SetActive(false);
        }

        public void CalculateTotalScoreInMoney()
        {
            finishScene = true;

            if (stopDriftingCoroutine != null)
            {
                StopCoroutine(stopDriftingCoroutine);
                stopDriftingCoroutine = null;
            }

            var addMoney = Mathf.RoundToInt(totalScore / 100);
            MoneyManager.Instance.PlayerDollars += addMoney;
            MoneyManager.Instance.FastSaveMoneyToJSON();

            float x = 0;
            finishScorePanel.SetActive(true);

            DOTween.To(() => x, newValue =>
            {
                x = newValue;
                finishScoreText.text = Mathf.RoundToInt(x).ToString();
            }, addMoney, 4f);

            DOTween.To(() => totalScore, newValue =>
                {
                    totalScore = newValue;
                    totalScoreText.text = $"Total: {totalScore:###,###,000}";
                }, 0, 4f)
                .OnComplete(() => SceneManager.LoadScene(SceneNames.GarageMenuScene));
        }
    }
}