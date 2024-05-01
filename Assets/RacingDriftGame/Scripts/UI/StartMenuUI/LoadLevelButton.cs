using RacingDriftGame.Scripts.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace RacingDriftGame.Scripts.UI.StartMenuUI
{
    public class LoadLevelButton : MonoBehaviour
    {
        public void LoadNarrowFieldLevel() // OnClick Event
        {
            SceneManager.LoadScene(SceneNames.NarrowFieldRoad);
        }
        public void LoadNarrowForestLevel() // OnClick Event
        {
            SceneManager.LoadScene(SceneNames.NarrowForestRoad);
        }
        public void LoadWideForestLevel() // OnClick Event
        {
            SceneManager.LoadScene(SceneNames.WideForestRoad);
        }
    }
}
