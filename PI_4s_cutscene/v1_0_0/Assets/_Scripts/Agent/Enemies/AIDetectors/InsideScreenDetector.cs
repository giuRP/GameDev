using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideScreenDetector : MonoBehaviour
{
    [SerializeField]
    float delayTime = 0.2f;

    public bool IsInsideScreen { get; private set; }

    private void Start()
    {
        delayTime = Random.Range(1.0f, 2.5f);
        //Debug.Log(delayTime);

        IsInsideScreen = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StopAllCoroutines();

        if (collision.CompareTag("EnemyConfiner"))
        {
            StartCoroutine(Delay(true));
        }     
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StopAllCoroutines();

        if (collision.CompareTag("EnemyConfiner"))
        {
            StartCoroutine(Delay(false));
        }
    }

    IEnumerator Delay(bool val)
    {
        yield return new WaitForSeconds(delayTime);
        IsInsideScreen = val;
    }
}
