using System;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ScratchSpace
{
    interface IStrike
    {
        void Damge(GameObject obj);
    }

    [System.Serializable]
    public class DeletedCheck
    {
        public bool Click { get; set; }

        public Text BetText;

    }


    public enum BetQuantity
    {
        one = 50000,
        two = 10000,
        tree = 40000,
        forr = 5000,
        five = 6000,
        six = 7000,

    }



}
