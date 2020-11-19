using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Events;


public class BtnRestart : MonoBehaviour
{

    public GameObject restartMenu; 
    private static int restarts = 0;
    private static string currentTime;
    Log Log = new Log();


   public void Reload(){
        currentTime = Time.time.ToString("f5");
        restarts++;
        Log.Write("Restart", restarts.ToString(), currentTime);
       restartMenu.SetActive(false);
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }

}
