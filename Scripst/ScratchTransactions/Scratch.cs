using System.Collections;
using System.Collections.Generic;
using ScratchSpace;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Scratch : MonoBehaviour
{

    private int[] BetEarning =
    {
        (int)BetQuantity.one,
        (int)BetQuantity.two,
        (int)BetQuantity.tree,
        (int)BetQuantity.forr,
        (int)BetQuantity.five,
        (int)BetQuantity.six,
    };

    List<BetQuantity> BetQuantities = new List<BetQuantity>();
    public static Scratch Instance;

    public List<Earning> Earnings;
    public List<GameObject> MaskObjeDelete = new List<GameObject>();
    public GameObject Mask;
    public GameObject LosePanel;
    public GameObject WinPanel;
    public GameObject RedPanel;
    public GameObject ScratchOffPanel;

    public Animator AnimatorTop;

    private bool _pressed;

    private void Start() => Instance = this;

    private void Update() => ScratchManger();

    private void ScratchManger()
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

    }

    public void DeleteCheckController()
    {
        int conclusion = 0;
        List<int> numbers = new List<int>();
        int a = Earnings.Count(m => m.DeletedCheck.Click == true);
        if (a >= 8)
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
                conclusion += BetEarning[numbers[i]];

            if (conclusion <= 0)
                Invoke("LosePanelActive", 2f);
            else
            {
                //Para burda eklenicek
                Invoke("WinPanelActive", 2f);
                WinPanel.transform.GetChild(0).GetComponent<Text>().text = "Win : " + (conclusion).ToString("n0");
            }
            BetQuantities.Clear();
            SelectManger.GameActive = false;
        }
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

    public void AssignPosts()
    {
        BetQuantity betQuantity = new BetQuantity();
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

            BetQuantities.Add(betQuantity);
            Earnings[i].DeletedCheck.BetText.text = ((int)betQuantity).ToString("n0");
        }
    }


}



