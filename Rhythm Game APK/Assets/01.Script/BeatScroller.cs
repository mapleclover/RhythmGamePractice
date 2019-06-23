using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float beatTempo;

    public bool hasStarted;

    // Start is called before the first frame update
    void Start()
    {
        beatTempo = beatTempo / 60.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted)
        {
            this.transform.localScale = new Vector3(1, GameManager.instance.speedMultiplier, 1);
            if (GameManager.instance.HP >= 0)
            {
                transform.Translate(0, -beatTempo * GameManager.instance.speedMultiplier * Time.deltaTime, 0);
            }
        }
    }
}
