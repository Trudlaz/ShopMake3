using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Player player;
    Transform up;
    Transform down;

    private void Awake()
    {
        up = transform.GetChild(0);
        down = transform.GetChild(1);
    }
    public virtual void Interect()
    {
        if (player.transform.position.y > transform.position.y)
        {
            player.transform.position = down.position;
            Debug.Log("down");
        }
        else
        {
            player.transform.position = up.position;
            Debug.Log("up");
            Time.timeScale = 0.1f;
        }
    }
}
