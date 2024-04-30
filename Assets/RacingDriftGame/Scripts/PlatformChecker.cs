using UnityEngine;

namespace RacingDriftGame.Scripts
{
    public class PlatformChecker : MonoBehaviour
    {
        public static bool IsAndroidPlatform { get; private set; }
        private void Awake()
        {
            IsAndroidPlatform = Application.platform == RuntimePlatform.Android;

        }

    }
}
