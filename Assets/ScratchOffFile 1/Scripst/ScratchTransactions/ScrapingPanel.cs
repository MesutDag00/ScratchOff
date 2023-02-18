using System.Collections;
using System.Collections.Generic;
using ScratchSpace;
using UnityEngine;

public class ScrapingPanel : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ScratchOffPanel") collision.gameObject.GetComponent<IStrike>().Damge(this.gameObject);
    }

}
