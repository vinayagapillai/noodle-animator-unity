using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class NoodleAnimator : MonoBehaviour
{
    
    public List<Transform> ChildTransfomrs;
    

    private void OnValidate()
    {
        ChildTransfomrs = new List<Transform>();
        if (transform.GetChild(0).name == "Armature")
        {
            ChildTransfomrs = transform.GetChild(0).GetChild(0).GetComponentsInChildren<Transform>().ToList();
            if (!ChildTransfomrs[0].TryGetComponent(out Rigidbody rb))
            {
                foreach (Transform i in ChildTransfomrs)
                {
                    i.AddComponent<CapsuleCollider>();
                    i.AddComponent<Rigidbody>();
                }

                for (int i = 1; i < ChildTransfomrs.Count; i++)
                {
                    ChildTransfomrs[i].AddComponent<CharacterJoint>();
                }

                for (int i = 1; i < ChildTransfomrs.Count; i++)
                {
                    ChildTransfomrs[i].GetComponent<CharacterJoint>().connectedBody =
                        ChildTransfomrs[i - 1].GetComponent<Rigidbody>();
                }
            }
        }
    }
}
