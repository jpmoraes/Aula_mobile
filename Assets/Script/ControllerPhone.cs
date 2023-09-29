using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerPhone : MonoBehaviour
{
    // Start is called before the first frame update
    public void Vibration()
    {
        AndroidAPI.Vibration(300);
    }
}
