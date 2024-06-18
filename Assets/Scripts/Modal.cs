using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modal : MonoBehaviour
{
    private void Start() {
        transform.localScale = Vector3.zero;
        Open();
    }

    public void Open() {
        transform.LeanScale(Vector3.one, 1f).setEaseOutBack();
    }   

    public void Close() {
        transform.LeanScale(Vector3.zero, 1f).setEaseInBack();
    }
}