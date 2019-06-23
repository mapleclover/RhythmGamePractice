using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public AudioSource theMusic;
    public VideoPlayer theBGA;

    public bool startPlaying;

    public BeatScroller theBS;

    public static GameManager instance;

    public int HP = 100;
    public int maxHP = 100;
    public int damage = 5;
    public int[] heal = { 0, 1, 3 };

    public int currentScore;
    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;

    public int combo;
    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;

    public float speedMultiplier = 1;

    public Text scoreText;
    public Text multiText;
    public Text[] speed;

    public float totalNotes;
    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public float missedHits;

    public GameObject resultsScreen;
    public GameObject failScreen;
    public GameObject option;
    public Text percentHitText, normalsText, goodsText, perfectsText, missesText, rankText, finalScoreText, comboText;

    public Image hpbar;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        scoreText.text = "Score: 0";
        currentMultiplier = 1;
        combo = 0;
        totalNotes = FindObjectsOfType<NoteObject>().Length;
    }

    // Update is called once per frame
    void Update()
    {
        hpbar.fillAmount = HP / 100.0f;
        if (HP > maxHP)
        {
            HP = 100;
        }
        if (!startPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                if (speedMultiplier < 4.0f)
                {
                    speedMultiplier += .25f;
                    speed[1].text = "Speed: x" + speedMultiplier.ToString("F2");
                    speed[0].text = "Speed: x" + speedMultiplier.ToString("F2");
                }
            }
            if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                if (speedMultiplier > 1.0f)
                {
                    speedMultiplier -= .25f;
                    speed[0].text = "Speed: x" + speedMultiplier.ToString("F2");
                    speed[1].text = "Speed: x" + speedMultiplier.ToString("F2");
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                option.SetActive(false);
                startPlaying = true;
                theBS.hasStarted = true;
                theMusic.Play();
                theBGA.Play();
            }
        }
        else if (startPlaying)
        {
            ShowCombo();
            
            if (HP < 0)
            {
                theMusic.Stop();
                theBGA.Stop();
                failScreen.SetActive(true);
            }

            else if (!theMusic.isPlaying && !resultsScreen.activeInHierarchy)
            {
                resultsScreen.SetActive(true);
                theBGA.Stop();
                normalsText.text = "" + normalHits;
                goodsText.text = goodHits.ToString();
                perfectsText.text = perfectHits.ToString();
                missesText.text = "" + missedHits;

                float percentHit = (100.0f * perfectHits + 70.0f * goodHits + 30.0f * normalHits) / totalNotes;

                percentHitText.text = percentHit.ToString("F1") + "%";

                string rankVal = "F";

                if (percentHit == 100.0f)
                {
                    rankVal = "P";
                }
                else if (percentHit >= 95.0f)
                {
                    rankVal = "S";
                }
                else if (percentHit >= 90.0f)
                {
                    rankVal = "A";
                }
                else if (percentHit >= 80.0f)
                {
                    rankVal = "B";
                }
                else if (percentHit >= 70.0f)
                {
                    rankVal = "C";
                }

                rankText.text = rankVal;

                finalScoreText.text = currentScore.ToString();
            }
        }      
    }

    public void NoteHit()
    {
        Debug.Log("Hit On Time");

        if(currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;
            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                currentMultiplier++;
            }
        }
        combo++;

        multiText.text = "Multiplier: x" + currentMultiplier;

        //currentScore += scorePerNote * currentMultiplier;
        scoreText.text = "Score: " + currentScore;
    }

    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();
        HP += heal[0];
        normalHits++;
    }
    
    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();
        HP += heal[1];
        goodHits++;
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();
        HP += heal[2];
        perfectHits++;
    }

    public void NoteMissed()
    {
        Debug.Log("Missed Note");

        HP -= damage;
        currentMultiplier = 1;
        multiplierTracker = 0;
        combo = 0;

        multiText.text = "Multiplier: x" + currentMultiplier;

        missedHits++;
    }

    public void ShowCombo()
    {
        switch (currentMultiplier)
        {
            case 1:
                comboText.color = new Color(0.5f, 0.5f, 0.5f);
                break;
            case 2:
                comboText.color = new Color(0.83f, 0.5f, 1f);
                break;
            case 3:
                comboText.color = new Color(0.65f, 1f, 0.5f);
                break;
            case 4:
                comboText.color = new Color(0.95f, 1f, 0.3f);
                break;
        }
            

        if (combo >= 1)
        {
            comboText.text = "Combo " + combo;
        }
        else if (combo == 0)
        {
            comboText.text = "";
        }
    }
}
