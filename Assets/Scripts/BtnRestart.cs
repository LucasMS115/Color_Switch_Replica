using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnRestart : MonoBehaviour
{

    public GameObject restartMenu; 
    private static int restarts = 0;
    private static string currentTime;

    public GameObject Player;
    public Log Log;
    public Atividade Atividade;

    public void Start(){
        Log = Player.GetComponent<Log>();
        Atividade = Player.GetComponent<Atividade>();
        Debug.Log("Btn => " + Atividade.atividadeCodigo);
    }

   public void Reload(){
        restarts++;

        // *LOG*
        Log.addLog("Restart", restarts.ToString(), "Jogada");
        
       restartMenu.SetActive(false);
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }

   public void End(){
        restarts++;
        Debug.Log("End()");

        // *LOG*
        Log.addLog("Exit", restarts.ToString(), "Jogada");
        Atividade.encerrarAtividade();
        
   }

}
