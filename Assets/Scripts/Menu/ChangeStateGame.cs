using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStateGame : MonoBehaviour
{
    public void ReturnOnMainMenu()
    {
        GameManager.Instance.ChangeState(GameManager.MyState.MAINMENU);
    }
}
