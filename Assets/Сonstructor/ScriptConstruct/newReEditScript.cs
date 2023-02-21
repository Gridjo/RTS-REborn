using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RTSEngine.Attack;
using RTSEngine.Entities;
using RTSEngine.Movement;
using RTSEngine.Event;
using RTSEngine.Logging;
using RTSEngine.UI;
using RTSEngine.Health;

public class newReEditScript : MonoBehaviour
{
    //конструктор барак 1

    public GameObject UIBar;
    public Text energyCountText;
    public Text heetPointCountText;
    public Slider heetPointSlider;
    public GameObject prefabhp;

    public int maxHeetPoint;
    public int energyCount;

    private int plaseholderInt;



    public void HeetPoint()
    {
        //изменение количества хп
        prefabhp.GetComponent<UnitHealth>().maxHealth = maxHeetPoint;
        prefabhp.GetComponent<UnitHealth>().initialHealth = maxHeetPoint;
    }
    public void UpdateUI()
    {
        energyCountText.text = energyCount.ToString();
        plaseholderInt = (int)heetPointSlider.value;
        heetPointCountText.text = plaseholderInt.ToString();
    }
    public void Start()
    {
        heetPointSlider.value = maxHeetPoint;
        heetPointCountText.text = maxHeetPoint.ToString();
    }
    public void Update()
    {
        //для примера по кнопке "стрелочка вверх"
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            FindLumfirst();
            UpdateUI();
            HeetPoint();
            Debug.Log("KeyDown");
        }
    }
    public void Relise()
    {
        FindLumfirst();
        maxHeetPoint = (int)heetPointSlider.value;
        UpdateUI();
        HeetPoint();

    }
    public void FindLumfirst()
    {
        //поиск родительского класса
        int count = transform.parent.parent.childCount;
        for(int i = 0; i < count; i++)
        {
            if(transform.parent.parent.GetChild(i).gameObject.name== "PrefabsParent")
            {
                //поиск юнита
                Debug.Log("ObjectFound parent");
                int count2 = transform.parent.parent.GetChild(i).childCount;
                for (int j = 0; j < count2; j++)
                {
                    if (transform.parent.parent.GetChild(i).GetChild(j).gameObject.name == "Lum_lite(Clone)")
                    {
                        prefabhp = transform.parent.parent.GetChild(i).GetChild(j).gameObject;
                        Debug.Log("ObjectFound");
                        return;
                    }
                }
            }
        }

    }

}
