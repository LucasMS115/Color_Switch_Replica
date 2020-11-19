using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

using Events;

public class PlayerBehaviour : MonoBehaviour
{
    public int jumpingForce;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public GameObject destroyedPrefab, restartMenu;
    public string currentColor;

    public int wrongColor = 0;
    private static string currentTime;
    private static bool gameOver = false;
    private static bool first = false;
    private int points = 0;
    Log Log = new Log();

    void Start()
    {

        gameOver = false;

        Time.timeScale = 1.0f;
        if(!first){
            first = true;
            Log.Write("EVENT", "VALUE", "TIME");
            currentTime = Time.time.ToString("f5");
            Log.Write("Start", "0", currentTime);
        }
     

        int index = Random.Range(0, 4);
        currentColor = setColor(index);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) && !gameOver)
        {
            currentTime = Time.time.ToString("f5");
            Log.Write("cmd", "tap", currentTime);
            rb.velocity = Vector2.up * jumpingForce;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PowerUp" && !gameOver)
        {
            currentColor = setColor(other.GetComponent<PowerUpBehaviour>().colorIndex);
            currentTime = Time.time.ToString("f5");
            Log.Write("PU", currentColor, currentTime);
            Destroy(other.gameObject);
        }
        else
        {
            if (other.gameObject.tag != currentColor)
            {
                StartCoroutine(cameraJig());
                spriteRenderer.enabled = false;
            }else{
                points++;
                currentTime = Time.time.ToString("f5");
                Log.Write("RC", points.ToString(), currentTime);
            }
        }
    }

    IEnumerator cameraJig()
    {
        if(!gameOver){
            currentTime = Time.time.ToString("f5");
            wrongColor++;
            Log.Write("WC", wrongColor.ToString(), currentTime);
            gameOver = true;
        }
        

        Camera.main.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(.8f);
        Time.timeScale = 0f;
        restartMenu.SetActive(true);
    }

    string setColor(int index)
    {

        switch (index)
        {
            case 0:
                spriteRenderer.color = Color.cyan;
                return "blue";
            case 1:
                spriteRenderer.color = Color.yellow;
                return "yellow";
            case 2:
                spriteRenderer.color = new Color(0.5f, 0, 1, 1);
                return "purple";
            case 3:
                spriteRenderer.color = new Color(1, 0.1f, 0.5f, 1);
                return "pink";
        }

        return "blue";
    }
}
