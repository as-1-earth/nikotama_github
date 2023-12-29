using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;/*
using PlayFab;
using PlayFab.ClientModels;*/
using Unity.Collections;

public class TitleUIController : MonoBehaviour
{
    /*name‚ª“ü—Í‚³‚ê‚½Text*/
    public InputField userName;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("userName") != "")
        {
            userName.text = PlayerPrefs.GetString("userName");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeName()
    {
        Debug.Log(userName.text);
        PlayerPrefs.SetString("userName", userName.text);
    }

    public void OnEasyButton()
    {
        PlayerPrefs.SetString("Level", "easy");
        PlayerPrefs.SetInt("SpeedZ", 15);
        ChangeToMain();
    }
    public void OnNormalButton()
    {
        PlayerPrefs.SetString("Level", "normal");
        PlayerPrefs.SetInt("SpeedZ", 30);
        ChangeToMain();
    }
    public void OnHardButton()
    {
        PlayerPrefs.SetString("Level", "hard");
        PlayerPrefs.SetInt("SpeedZ", 45);
        ChangeToMain();
    }
    public void OnEXButton()
    {
        PlayerPrefs.SetString("Level", "ex");
        PlayerPrefs.SetInt("SpeedZ", 60);
        ChangeToMain();
    }

    public static void ChangeToMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnRankingButton()
    {
        SceneManager.LoadScene("CollectRanking");
    }



    public void OnShowEasyButton()
    {
        PlayerPrefs.SetString("Level", "easy");
        SceneManager.LoadScene("ShowRanking");
    }
    public void OnShowNormalButton()
    {
        PlayerPrefs.SetString("Level", "normal");
        SceneManager.LoadScene("ShowRanking");
    }
    public void OnShowHardButton()
    {
        PlayerPrefs.SetString("Level", "hard");
        SceneManager.LoadScene("ShowRanking");
    }
    public void OnShowEXButton()
    {
        PlayerPrefs.SetString("Level", "ex");
        SceneManager.LoadScene("ShowRanking");
    }
}
