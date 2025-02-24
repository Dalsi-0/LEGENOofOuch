using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject pannel;

    public void toglePanel()
    {
        pannel.SetActive(!pannel.activeSelf);
    }
}
