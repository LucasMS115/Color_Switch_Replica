using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

using Events;

public class PlayerBehaviour : MonoBehaviour
{
    public int jumpingForce;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public GameObject destroyedPrefab;
    public string currentColor;

    private static string currentTime;
    private static bool first = false;
    private static int restarts = 0;
    private int points = 0;
    Log Log = new Log();

    void Start()
    {
        if(!first){
            first = true;
            Log.Write("EVENT", "VALUE", "TIME");
            currentTime = Time.time.ToString("f5");
            Log.Write("Start", restarts.ToString(), currentTime);
        }else{
            currentTime = Time.time.ToString("f5");
            restarts++;
            Log.Write("Restart", restarts.ToString(), currentTime);
        }
     

        int index = Random.Range(0, 4);
        currentColor = setColor(index);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            currentTime = Time.time.ToString("f5");
            Log.Write("cmd", "tap", currentTime);
            rb.velocity = Vector2.up * jumpingForce;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PowerUp")
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
               /*  Debug.Log("PlayerBehaviour - Collider != currentColor"); */
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
        /* Debug.Log("PlayerBehaviour - camerJig()"); */
        currentTime = Time.time.ToString("f5");
        Log.Write("WC", restarts.ToString(), currentTime);

        Camera.main.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(.8f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    string setColor(int index)
    {

        /* Debug.Log("PlayerBehaviour - setColor()"); */

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
                Debug.Log("purple");
                return "purple";
            case 3:
                spriteRenderer.color = new Color(1, 0.1f, 0.5f, 1);
                Debug.Log("pink");
                return "pink";
        }

        return "blue";
    }
}
