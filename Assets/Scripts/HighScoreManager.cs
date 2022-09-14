using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager Instance;

    public string playerName;

    public string[] top5Players = new string[5];
    public int[] top5PlayerScores = new int[5];

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        for (int i = 0; i < top5Players.Length; i++)
        {
            if (top5Players[i] == null)
            {
                top5Players[i] = " ";
            }
        }

        LoadScores();
    }

    private void Start()
    {
        for (int i = 0; i < top5Players.Length; i++)
        {
            if (top5Players[i] == null)
            {
                top5Players[i] = " ";
            }
        }
    }

    public void UpdateScore(int scoreToRegister)
    {
        if (scoreToRegister >= top5PlayerScores[4])
        {
            if (scoreToRegister >= top5PlayerScores[3])
            {
                if (scoreToRegister >= top5PlayerScores[2])
                {
                    if (scoreToRegister >= top5PlayerScores[1])
                    {
                        if (scoreToRegister >= top5PlayerScores[0])
                        {
                            top5Players[4] = top5Players[3];
                            top5PlayerScores[4] = top5PlayerScores[3];

                            top5Players[3] = top5Players[2];
                            top5PlayerScores[3] = top5PlayerScores[2];

                            top5Players[2] = top5Players[1];
                            top5PlayerScores[2] = top5PlayerScores[1];

                            top5Players[1] = top5Players[0];
                            top5PlayerScores[1] = top5PlayerScores[0];

                            top5Players[0] = playerName;
                            top5PlayerScores[0] = scoreToRegister;
                        }
                        else
                        {
                            top5Players[4] = top5Players[3];
                            top5PlayerScores[4] = top5PlayerScores[3];

                            top5Players[3] = top5Players[2];
                            top5PlayerScores[3] = top5PlayerScores[2];

                            top5Players[2] = top5Players[1];
                            top5PlayerScores[2] = top5PlayerScores[1];

                            top5Players[1] = playerName;
                            top5PlayerScores[1] = scoreToRegister;
                        }
                    }
                    else
                    {
                        top5Players[4] = top5Players[3];
                        top5PlayerScores[4] = top5PlayerScores[3];

                        top5Players[3] = top5Players[2];
                        top5PlayerScores[3] = top5PlayerScores[2];

                        top5Players[2] = playerName;
                        top5PlayerScores[2] = scoreToRegister;
                    }
                }
                else
                {
                    top5Players[4] = top5Players[3];
                    top5PlayerScores[4] = top5PlayerScores[3];

                    top5Players[3] = playerName;
                    top5PlayerScores[3] = scoreToRegister;
                }
            }
            else
            {
                top5Players[4] = playerName;
                top5PlayerScores[4] = scoreToRegister;
            }
        }
    }

    [System.Serializable]
    class SaveData
    {
        public string[] Top5Players;
        public int[] Top5PlayerScores;
    }

    public void SaveScores()
    {
        SaveData data = new SaveData();
        data.Top5Players = top5Players;
        data.Top5PlayerScores = top5PlayerScores;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScores()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            top5Players = data.Top5Players;
            top5PlayerScores = data.Top5PlayerScores;
        }
    }

    private void OnApplicationQuit()
    {
        SaveScores();
    }
}
