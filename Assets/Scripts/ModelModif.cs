using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelModif : MonoBehaviour
{
    private void Start() {
        transform.localScale = Vector3.zero;
        Open();
    }

    public void Open() {
        transform.LeanScale(Vector3.one, 0.3f).setEaseOutBack();
    }   

    public void Close() {
        transform.LeanScale(Vector3.zero, 0.3f).setEaseInBack();
    }
}