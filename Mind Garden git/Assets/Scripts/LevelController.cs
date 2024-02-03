using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelController : Singleton<LevelController>
{
    public List<GameObject> levelList;
    public List<GameObject> popupList;

    public int LevelIndex;
    // private Timer timer; // Create an instance of Timer
    // private void Start()
    // {
    //     // Assuming there is a Timer component attached to the same GameObject as LevelController
    //     timer = GetComponent<Timer>();
    // }
    public void NextLevel(){
        //添加if条件 score - 5 = 0 或者 score - 8 = 0 button才会enabled
        // if(UIController.num - 5 == 0){
            // if(LevelIndex < levelList.Count - 1){
                // Debug.Log("Next Level");
                //关闭全部关卡
                foreach(var o in levelList){
                    o.SetActive(false);
                }
                Debug.Log(popupList);
                popupList[LevelIndex].SetActive(false);
                //显示指定关卡
                LevelIndex += 1;
                levelList[LevelIndex].SetActive(true);
                
                // Reset the timer fill amount
                // if (timer != null && timer.uiFill != null)
                // {
                //     Debug.Log("Enter Timer");
                //     timer.RemainingDuration = timer.Duration;
                //     timer.uiFill.fillAmount = Mathf.InverseLerp(0, timer.Duration, timer.Duration);
                // }

            // }else{
            //     Debug.Log("No more level");
            // }
        // }else{
        //     Debug.Log("You need to spot all differences before enter next level");
        // }
        
    }
    public void LoadScene()
    {
        // var countdownTimer = TimerManager.Instance.GetComponent<CountdownTimer>();
        // if(sceneName != "SampleScene"){
        //     countdownTimer.PauseTimer();
        // }else{
        //     PlayerPrefs.SetFloat("TimeRemaining", countdownTimer.timeRemaining);
        //     countdownTimer.ResumeTimer();
        // }
        UIController.num = 0;
        SceneManager.LoadScene("SampleScene");
    }
}
