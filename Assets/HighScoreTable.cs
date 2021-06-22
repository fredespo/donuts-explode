using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreTable : MonoBehaviour
{
    public DataStorage dataStorage;
    public GameObject noScoresMsg;
    public GameObject entryPrefab;
    public float spaceBetweenEntries = 66;
    public int capacity = 10;
    private int size = 0;
    public List<Color> colors;

    public void Refresh()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        List<Entry> entries = dataStorage.GetHighScoreEntries();
        size = entries.Count;
        if (entries.Count > 0)
        {
            Add(entries);
            noScoresMsg.SetActive(false);
        }
        else
        {
            noScoresMsg.SetActive(true);
        }
    }

    public void Add(List<Entry> entries)
    {
        float yPos = 0;
        int colorIndex = 0;
        int rank = 1;
        foreach(Entry entry in entries)
        {
            Add(entry, yPos, colors[colorIndex], rank);
            colorIndex = (colorIndex + 1) % colors.Count;
            yPos -= spaceBetweenEntries;
            ++rank;
        }
    }

    private void Add(Entry entry, float yPos, Color color, int rank)
    {
        GameObject entryObj = Instantiate(entryPrefab, transform);
        HighScoreEntry entryInfo = entryObj.GetComponent<HighScoreEntry>();
        entryInfo.SetName(entry.GetName());
        entryInfo.SetScore(entry.GetScore());
        entryInfo.SetColor(color);
        entryInfo.SetY(yPos);
        entryInfo.SetRank(rank);
    }

    public int GetCapacity()
    {
        return this.capacity;
    }

    public int GetSize()
    {
        return this.size;
    }

    public class Entry
    {
        private string name;
        private int score;

        public Entry(string name, int score)
        {
            this.name = name;
            this.score = score;
        }

        public string GetName()
        {
            return this.name;
        }

        public int GetScore()
        {
            return this.score;
        }
    }
}
