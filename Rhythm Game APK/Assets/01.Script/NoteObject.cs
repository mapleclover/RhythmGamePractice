using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;

    public KeyCode keyToPress;

    private BoxCollider2D box;
    
    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;

    // Start is called before the first frame update
    void Start()
    {
        box = this.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localScale = new Vector3(0.155f, 0.06f/GameManager.instance.speedMultiplier, 1);
        box.size = new Vector2(5.0f, 15.0f * GameManager.instance.speedMultiplier);
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                gameObject.SetActive(false);  

                if(Mathf.Abs(transform.position.y) > (0.5f * GameManager.instance.speedMultiplier))
                {
                    Debug.Log("Hit");
                    GameManager.instance.NormalHit();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                }              
                else if(Mathf.Abs(transform.position.y) > (0.25f * GameManager.instance.speedMultiplier))
                {
                    Debug.Log("Good");
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, transform.position, hitEffect.transform.rotation);
                }
                else
                {
                    Debug.Log("Perfect");
                    GameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, transform.position, hitEffect.transform.rotation);
                }
            }
        }

        if (transform.position.y < (-0.5f * GameManager.instance.speedMultiplier))
        {
            canBePressed = false;

            GameManager.instance.NoteMissed();
            
            Instantiate(missEffect, transform.position, hitEffect.transform.rotation);

            gameObject.SetActive(false);

            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            canBePressed = true;
        }
    }
}