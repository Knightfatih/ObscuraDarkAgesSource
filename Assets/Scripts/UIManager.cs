using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public AudioSource audioSource { get; set; }
    [SerializeField] Text scoreText;
    [SerializeField] Text FinalScoreText;
    [SerializeField] GameObject healthBar;
    [SerializeField] GameObject waveBar;
    [SerializeField] GameObject deathPanel;
    public GameObject wavePanel;
    public GameObject leftWarning;
    public GameObject rightWarning;
    public float targetEnemiesInWave;
    public float remainingEnemiesInWave;

    public bool gameIsPaused = false;

    public GameObject PauseUI;

    static public bool deathPanelBool = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void UpdateScoreText(int score)
    {
        scoreText.text = $"Score: {GameManager.gm.score}";
        FinalScoreText.text = $"Score: {GameManager.gm.score}";
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    void Resume()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public bool SendWarning(string direction)
    {
        switch (direction)
        {
            case "left":
                if (leftWarning.activeInHierarchy == false)
                {
                    StartCoroutine(Warning(direction));
                    return true;
                }
                else
                {
                    return false;
                }
                
            case "right":
                if (rightWarning.activeInHierarchy == false)
                {
                    StartCoroutine(Warning(direction));
                    return true;
                }
                else
                {
                    return false;
                }
                
            default:
                return false;
                
        }

    }
    public IEnumerator Warning(string direction)
    {
        switch (direction)
        {
            case "left":
                    leftWarning.SetActive(true);
                ReferenceManager.RM.clipManager.PlayClip(audioSource, ReferenceManager.RM.clipManager.fanfareOne);
                //play trumpet
                yield return new WaitForSeconds(2);
                    leftWarning.SetActive(false);
                break;

            case "right":
              
                    rightWarning.SetActive(true);
                ReferenceManager.RM.clipManager.PlayClip(audioSource, ReferenceManager.RM.clipManager.fanfareOne);
                //playtrumpet
                yield return new WaitForSeconds(2);
                    rightWarning.SetActive(false);
                break;
        }
    }
    public void SetHealthBarPercentage(float percentage)
    {
        healthBar.GetComponent<Image>().fillAmount = percentage / 100;
    }
    public void SetWaveBarPercentage(float enemiesRemaining)
    {
        remainingEnemiesInWave = enemiesRemaining;
        float result = enemiesRemaining / targetEnemiesInWave;
        waveBar.GetComponent<Image>().fillAmount = result;
     
    }
    public void DisplayDeath()
    {
        deathPanel.SetActive(true);
        deathPanelBool = true;
        rightWarning.SetActive(false);
        leftWarning.SetActive(false);
        if (deathPanelBool)
        {
            StartCoroutine(WaitLoadScene());
        }
    }

    public IEnumerator WaitLoadScene()
    {
        yield return new WaitForSeconds(5);
        deathPanelBool = false;
        SceneManager.LoadScene("Main Menu");
    }

}
