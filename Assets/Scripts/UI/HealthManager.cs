using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private Image HP_Bar;
    [SerializeField] private float HP_Health;
    public float HP_Max;


    public void increaseHP(float incHeal)
    {
        //Debug.Log($"HP_Health + incHeal {HP_Health} + {incHeal}; HP_MAX = {HP_Max}");
        HP_Health = HP_Max > HP_Health + incHeal ? HP_Max : HP_Health + incHeal;
    
        HP_Bar.fillAmount = HP_Health / HP_Max;
    }
    
    public void decreaseHP(float incDmg)
    {
        HP_Health = HP_Health > incDmg ? HP_Health - incDmg : 0;

        HP_Bar.fillAmount = HP_Health / HP_Max;
    }

    public float getHP()
    {
        return HP_Health;
    }
}
