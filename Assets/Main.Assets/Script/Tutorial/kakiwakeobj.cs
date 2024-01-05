using UnityEngine;

public class kakiwakeobj : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // É^ÉOÅFEnemy
        if (other.gameObject.tag == "Enemy")
        {
            kakiwakeManager.hitolist.Add(other.gameObject);
        }
    }
}