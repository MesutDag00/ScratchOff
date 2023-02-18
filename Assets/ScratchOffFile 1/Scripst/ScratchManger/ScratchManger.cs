using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class ScratchManger : MonoBehaviour
{
    public static ScratchManger Instance;
    public static int Counter;
    public Sprite NormalImage;
    public Text CardCountText;

    public GameObject BuyButton;
    public GameObject FreeButton;

    // public Transform AnimasyonPosition;
    // public GameObject ScratchOffPanel;
    // public GameObject CardSelectButton;
    // public GameObject CartPanel;

    private void Awake()
    {
        Instance = this;
        PlayerPrefs.SetInt("Claim", 3);
    }

    public void GamePlay()
    {
        Scratch.Instance.RedPanel.GetComponent<Animator>().enabled = true;
        Scratch.Instance.AssignPosts();
        Scratch.Instance.LosePanel.SetActive(false);
        Scratch.Instance.WinPanel.SetActive(false);
        Scratch.Instance.AnimatorTop.SetBool("isActive", false);
    }

    public void GamePlayPaid()
    {
        //�deme i�in gerkeli �eyler buraya ya�zl�cak veyahut �ekilicek
        GamePlay();
    }

    public void ScratchOpenFree()
    {
        if (PlayerPrefs.GetInt("Claim") > 0)
        {
            Scratch.Instance.multiValue = 1;
            PlayerPrefs.SetInt("Claim", PlayerPrefs.GetInt("Claim") - 1);
            CardCountText.text = PlayerPrefs.GetInt("Claim").ToString();
            Invoke(nameof(ScratchOpenWaiting), 0.1f);
        }
        else
        {
            Debug.Log("ZAMAN AŞAMASINDA");
        }
    }

    public void ScratchOpenBuy()
    {
        Scratch.Instance.multiValue = 2;
        Invoke(nameof(ScratchOpenWaiting), 0.1f);
    }

    private void ScratchOpenWaiting()
    {
        SelectManger.GameActive = true;
        Scratch.Instance.AssignPosts();
    }

    public void ComebackMenu()
    {
        for (int i = 0; i < Scratch.Instance.Earnings.Count; i++)
        {
            Scratch.Instance.Earnings[i].DeletedCheck.BetText.color = Color.black;
            if (i == 5)
                continue;
            Scratch.Instance.Earnings[i].DeletedCheck.BetText.transform.GetChild(0).GetComponent<Image>().sprite =
                NormalImage;
        }

        Scratch.Instance.RedPanel.GetComponent<Animator>().enabled = false;
        Scratch.Instance.DeleteMaskObje();
        FreeButton.SetActive(true);
        BuyButton.SetActive(true);
        // Scratch.Instance.ScratchOffPanel.SetActive(false);
    }

    public void ComeBackMenuWaiting() => Invoke(nameof(ComebackMenu), 3f);

    public void GrabButton()
    {
        Debug.Log("you received: " + Scratch.Instance.TotalEarnig);
        Scratch.Instance.TotalEarnig = 0;
        Scratch.Instance.TotalAmountofEarningsText.text = Scratch.Instance.TotalEarnig.ToString();
    }
}

static class SelectManger
{
    public static GameObject SelectObje;

    public static int SelectNumber;

    public static bool GameActive;
}