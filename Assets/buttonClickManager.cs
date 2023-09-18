using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonClickManager : MonoBehaviour
{
    private GameObject wwiseObject;
    public AK.Wwise.Event Button_Click2;
    // Start is called before the first frame update
    void Start()
    {
        wwiseObject = GameObject.FindGameObjectWithTag("Wwise Global");
    }

    public void ButtonClick2()
    {
        Button_Click2.Post(wwiseObject);
    }
}
