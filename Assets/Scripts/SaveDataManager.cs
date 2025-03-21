using UnityEngine;
using UnityEngine.UI;
using System.IO;
using static UnityEngine.Analytics.IAnalytic;
using System.Xml.Linq;
using System;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;
public class SaveDataManager : MonoBehaviour
{
    public string inputName;
    public InputField inputField;

    public static SaveDataManager Instance;
    public Text bestScoreText;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        inputField.text = LoadName();
        bestScoreText.text = $"Best Score : {LoadName()} : {LoadScore()}";
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    [Serializable]
    class SaveDataPerson
    {
        public string Name;
        public int Score;
    }
    public void SaveName()
    {
        SaveDataPerson data = new SaveDataPerson();

        data.Name = inputName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/saveName.json", json);
    }
    public string LoadName()
    {
        string path = Application.persistentDataPath + "/saveName.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveDataPerson data = JsonUtility.FromJson<SaveDataPerson>(json);
            return data.Name;
        }
        return null;
    }

    public void SaveScore()
    {
        SaveDataPerson data = new SaveDataPerson();

        data.Score = MainManager.Instance.MaxPoints;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/saveScore.json", json);
    }
    public int LoadScore()
    {
        string path = Application.persistentDataPath + "/saveScore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveDataPerson data = JsonUtility.FromJson<SaveDataPerson>(json);
            return data.Score;
        }
        return -1;
    }

    public void PlayGame()
    {
        inputName = inputField.text;
        SceneManager.LoadScene(1);
    }
}
