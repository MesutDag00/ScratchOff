using System.Collections;
using System.Collections.Generic;
using ScratchSpace;
using UnityEngine;

public class Earning : MonoBehaviour
{
    public int a;
    public bool _first;

    public DeletedCheck DeletedCheck;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.tag == "ScratchOffPanel")
        {
            a++;
            if (a > 15 && !_first)
            {
                _first = true;
                DeletedCheck.Click = true;
                a = 0;
                ScratchManger.Counter++;
                if (ScratchManger.Counter == 8)
                    Scratch.Instance.DeleteCheckController();
            }
        }
    }
}