using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.IO.Ports;

public class MenuManager : MonoBehaviour
{
    public enum SelectedButton { Play, HowToPlay, HighScore, Credits, Quit, InMenu }
    public GameObject mainMenu;
    public GameObject aboutMenu;
    public GameObject creditsMenu;
    public GameObject secondCreditsMenu;
    public GameObject thirdCreditsMenu;
    public GameObject highestMenu;

    public GameObject howBtn;
    public GameObject highestBtn;
    public GameObject creditBtn;
    public GameObject quitBtn;
    public GameObject mainBtn;

    public GameObject mainFirstBtn;
    public GameObject howFirstBtn;
    public GameObject creditsFirstBtn;
    public GameObject secondCreditsFirstBtn;
    public GameObject thirdCreditsFirstBtn;
    public GameObject highestFirstBtn;
    public bool canMove = true;
    SerialPort sp = new SerialPort("COM4", 38400);
    public SelectedButton activeButton;
    private void Start()
    {
        sp.Open();
        sp.ReadTimeout = 1;
    }
    private void Update()
    {

        if (sp.IsOpen)
        {
         
            try
            {

                string smt = sp.ReadLine();


                if (int.TryParse(smt, out int number))
                {
                    int value = System.Int32.Parse(smt);
                    
                }
                else
                {
                    switch (smt)
                    {
                        case "ccw":
                            switch (activeButton)
                            {
                                case SelectedButton.Play:
                                    if (canMove)
                                    {
                                        ChangeButton(howBtn);
                                        activeButton = SelectedButton.HowToPlay;
                                    }
                    
                                    break;
                                case SelectedButton.HowToPlay:
                                    if (canMove)
                                    {
                                        ChangeButton(highestBtn);
                                        activeButton = SelectedButton.HighScore;
                                    }
                                    break;
                                case SelectedButton.HighScore:
                                    if (canMove)
                                    {
                                        ChangeButton(quitBtn);
                                        activeButton = SelectedButton.Quit;
                                    }
                                    break;
                                case SelectedButton.Quit:
                                    if (canMove)
                                    {
                                        ChangeButton(creditBtn);
                                        activeButton = SelectedButton.Credits;
                                    }
                                    break;
                                case SelectedButton.Credits:
                                    if (canMove)
                                    {
                                        ChangeButton(mainBtn);
                                        activeButton = SelectedButton.Play;
                                    }
                                    break;
                            }
                            break;
                        case "cw":
                            switch (activeButton)
                            {
                                case SelectedButton.Play:
                                    if (canMove)
                                    {
                                        ChangeButton(creditBtn);
                                        activeButton = SelectedButton.Credits;
                                    }
                                    break;
                                case SelectedButton.HowToPlay:
                                    if (canMove)
                                    {
                                        ChangeButton(mainBtn);
                                        activeButton = SelectedButton.Play;
                                    }
                                    break;
                                case SelectedButton.HighScore:
                                    if (canMove)
                                    {
                                        ChangeButton(howBtn);
                                        activeButton = SelectedButton.HowToPlay;
                                    }
                                    break;
                                case SelectedButton.Quit:
                                    if (canMove)
                                    {
                                        ChangeButton(highestBtn);
                                        activeButton = SelectedButton.HighScore;
                                    }
                                    break;
                                case SelectedButton.Credits:
                                    if (canMove)
                                    {
                                        ChangeButton(quitBtn);
                                        activeButton = SelectedButton.Quit;
                                    }
                                    break;
                            }
                            break;
                        case "S":
                            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
                            break;
                    }
                }

            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }

    private void ChangeButton(GameObject button)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(button);
    }

    public void Play()
    {
        
        Time.timeScale = 1f;
        
        SceneManager.LoadScene("MainGame");
    }
    public void Exit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void MainMenu()
    {
        canMove = true;
        mainMenu.SetActive(true);
        aboutMenu.SetActive(false);
        creditsMenu.SetActive(false);
        secondCreditsMenu.SetActive(false);
        thirdCreditsMenu.SetActive(false);
        highestMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainFirstBtn);
    }

    public void AboutMenu()
    {
        canMove = false;
        mainMenu.SetActive(false);
        aboutMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(howFirstBtn);
    }

    public void CreditsMenu()
    {
        canMove = false;
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(creditsFirstBtn);
    }

    public void SecondCreditsMenu()
    {
        canMove = false;
        creditsMenu.SetActive(false);
        secondCreditsMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(secondCreditsFirstBtn);
    }

    public void ThirdCreditsMenu()
    {
        canMove = false;
        secondCreditsMenu.SetActive(false);
        thirdCreditsMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(thirdCreditsFirstBtn);
    }

    public void HighestMenu()
    {
        canMove = false;
        mainMenu.SetActive(false);
        highestMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(highestFirstBtn);
    }
}

