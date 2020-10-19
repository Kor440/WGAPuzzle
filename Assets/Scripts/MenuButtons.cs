using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuButtons : MonoBehaviour
{
    public Animator panelAnim;
    public Animator gameIntroAnim;
    public Animator gameWinAnim;


    public void DoExit()
    {
        Application.Quit();
    }

    public void NewGameButton()
    {
        if (panelAnim != null && gameWinAnim != null)
        {
            panelAnim.SetBool("Out", true);
            gameWinAnim.SetBool("Out", true);
        }

    }

    public void StartButton()
    {
        if (panelAnim != null && gameIntroAnim != null)
        {
            panelAnim.SetBool("Out", true);
            gameIntroAnim.SetBool("Out", true);
        }
        
    }
}
