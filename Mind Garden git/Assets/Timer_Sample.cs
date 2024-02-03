using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Timer_Sample : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;

    // Update is called once per frame
    void Update()
    {
        if(remainingTime > 0){
            remainingTime -= Time.deltaTime;
        }else{
            remainingTime = 30;
  
            Debug.Log("Times out");
        }
       
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        
    }
}