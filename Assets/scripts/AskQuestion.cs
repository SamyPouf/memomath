﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AskQuestion : MonoBehaviour {

    public Canvas joystick;

    //public int QuestionNbType;
    private bool isScoreHigherThen40;

    public GameObject GQuestion;
    public bool isAnsweringQuestion;

    AskQuestionType1 askQuestionType1;
    AskQuestionType2 askQuestionType2;
    AskQuestionType3 askQuestionType3;
    AskQuestionType4 askQuestionType4;
    AskQuestionType5 askQuestionType5;

    public TextMeshProUGUI TexteScore;
    public int Score;

    public float timer;

    private int NbQuestion;
    public TextMeshProUGUI NbDeQuestion;

    public Canvas QuestionCanvas;

    public TextMeshProUGUI TextBravo;
    public TextMeshProUGUI TextOups;

    private bool cooldown;

    public GameObject CameraJeu;
    CameraMouvement VitesseScript;

    GameObject GameData;
    GameData DataScript;

    // Use this for initialization
    void Start () {
        GameData = GameObject.Find("Game Data");
        DataScript = GameData.GetComponent<GameData>();
        isAnsweringQuestion = false;
        askQuestionType1 = gameObject.GetComponent<AskQuestionType1>();
        askQuestionType2 = gameObject.GetComponent<AskQuestionType2>();
        askQuestionType3 = gameObject.GetComponent<AskQuestionType3>();
        askQuestionType4 = gameObject.GetComponent<AskQuestionType4>();
        askQuestionType5 = gameObject.GetComponent<AskQuestionType5>();
        TexteScore.text = "Score: 0";
        DisableMessageText();

        QuestionCanvas.enabled = false;
        NbQuestion = 1;
        TexteScore.enabled = true;
    }

    private void Update()
    {
        if(DataScript.CameraSpeed > 40)
        {
            isScoreHigherThen40 = true;
        }
        if (cooldown == true)
        {
            timer -= Time.fixedDeltaTime;
            if (timer <= 0)
            {
                DisableMessageText();
                timer = 1;
            }
        }


        if(isAnsweringQuestion){
            joystick.enabled = false;
        }else {
            joystick.enabled = true;
        }
    }

    public void Ask_Question (int QuestionNumber)
    {
        GQuestion.SetActive(true);
        if (QuestionNumber == 1) { askQuestionType1.Ask_Question(); }
        if (QuestionNumber == 2) { askQuestionType2.Ask_Question(); }
        if (QuestionNumber == 3) { askQuestionType3.Ask_Question(); }
        if (QuestionNumber == 4) { askQuestionType4.Ask_Question(); }
        if (QuestionNumber == 5) { askQuestionType5.Ask_Question(); }

        NbDeQuestion.text = "Question #" + NbQuestion;
        NbQuestion++;
        

    }
    public void isGoodAnswer()
    {
        Score++;
        if (Score > PlayerPrefs.GetInt(DataScript.PlayerPrefs)) { PlayerPrefs.SetInt(DataScript.PlayerPrefs, Score); }
        TexteScore.text = "Score: " + Score;
        QuestionCanvas.enabled = false;
        if(isScoreHigherThen40 == false)
        {
            DataScript.CameraSpeed++;
        }


        TextBravo.enabled = true;
        TextOups.enabled = false;
        cooldown = true;
        isAnsweringQuestion = false;

    }
    public void isBadAnswer()
    {
        QuestionCanvas.enabled = false;

        TextOups.enabled = true;
        TextBravo.enabled = false;
        cooldown = true;
        isAnsweringQuestion = false;
    }

    public void DisableMessageText()
    {
        TextBravo.enabled = false;
        TextOups.enabled = false;
        cooldown = false;
    }
}
