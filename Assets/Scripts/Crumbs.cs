using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crumbs : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(CrumbShot());
    }

    IEnumerator CrumbShot()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}