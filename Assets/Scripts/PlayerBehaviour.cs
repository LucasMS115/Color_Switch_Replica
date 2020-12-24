using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerBehaviour : MonoBehaviour
{
    public int jumpingForce;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public GameObject destroyedPrefab, restartMenu;
    public string currentColor;
    private float runTimeCounter = 0;
    
    public Text score;
    public Text runTime;

    public int wrongColor = 0;
    private static string currentTime;
    private static bool gameOver = false;
    private static bool first = false;
    private int points = 0;

    public Log Log;
    public Atividade Atividade;


    void Start()
    {
        Log = GetComponent<Log>();
        Atividade = GetComponent<Atividade>();

        gameOver = false;
        score.text = "Score: " + 0;

        Time.timeScale = 1.0f;
        if(!first){

            Atividade.criarAtividade();

            first = true;

            // *LOG*
            Log.addLog("Start", "0", "Jogada");
        }
     

        int index = Random.Range(0, 4);
        currentColor = setColor(index);
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + points;
        score.color = spriteRenderer.color;

        runTimeCounter += Time.deltaTime;
        runTime.text = "  Time: " + runTimeCounter.ToString("f1");

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) && !gameOver)
        {
            Log.addLog("cmd", "tap", "Interacao");
            rb.velocity = Vector2.up * jumpingForce;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PowerUp" && !gameOver)
        {
            points+=2;
            currentColor = setColor(other.GetComponent<PowerUpBehaviour>().colorIndex);
            
            // *LOG*
            Log.addLog("PU", points.ToString(), "Pontuacao");

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

                // *LOG*
                Log.addLog("RC", points.ToString(), "Pontuacao");
            }
        }
    }

    IEnumerator cameraJig()
    {
        if(!gameOver){
            currentTime = Time.time.ToString("f5");
            wrongColor++;

            // *LOG*
            Log.addLog("GO", "WC", "Jogada");
            Log.addLog("RT", runTimeCounter.ToString(), "Jogada");

            gameOver = true;
        }

        Camera.main.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(.8f);
        restartMenu.SetActive(true);
        Time.timeScale = 0f;
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
