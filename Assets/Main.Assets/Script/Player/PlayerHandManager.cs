using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandManager : MonoBehaviour
{
    [SerializeField] AudioClip _hit;

    AudioSource _audioSource;

    bool _SEflag = true;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Audience")
        {
            if (_SEflag)
            {
                _audioSource.PlayOneShot(_hit);
                //Debug.Log("ìñÇΩÇ¡ÇΩ");
                _SEflag = false;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Audience"))
        {
            _SEflag = true;
            //Debug.Log("èoÇΩ");
        }
    }
}