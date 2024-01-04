using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyDownManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _hip;

    [SerializeField]
    private GameObject _root;

    [SerializeField]
    private float _cheackDis=1f;

   // private float _distance;

    private bool _isDown=false;

    [SerializeField]
    private AudioManager _audioSystem;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float _distance = Vector3.Distance(_hip.transform.position, _root.transform.position);
        //Debug.Log(_distance);

        if (_distance <= _cheackDis&&!_isDown)
        {
            Debug.Log("‹ß‚Ã‚¢‚½");
            _isDown = true;

            //_audioSystem.PlaySESound(SEData.SE.Squwat);
        }
        if(_distance >= _cheackDis && _isDown)
        {
            _isDown = false;
        }
    }
}
