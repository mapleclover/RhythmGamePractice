using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    public GameObject Title;
    public GameObject Screen;

    // Start is called before the first frame update
    void Start()
    {
        Title.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Screen.transform.position.y > 300.0f)
        {
            Screen.transform.Translate(0, -0.5f, 0);
        }
        else 
        {
            Title.SetActive(true);
        }
    }
}