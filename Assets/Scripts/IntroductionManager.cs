using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroductionManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine("Countdown", 10);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(2);
        }
    }

    private IEnumerator Countdown(int time)
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
        }

        SceneManager.LoadScene(2);
    }
}
