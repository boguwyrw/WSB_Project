using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    IEnumerator doorCoroutine;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            doorCoroutine = ManagerUI.Instance.DelayHideInfoPanel();
            ManagerUI.Instance.ShowInfoPanel();
            StartCoroutine(doorCoroutine);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            if(other.gameObject.tag.Equals("Player"))
            {
                Player player = other.gameObject.GetComponent<Player>();
                player.enabled = false;
            }

            ManagerUI.Instance.ShowWinText();
        }
    }
}
