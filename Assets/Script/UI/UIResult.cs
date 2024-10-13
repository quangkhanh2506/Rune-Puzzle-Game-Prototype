using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIResult : MonoBehaviour
{
    public void Nav_Home()
    {
        GameManager.instance.uiHome.SetActive(true);
        GameManager.instance.uiResult.SetActive(false);
    }
}
