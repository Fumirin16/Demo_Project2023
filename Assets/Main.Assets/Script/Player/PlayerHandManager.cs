using UnityEngine;

public class PlayerHandManager : MonoBehaviour
{
    /// <summary>
    /// 手に当たった時の音取得
    /// </summary>
    [Tooltip("手に当たった時の音挿入")]
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