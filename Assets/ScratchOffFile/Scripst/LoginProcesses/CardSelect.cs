using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSelect : MonoBehaviour
{
    public GameObject GameStartButton;

    private void OnMouseDown()
    {
        // ScratchManger.Instance.CartPanel.GetComponent<GridLayoutGroup>().enabled = false;

        if (SelectManger.SelectNumber >= 1)
            SelectManger.SelectObje.transform.GetComponent<Image>().color = Color.white;
        else
            SelectManger.SelectNumber++;

        GameStartButton.SetActive(true);
        this.gameObject.GetComponent<Image>().color = Color.red;
        SelectManger.SelectObje = gameObject;
    }

}


