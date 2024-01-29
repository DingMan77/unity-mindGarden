using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : Singleton<UIController>
{
    public TMP_Text score;
    public static int num;
    public void AddScore()
    {
        num += 1;
        score.text = num.ToString();
    }
}
