using System.Collections;
using System.Collections.Generic;
using ScratchSpace;
using UnityEngine;

public class ObjectReplicators : MonoBehaviour, IStrike
{
    public void Damge(GameObject obj)
    {
        if (SelectManger.GameActive)
            this.gameObject.transform.parent = obj.transform;
        else
            Destroy(this.gameObject);
  
    }

}
