using System;
using System.Collections;
using System.Collections.Generic;
using ScratchSpace;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Scratch : MonoBehaviour
{
    public int multiValue;
    public int TotalEarnig;

    private int[] BetEarning =
    {
        (int)BetQuantity.one,
        (int)BetQuantity.two,
        (int)BetQuantity.tree,
        (int)BetQuantity.forr,
        (int)BetQuantity.five,
        (int)BetQuantity.six,
    };

    private int[] BetBonusess =
    {
        (int)BetBonus.bonusone,
        (int)BetBonus.bonustwo,
        (int)BetBonus.bonustree,
    };

    List<BetQuantity> BetQuantities = new List<BetQuantity>();
    List<BetBonus> BetBonuses = new List<BetBonus>();

    public static Scratch Instance;
    public List<Earning> Earnings;
    public List<GameObject> MaskObjeDelete = new List<GameObject>();
    public GameObject Mask;
    public GameObject LosePanel;
    public GameObject WinPanel;
    public GameObject RedPanel;
    public GameObject ScratchOffPanel;

    public Transform TargetOpen;
    public Transform TargetClose;

    public Sprite ColorImage;

    public Color WinColor; //93B843
    public Text TotalAmountofEarningsText;
    public Text AddCoinText;

    public Animator AnimatorTop;

    private bool _pressed;

    private void Start()
    {
        AssignPosts();
        Instance = this;
    }

    private void Update() => ScratchMangerController();

    private void ScratchMangerController()
    {
        if (SelectManger.GameActive)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;

            if (_pressed == true)
            {
                GameObject gameObject = Instantiate(Mask, pos, Quaternion.identity);
                MaskObjeDelete.Add(gameObject);
            }
        }

        if (Input.GetMouseButtonDown(0))
            _pressed = true;
        else if (Input.GetMouseButtonUp(0))
            _pressed = false;
    }

    public void DeleteMaskObje()
    {
        foreach (GameObject Mask in MaskObjeDelete)
            Destroy(Mask);
        MaskObjeDelete.Clear();

        foreach (var a in Earnings)
        {
            a.DeletedCheck.Click = false;
            a._first = false;
        }

        RedPanel.SetActive(true);
    }

    public void DeleteCheckController()
    {
        int conclusion = 0;
        List<int> numbers = new List<int>();
        List<int> colorDocuments = new List<int>();
        ScratchManger.Counter = 0;
        if (Earnings.Count(m => m.DeletedCheck.Click == true) >= 8)
        {
            AnimatorTop.SetBool("isActive", true);
            int[] number = new int[]
            {
                BetQuantities.Count(m => m == BetQuantity.one),
                BetQuantities.Count(m => m == BetQuantity.two),
                BetQuantities.Count(m => m == BetQuantity.tree),
                BetQuantities.Count(m => m == BetQuantity.forr),
                BetQuantities.Count(m => m == BetQuantity.five),
                BetQuantities.Count(m => m == BetQuantity.six)
            };


            for (int i = 0; i < number.Length; i++)
                if (number[i] >= 3)
                    numbers.Add(i);
            for (int i = 0; i < numbers.Count; i++)
            {
                conclusion += (BetEarning[numbers[i]] * multiValue);
                colorDocuments.Add(BetEarning[numbers[i]] * multiValue);
            }

            if (conclusion != 0)
            {
                Earnings[5].DeletedCheck.BetText.color = WinColor;
                for (int i = 0; i < colorDocuments.Count; i++)
                for (int j = 0; j < Earnings.Count; j++)
                    if (Earnings[j].DeletedCheck.BetText.text == colorDocuments[i].ToString("N0"))
                    {
                        Earnings[j].DeletedCheck.BetText.color = WinColor;
                        Earnings[j].DeletedCheck.BetText.transform.GetChild(0).GetComponent<Image>().sprite =
                            ColorImage;
                    }
                // Debug.Log($"BetEarnig : {i} Earnigs : {j}  True ");
            }

            if (conclusion <= 0)
                ScratchManger.Instance.ComeBackMenuWaiting();
            else
            {
                //Para burda eklenicek
                ScratchManger.Instance.ComeBackMenuWaiting();
                TotalEarnig += conclusion * ((int)BetBonuses[0] * multiValue);
                TotalAmountofEarningsText.text = TotalEarnig.ToString("n0");
                AddCoinText.text =
                    "+" + (conclusion * ((int)BetBonuses[0]) * multiValue).ToString("n0");
                ColorOpenClose(200);
                AddCoinText.transform.DOMove(TargetOpen.position, 0.5f).SetEase(Ease.InFlash);
                Invoke(nameof(CloseAddCoinAnimation), 1);
            }

            BetBonuses.Clear();
            BetQuantities.Clear();
            SelectManger.GameActive = false;
        }
    }

    private void CloseAddCoinAnimation()
    {
        AddCoinText.transform.DOMove(TargetClose.position, 0.5f).SetEase(Ease.InFlash)
            .OnComplete(() => ColorOpenClose(0));
    }

    private void ColorOpenClose(int a)
    {
        var color = AddCoinText.color;
        color.a = a;
        AddCoinText.color = color;
    }

    private void LosePanelActive()
    {
        LosePanel.SetActive(true);
        DeleteMaskObje();
    }

    private void WinPanelActive()
    {
        WinPanel.SetActive(true);
        DeleteMaskObje();
    }

    public void AssignPosts() //Satın alınma veyahut ücretsiz verme bölümü
    {
        RedPanel.GetComponent<Animator>().enabled = true;
        AnimatorTop.SetBool("isActive", false);
        BetQuantity betQuantity = new BetQuantity();
        BetQuantities.Clear();

        for (int i = 0; i < Earnings.Count; i++)
        {
            int a = Random.Range(0, 6);
            switch (a)
            {
                case 0:
                    betQuantity = BetQuantity.one;
                    break;
                case 1:
                    betQuantity = BetQuantity.two;
                    break;
                case 2:
                    betQuantity = BetQuantity.tree;
                    break;
                case 3:
                    betQuantity = BetQuantity.forr;
                    break;
                case 4:
                    betQuantity = BetQuantity.five;
                    break;
                case 5:
                    betQuantity = BetQuantity.six;
                    break;
            }

            if (i == 5)
            {
                BetBonus betBonus = new BetBonus();
                switch (Random.Range(0, 3))
                {
                    case 0:
                        betBonus = BetBonus.bonusone;
                        break;
                    case 1:
                        betBonus = BetBonus.bonustwo;
                        break;
                    case 2:
                        betBonus = BetBonus.bonustree;
                        break;
                }

                BetBonuses.Add(betBonus);
                Earnings[i].DeletedCheck.BetText.text =
                    "x" + ((int)betBonus * multiValue).ToString("n0");
            }
            else
            {
                BetQuantities.Add(betQuantity);
                Earnings[i].DeletedCheck.BetText.text =
                    ((int)betQuantity * multiValue).ToString("n0");
                continue;
            }
        }
    }
}