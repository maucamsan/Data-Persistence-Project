using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;
    [SerializeField] Text bScoreText;
    [HideInInspector] public int score;
    [HideInInspector] public string playerName;
    
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadScores();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
    EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }

    [System.Serializable]
    public class BestScore
    {
       public int score;
       public string pName;
    }

    public void SaveScores()
    {
        BestScore bScore = new BestScore();
        bScore.score = score;
        bScore.pName = playerName;
        string json = JsonUtility.ToJson(bScore);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScores()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            BestScore bScore = JsonUtility.FromJson<BestScore>(json);
            bScoreText.text = "Best Score: " + bScore.score.ToString() + "\nBy: " + bScore.pName;

        }
    }
}
