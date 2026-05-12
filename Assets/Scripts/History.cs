using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class History : MonoBehaviour
{
    private const string KEY = "PlayHistory_v1";
    private const int MAX_RECORDS = 50;

    [Header("Buttons")]
    public Button openButton;
    public Button closeButton;
    public Button clearButton;

    [Header("Panel")]
    public GameObject panelRoot;
    public GameObject overlay;

    [Header("Scroll")]
    public GameObject itemPrefab;
    public Transform contentParent;

    [Header("Text Setting")]
    public float fontSize = 16f;

    [Serializable]
    public class PlayRecord
    {
        public int coins;
        public float distance;
        public string dateTime;
    }

    [Serializable]
    public class PlayRecordList
    {
        public List<PlayRecord> records = new List<PlayRecord>();
    }

    private PlayRecordList data = new PlayRecordList();

    private void Awake()
    {
        Load();
        panelRoot.SetActive(false);

        openButton.onClick.AddListener(() => {
            panelRoot.SetActive(true);
            overlay.SetActive(true);
            Refresh();
        });
        closeButton.onClick.AddListener(() => {
            panelRoot.SetActive(false);
            overlay.SetActive(false);
        });
        clearButton.onClick.AddListener(() => {
            data.records.Clear();
            Save();
            Refresh();
        });
    }

    public void AddRecord(int coins, float distance)
    {
        data.records.Insert(0, new PlayRecord
        {
            coins = coins,
            distance = distance,
            dateTime = DateTime.Now.ToString("dd/MM/yy HH:mm")
        });
        if (data.records.Count > MAX_RECORDS)
            data.records.RemoveAt(data.records.Count - 1);
        Save();
    }

    private void Refresh()
    {
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        foreach (var record in data.records)
        {
            var go = Instantiate(itemPrefab, contentParent);
            var texts = go.GetComponentsInChildren<TextMeshProUGUI>();
            if (texts.Length >= 3)
            {
                texts[0].text = record.dateTime;
                texts[0].fontSize = fontSize;
                texts[1].text = $"{record.coins} coins";
                texts[1].fontSize = fontSize;
                texts[2].text = $"{record.distance:F1} M";
                texts[2].fontSize = fontSize;
            }
        }
    }

    private void Load()
    {
        string json = PlayerPrefs.GetString(KEY, "");
        data = !string.IsNullOrEmpty(json)
            ? JsonUtility.FromJson<PlayRecordList>(json) ?? new PlayRecordList()
            : new PlayRecordList();
    }

    private void Save()
    {
        PlayerPrefs.SetString(KEY, JsonUtility.ToJson(data));
        PlayerPrefs.Save();
    }
}
