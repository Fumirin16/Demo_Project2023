using UnityEngine;

public class PlayerHandManager : MonoBehaviour
{
    /// <summary>
    /// Žè‚É“–‚½‚Á‚½Žž‚Ì‰¹Žæ“¾
    /// </summary>
    [Tooltip("Žè‚É“–‚½‚Á‚½Žž‚Ì‰¹‘}“ü")]
    [SerializeField] AudioClip _hit;

    AudioSource _audioSource;

    bool _SEflag = true;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(_SEflag && collision.gameObject.CompareTag("Audience"))
        {
            _audioSource.PlayOneShot(_hit);
            _SEflag = false;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Audience"))
        {
            _SEflag = true;
        }
    }
}