using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreTable : MonoBehaviour
{
    public GameObject entryPrefab;
    public float spaceBetweenEntries = 66;
    public List<Color> colors;

    public void Start()
    {
        List<Entry> testEntries = new List<Entry>();
        testEntries.Add(new Entry("Fred", 1000));
        testEntries.Add(new Entry("Daisy", 1250));
        testEntries.Add(new Entry("Chelsea", 1250));
        testEntries.Add(new Entry("Kitty", 450));
        Add(testEntries);
    }

    public void Add(List<Entry> entries)
    {
        entries.Sort(delegate (Entry entry1, Entry entry2)
        {
            if(entry2.GetScore() != entry1.GetScore())
            {
                return entry2.GetScore().CompareTo(entry1.GetScore());
            }
            else
            {
                return entry1.GetName().CompareTo(entry2.GetName());
            }
        });
        float yPos = 0;
        int colorIndex = 0;
        foreach(Entry entry in entries)
        {
            Add(entry, yPos, colors[colorIndex]);
            colorIndex = (colorIndex + 1) % colors.Count;
            yPos -= spaceBetweenEntries;
        }
    }

    private void Add(Entry entry, float yPos, Color color)
    {
        GameObject entryObj = Instantiate(entryPrefab, transform);
        HighScoreEntry entryInfo = entryObj.GetComponent<HighScoreEntry>();
        entryInfo.SetName(entry.GetName());
        entryInfo.SetScore(entry.GetScore());
        entryInfo.SetColor(color);
        entryInfo.SetY(yPos);
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
