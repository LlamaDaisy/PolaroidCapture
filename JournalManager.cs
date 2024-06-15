using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalManager : MonoBehaviour
{
    [SerializeField] Image[] ImagesCaptured;


    Dictionary<string, Image> journalPhotos = new Dictionary<string, Image>();
    void Start()
    {
        foreach (Image slot in ImagesCaptured)
        {
            string tag = slot.gameObject.tag;
            if (!journalPhotos.ContainsKey(tag))
            {
                journalPhotos.Add(tag, slot);
            }
        }
    }

    // Method to update the journal with the photo
    public void UpdateJournal(string tag, Sprite photoSprite)
    {
        if (journalPhotos.ContainsKey(tag))
        {
            journalPhotos[tag].sprite = photoSprite;
        }
        else
        {
            Debug.LogWarning("Tag not found in journal slots: " + tag);
        }
    }
}
