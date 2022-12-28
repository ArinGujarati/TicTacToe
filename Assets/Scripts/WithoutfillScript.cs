using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WithoutfillScript : MonoBehaviour
{
    [SerializeField]
    Manager manager;

    [SerializeField]
    int Number;


    private void Start()
    {
        manager = FindObjectOfType<Manager>();
    }

    private void OnMouseDown()
    {
        //   Debug.Log(this.gameObject.name);

        if (manager.IsPlay)
        {
            manager.PutObjScript(this.gameObject, Number);

        }
    }
}
