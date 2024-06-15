using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoCapture : MonoBehaviour
{
    [Header("PhotoTaker")]
    [SerializeField] private Image photoDisplayArea;
    [SerializeField] private GameObject photoFrame;
    [SerializeField] private GameObject cameraUI;

    [SerializeField] private JournalManager journalManager;

    [Header("Photo Fader Effect")]
    [SerializeField] private Animator fadingAnim;

    [SerializeField] GameObject player;
    [SerializeField] Camera polaroidCamera;
    Vector3 originOfRay;


    private Texture2D screenCapture;
    private bool viewingPhoto;
    private PlayerMovement playerMovement;

    private void Start()
    {
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        playerMovement = player.GetComponent<PlayerMovement>();

    }

    private void Update()
    {
        if (viewingPhoto && Input.GetMouseButtonDown(0) || viewingPhoto && Input.GetKeyDown(KeyCode.E)) 
        { 
            RemovePhoto();
        }

        else if (playerMovement.polaroidView && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(CapturePhoto());
        }
  
    }

    IEnumerator CapturePhoto()
    {
        cameraUI.SetActive(false);
        viewingPhoto = true;

        yield return new WaitForEndOfFrame();

        Rect reigonToRead = new Rect (0, 0, Screen.width, Screen.height);

        screenCapture.ReadPixels(reigonToRead, 0, 0, false);
        screenCapture.Apply();
        ShowPhoto();
        CapturedObject();
    }

    void ShowPhoto()
    {
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100.0f);
        photoDisplayArea.sprite = photoSprite;

        photoFrame.SetActive(true);
        fadingAnim.Play("PhotoFade");
    }

    void RemovePhoto()
    {

        if(playerMovement.polaroidView) 
        {
            cameraUI.SetActive(true);
        }

        else 
        {
            cameraUI.SetActive(false);
        }
        viewingPhoto = false;
        photoFrame.SetActive(false);
    }

    void CapturedObject()
    {
        RaycastHit hit;
        originOfRay = polaroidCamera.transform.position;

        if(Physics.Raycast(originOfRay, polaroidCamera.transform.forward, out hit)) 
        {
            string tag = hit.transform.tag;
            Debug.Log(tag);

            if (journalManager != null && !string.IsNullOrEmpty(tag))
            {
                journalManager.UpdateJournal(tag, photoDisplayArea.sprite);
                Debug.Log($"Captured photo of: {hit.transform.name} with tag: {tag}");
            }
        }
    } 

}
