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
using UnityEngine.Serialization;

public class TitleUIController : MonoBehaviour
{
    /*nameが入力されたText*/
    public InputField userName;
    public GameObject lockText;
    public GameObject lockNormal;
    public GameObject lockHard;
    public GameObject lockEX;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("userName") != "")
        {
            userName.text = PlayerPrefs.GetString("userName");
        }
        
        lockText.SetActive(false);

        if (PlayerPrefs.GetInt("EASY") >= 1000)
        {
            lockNormal.SetActive(false);
        }

        if (PlayerPrefs.GetInt("NORMAL") >= 3000)
        {
            lockHard.SetActive(false);
        }
        
        if (PlayerPrefs.GetInt("HARD") >= 5000)
        {
            lockEX.SetActive(false);
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
        PlayerPrefs.SetString("Level", "EASY");
        PlayerPrefs.SetInt("SpeedZ", 20);
        ChangeToMain();
    }
    public void OnNormalButton()
    {
        PlayerPrefs.SetString("Level", "NORMAL");
        PlayerPrefs.SetInt("SpeedZ", 30);
        ChangeToMain();
    }

    public void OnNormalErrorButton()
    {
        lockText.SetActive(true);

        lockText.gameObject.GetComponent<Text>().text = "EASYで1000mを超えないと解放されません";
    }
    
    public void OnHardButton()
    {
        PlayerPrefs.SetString("Level", "HARD");
        PlayerPrefs.SetInt("SpeedZ", 45);
        ChangeToMain();
    }
    
    public void OnHardErrorButton()
    {
        lockText.SetActive(true);

        lockText.gameObject.GetComponent<Text>().text = "NORMALで3000mを超えないと解放されません";
        
    }
    
    public void OnEXButton()
    {
        PlayerPrefs.SetString("Level", "EX");
        PlayerPrefs.SetInt("SpeedZ", 60);
        ChangeToMain();
    }
    
    public void OnEXErrorButton()
    {
        lockText.SetActive(true);

        lockText.gameObject.GetComponent<Text>().text = "HARDで5000mを超えないと解放されません";
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
        PlayerPrefs.SetString("Level", "EASY");
        SceneManager.LoadScene("ShowRanking");
    }
    public void OnShowNormalButton()
    {
        PlayerPrefs.SetString("Level", "NORMAL");
        SceneManager.LoadScene("ShowRanking");
    }
    public void OnShowHardButton()
    {
        PlayerPrefs.SetString("Level", "HARD");
        SceneManager.LoadScene("ShowRanking");
    }
    public void OnShowEXButton()
    {
        PlayerPrefs.SetString("Level", "EX");
        SceneManager.LoadScene("ShowRanking");
    }

    public void OnTapToStart()
    {
        SceneManager.LoadScene("Title");
    }
}
