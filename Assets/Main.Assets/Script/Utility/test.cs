using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField]
    private AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager.PlaySESound(SEData.SE.Walk);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
