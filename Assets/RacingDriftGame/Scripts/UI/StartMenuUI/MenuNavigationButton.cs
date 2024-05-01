using UnityEngine;

namespace RacingDriftGame.Scripts.UI.StartMenuUI
{
    public class MenuNavigationButton : MonoBehaviour
    {
        [SerializeField] private TypeOfPanel typeOfPanel;
        private MenuUI menuUI;
        
        public void SetMainUI(MenuUI _menuUI)
        {
            menuUI = _menuUI;
        }

        public void OpenActualPanel() // OnClick Event
        {
            menuUI.OpenNewPanel(typeOfPanel);
        } 
    
    }
}
