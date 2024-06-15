using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalManager : MonoBehaviour
{
    Dictionary<string, Image> journalPhotos = new Dictionary<string, Image>();
    void Start()
    {
        Image[] slotImages = GetComponentsInChildren<Image>();
        foreach (Image slot in slotImages)
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
