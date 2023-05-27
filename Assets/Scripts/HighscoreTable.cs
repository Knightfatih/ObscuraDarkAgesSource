using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class HighscoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    //private List<GameManager> highscoreEntryList;
    private List<Transform> highscoresEntryTransformList;
    string json;
    private void Awake()
    {
        entryContainer = transform.Find("Container");
        entryTemplate = entryContainer.Find("Template");

        entryTemplate.gameObject.SetActive(false);

        /*highscoreEntryList = new List<GameManager>()
        {
            new GameManager{ score = 600, name = "NVN"},
            new GameManager{ score = 700, name = "JQN"},
            new GameManager{ score = 800, name = "FTH"},
            new GameManager{ score = 1000, name = "YRK"},
        };*/

        AddHighscoreEntry(0, "01/01/2022 12:12:12");
        //string jsonString = PlayerPrefs.GetString("HighscoreTableTest");
        
        //var scoredata = JsonUtility.FromJson<Entry>(test);
        Highscores highscores = JsonUtility.FromJson<Highscores>(json);

        // Sort entry list by Score
        highscores.highscoreEntryList.Sort((x, y) => y.score.CompareTo(x.score));

        //Top 10
        if (highscores.highscoreEntryList.Count > 10)
        {
            for (int h = highscores.highscoreEntryList.Count; h > 10; h--)
            {
                highscores.highscoreEntryList.RemoveAt(10);
            }
        }

        highscoresEntryTransformList = new List<Transform>();
        foreach (Entry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoresEntryTransformList);
        }

        /*Highscores highscores = new Highscores { highscoreEntryList = highscoreEntryList };
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTableTest", json);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetString("highscoreTableTest"));*/
    }

    private void CreateHighscoreEntryTransform(Entry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 60f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        entryTransform.Find("PosText").GetComponent<Text>().text = rankString;

        int score = highscoreEntry.score;
        entryTransform.Find("ScoreText").GetComponent<Text>().text = score.ToString();

        string time = highscoreEntry.time;
        entryTransform.Find("NameText").GetComponent<Text>().text = time;

        if(rank == 1)
        {
            ColourLetters(entryTransform, new Color32(212, 175, 55, 255), new Color32(212, 175, 55, 55));
        }
        if (rank == 2)
        {
            ColourLetters(entryTransform, new Color32(169, 169, 169, 255), new Color32(169, 169, 169, 55));
        }
        if (rank == 3)
        {
            ColourLetters(entryTransform, new Color32(205, 127, 50, 255), new Color32(205, 127, 50, 55));
        }

        transformList.Add(entryTransform);
    }

    private static void ColourLetters(Transform entryTransform, Color colorOne, Color colorTwo)
    {
        entryTransform.Find("PosText").GetComponent<Text>().color = colorOne;
        entryTransform.Find("PosText").GetComponent<Outline>().effectColor = colorTwo;
        entryTransform.Find("ScoreText").GetComponent<Text>().color = colorOne;
        entryTransform.Find("ScoreText").GetComponent<Outline>().effectColor = colorTwo;
        entryTransform.Find("NameText").GetComponent<Text>().color = colorOne;
        entryTransform.Find("NameText").GetComponent<Outline>().effectColor = colorTwo;
    }

    public void AddHighscoreEntry(int score, string time)
    {
        // Create HighscoreEntry
        Entry highscoreEntry = new Entry { score = score, time = time};

        // Load saved Highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {
            // There's no stored table, initialize
            highscores = new Highscores()
            {
                highscoreEntryList = new List<Entry>()
            };
        }
        // Add new entry to Highscores
        highscores.highscoreEntryList.Add(highscoreEntry);

        // Save updated Highscores
        string json = JsonUtility.ToJson(highscores);
        var scorefile = Application.persistentDataPath + "/scoredata.json";
        File.WriteAllText(scorefile, jsonString);
        PlayerPrefs.SetString("highscoreTable", json);
        
        PlayerPrefs.Save();
       
        this.json = File.ReadAllText(scorefile);

    }
    private class Highscores
    {
        public List<Entry> highscoreEntryList;
    }

    [System.Serializable]
    private class Entry
    {
        public int score;
        public string time;
    }
    
}
