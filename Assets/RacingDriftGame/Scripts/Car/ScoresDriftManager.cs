using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace RacingDriftGame.Scripts.Car
{
    public class ScoresDriftManager : MonoBehaviour
    {
        [SerializeField] private CarController player;
        [SerializeField] private TextMeshProUGUI totalScoreText, currentScoreText, factorText, driftAngleText;
        [SerializeField] GameObject driftingPanel;
        [SerializeField] private Color normalDriftColor;
        [SerializeField] private Color nearStopColor;
        [SerializeField] private Color driftEndedColor;

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

        private void Start()
        {
            driftingPanel.SetActive(false);
            playerBody = player.GetComponent<Rigidbody>();
        }

        private void Update()
        {
            ManageDrift();
            ManageUI();
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
                if (!isDrifting || stopDriftingCoroutine != null)
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

        async private void StartDrift()
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
    }
}