using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{
    public int colorIndex;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
/*         Debug.Log("PowerUp - Start()");
 */        colorIndex = Random.Range(0, 4);
        setColor(colorIndex);
    }

    void setColor(int index)
    {
/*         Debug.Log("PowerUp - setColor()");
 */        switch (index)
        {
            case 0:
                spriteRenderer.color = Color.cyan;
                break;
            case 1:
                spriteRenderer.color = Color.yellow;
                break;
            case 2:
                spriteRenderer.color =  new Color(129, 0, 203, 1);
                break;
            case 3:
                spriteRenderer.color = Color.magenta;
                break;
        }
    }
}
