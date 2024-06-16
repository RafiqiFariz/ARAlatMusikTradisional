using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SulingControll : MonoBehaviour
{
    int soundPoint = 0;
    private AudioSource audioSource;
    [SerializeField]
    protected AudioClip noteA;
    [SerializeField]
    protected AudioClip noteB;
    [SerializeField]
    protected AudioClip noteC;
    [SerializeField]
    protected AudioClip noteD;
    [SerializeField]
    protected AudioClip noteE;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(soundPoint == 0){
            StartTimer(1.5f, noteA);
        }
        else if(soundPoint == 1){
            StartTimer(1.5f, noteB);
        }
        else if(soundPoint == 2){
           StartTimer(1.5f, noteC);
        }
        else if(soundPoint == 7){
            StartTimer(1.5f, noteD);
        }
    }

    IEnumerator StartTimer(float duration, AudioClip audioClip)
    {
        Debug.Log("Timer dimulai");
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
        yield return new WaitForSeconds(duration);
        audioSource.Stop();
        Debug.Log("Timer selesai");
    }

    public void OnSelected(int soundPoint){
        this.soundPoint += soundPoint;
    }

    public void OnDeselected(int soundPoint){
        this.soundPoint -= soundPoint;
    }
}
