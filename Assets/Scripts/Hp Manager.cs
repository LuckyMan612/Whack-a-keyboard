using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HpManager : MonoBehaviour
{
    public Image health1;
    public Image health2;
    public Image health3;
   

    int currentHealth = 3;


    public void LoseHP()
    {
        if(currentHealth == 3)
        {
            currentHealth = 2;
            health3.color = new Color(60,60,60);
        }else if (currentHealth == 2)
        {
            currentHealth = 1;
            health2.color = new Color(60, 60, 60);
        }
        else if (currentHealth == 1)
        {
            currentHealth = 0;
            health1.color = new Color(60, 60, 60);
            GameManager.ChangeStateInvoke?.Invoke(GameState.GameLose);
        }
      


    }
}
