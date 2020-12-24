using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraBehaviour : MonoBehaviour
{
    public Transform player;
    public GameObject colorCircle, powerUp, restartMenu;
    public Vector3 spawnPosition;
    public int minSpawnDistance, maxSpawnDistance;
    public int minRotationSpeed, maxRotationSpeed;

    private static string currentTime;
    private static bool falling = false; 
    private static int falls = 0;

    public GameObject Player;
    public Log Log;
    public Atividade Atividade;

    void Start()
    {
        Log = Player.GetComponent<Log>();
        Atividade = Player.GetComponent<Atividade>();
        
       falling = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y > transform.position.y)
            transform.position = new Vector3(0, player.position.y, transform.position.z);
        if(player.transform.position.y + transform.position.y < -7 && !falling)
        {

            falls++;

            // *LOG*
            Log.addLog("GO", "Fall", "Jogada");
            
            falling = true;
            Time.timeScale = 0f;
            restartMenu.SetActive(true);

        }

        if(Mathf.Abs(transform.position.y - spawnPosition.y ) < 6)
        {
            if (Random.Range(0, 5) == 0)
            {
                spawnPowerUp();
            }
            else
            {
                spawnCircle(Random.Range(minRotationSpeed, maxRotationSpeed) * 10);
            }
        }
    }

    void spawnCircle(int speed)
    {
        GameObject tempCircle = Instantiate(colorCircle , spawnPosition, Quaternion.identity);
        tempCircle.GetComponent<CircleBehaviour>().rotationSpeed = speed;
        spawnPosition += new Vector3(0,Random.Range(minSpawnDistance,maxSpawnDistance), 0);
    }
    void spawnPowerUp()
    {
        GameObject tempCircle = Instantiate(powerUp, spawnPosition, Quaternion.identity);
        spawnPosition += new Vector3(0, Random.Range(minSpawnDistance, maxSpawnDistance), 0);
    }
}
