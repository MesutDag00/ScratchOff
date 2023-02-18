using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScratchManger : MonoBehaviour
{
    public static ScratchManger Instance;

    public Transform AnimasyonPosition;
    public GameObject ScratchOffPanel;
    public GameObject CartPanel;
    public GameObject CardSelectButton;

    private void Awake() => Instance = this;

    public void DrawingCards()
    {
        Transform _cardTransform = SelectManger.SelectObje.transform;
        _cardTransform.SetParent(AnimasyonPosition);
        _cardTransform.DOMove(AnimasyonPosition.position, 0.8f).SetEase(Ease.InBack).OnComplete(
            () => _cardTransform.DOScale(4.27f, 1f).OnComplete
            (() => _cardTransform.DORotate(new Vector3(0, -90, 0), 1)).OnComplete(() =>
            {
                CardSelectButton.SetActive(false);
                CartPanel.SetActive(false);
                _cardTransform.gameObject.SetActive(false);
                ScratchOffPanel.SetActive(true);
                ScratchOffPanel.transform.DORotate(new Vector3(0, 0, 0), 1);
                SelectManger.GameActive = true;
                SelectManger.SelectObje.transform.DOScale(1, 0.3f);
                SelectManger.SelectObje.GetComponent<Image>().color = Color.white;
                SelectManger.SelectObje.SetActive(true);
                SelectManger.SelectObje.transform.SetParent(CartPanel.transform);
            })
            );
    }

    public void GamePlay()
    {
        Scratch.Instance.RedPanel.GetComponent<Animator>().enabled = true;
        CartPanel.GetComponent<GridLayoutGroup>().enabled = true;
        Scratch.Instance.AssignPosts();
        Scratch.Instance.LosePanel.SetActive(false);
        Scratch.Instance.WinPanel.SetActive(false);
        Scratch.Instance.AnimatorTop.SetBool("isActive", false);

    }

    public void GamePlayPaid()
    {
        //ödeme için gerkeli þeyler buraya yaýzlýcak veyahut çekilicek
        GamePlay();
    }

    public void ComebackMenu()
    {
        Scratch.Instance.RedPanel.GetComponent<Animator>().enabled = false;
        Scratch.Instance.ScratchOffPanel.SetActive(false);
    }

}

static class SelectManger
{
    public static GameObject SelectObje;

    public static int SelectNumber;

    public static bool GameActive = false;
}           

