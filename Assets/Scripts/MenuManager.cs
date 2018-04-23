using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public EventSystem eventSystem;
    public MenuButton[] buttons;
    Mode currentmode;
    string levelname;

    MenuButton GetMenuButton(string name)
    {
        for (int i = 0; i < buttons.Length; ++i)
            if (buttons[i].button.name == name)
                return buttons[i];
        return buttons[0];
    }

    void LaunchLevel(Mode mode)
    {
        switch(mode)
        {
            case Mode.Level:
                SceneManager.LoadScene(levelname);
                break;
            case Mode.Exit:
                Application.Quit();
                break;
            
            default:
                break;
        }
    }

    public void ActivateMode()
    {
        if (eventSystem.currentSelectedGameObject)
        {
            MenuButton btn = GetMenuButton(eventSystem.currentSelectedGameObject.name);
            currentmode = btn.to;
            levelname = btn.button.name;
            if (currentmode == Mode.Level || currentmode == Mode.Exit)
                LaunchLevel(currentmode);
        }
        if (currentmode == Mode.Exit)
            return;
        for (int i = 0; i < buttons.Length; ++i)
        {
            if (buttons[i].buttonMode == Mode.Exit)
            {
                if (currentmode == Mode.MainMenu)
                    buttons[i].button.gameObject.SetActive(true);
                else
                    buttons[i].button.gameObject.SetActive(false);
            }
            if (buttons[i].buttonMode == Mode.ToMainMenu)
            {
                if (currentmode == Mode.MainMenu)
                    buttons[i].button.gameObject.SetActive(false);
                else
                    buttons[i].button.gameObject.SetActive(true);
            } 
            if (buttons[i].buttonMode != Mode.ToMainMenu && buttons[i].buttonMode != Mode.Exit)
            {
                if (buttons[i].buttonMode == currentmode)
                    buttons[i].button.gameObject.SetActive(true);
                else
                    buttons[i].button.gameObject.SetActive(false);
            }
        }
    }

    // Use this for initialization
    void Start () {
        currentmode = Mode.MainMenu;
	}

    

    [System.Serializable]
    public enum Mode
    {
        OnePlayer,
        TwoPlayers,
        MainMenu,
        ToMainMenu,
        Level,
        Exit
    }

    [System.Serializable]
    public struct MenuButton
    {
        public Mode buttonMode;
        public Mode to;
        public Button button;
    }
}
