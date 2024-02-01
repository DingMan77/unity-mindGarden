using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorButtonHandler_duck : MonoBehaviour
{
    public Sprite imageForPeace_complex;
    public Sprite imageForEnergetic_complex;
    public Sprite imageForDark_complex;
    public Sprite imageForPeace_middle;
    public Sprite imageForEnergetic_middle;
    public Sprite imageForDark_middle;
    public Sprite imageForPeace_easy;
    public Sprite imageForEnergetic_easy;
    public Sprite imageForDark_easy;

    public void OnPeaceClicked()
    {
        //Debug.Log($"ImageUpdater_duck Instance: {ImageUpdater_duck.Instance}");
        ImageUpdater_duck.SelectedImage = imageForPeace_complex;
        SceneManager.LoadScene("SampleScene");

    }

    public void OnEngergeticClicked()
    {
        //ImageUpdater_duck.Instance.UpdatePanelImage(imageForEnergetic_complex);
        ImageUpdater_duck.SelectedImage = imageForEnergetic_complex;
        SceneManager.LoadScene("SampleScene");
    }

    public void OnDarkClicked()
    {
        //ImageUpdater_duck.Instance.UpdatePanelImage(imageForDark_complex);
        ImageUpdater_duck.SelectedImage = imageForDark_complex;
        SceneManager.LoadScene("SampleScene");
    }
}
