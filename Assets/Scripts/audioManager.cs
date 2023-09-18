using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public AK.Wwise.Event PlayButton_Click;
    public AK.Wwise.Event Button_Click;

    private static audioManager instance;

    public static audioManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        // If no audioManager ever existed, we are it.
        if (instance == null)
            instance = this;
        // If one already exist, it's because it came from another level.
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    public void PlayButton()
    {
        PlayButton_Click.Post(gameObject);
    }

    public void ButtonClick()
    {
        Button_Click.Post(gameObject);
    }
}
