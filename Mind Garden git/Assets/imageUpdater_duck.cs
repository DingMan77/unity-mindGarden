using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class imageUpdater_duck : MonoBehaviour
{
    public static imageUpdater_duck Instance;
    public Image panelImage; // Assign this in the inspector

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void UpdatePanelImage(Sprite newImage)
    {
        panelImage.sprite = newImage;
    }
}
