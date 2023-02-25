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
using RTSEngine.Determinism;
using RTSEngine.Health;
using RTSEngine.EntityComponent;
using Grid.Restore;

namespace Grid.Construct
{
    public class newReEditScript : MonoBehaviour
    {
        //конструктор барак 1


        //меню
        public GameObject UIBar;
        public GameObject GameManager;


        //количество эергии текст
        public Text energyCountText;


        //изменение хп
        public Text heetPointCountText;
        public Slider heetPointSlider;
        public int maxHeetPoint;


        //изменение урона по юнитам
        public Text DamageUnitCountText;
        public Slider DamageUnitSlider;
        public int DamageUnitCount;


        //изменение урона по зданиям
        public Text DamageBuildingCountText;
        public Slider DamageBuildingSlider;
        public int DamageBuildingCount;


        //кнопка применить
        public Button Submit;

        //Юнит
        public GameObject prefabhp;

        //переменная энергии
        public int energyCount;



        //скорость атаки
        public Text AttackSpeedText;
        public Slider AttackSpeedSlider;
        public float AttackSpeedCount;


        //дальность атаки
        public Text AttackDistanceText;
        public Slider AttackDistanceSlider;
        public float AttackDistanceCount;


        //скорость снаряда
        public Text ProjectileAttackSpeedText;
        public Slider ProjectileAttackSpeedSlider;
        public float ProjectileAttackSpeedCount;


        //скорость движения
        public Text UnitSpeedText;
        public Slider UnitSpeedSlider;
        public float UnitSpeedCount;

        public int plaseholderInt;
        //      *****тип урона******
        //      *****броня*****
        public int Armor;
        public Text ArmorText;
        public Slider ArmorSlider;

        //рестор хп
        public int restoreInsec = 0;
        public bool RestoreBool = false;
        public Slider restoreInsecSlider;
        public Text restoreInsecText;
        //хп
        public void HeetPoint()
        {
            //изменение количества хп
            prefabhp.GetComponent<UnitHealth>().maxHealth = maxHeetPoint;
            prefabhp.GetComponent<UnitHealth>().initialHealth = maxHeetPoint;

        }
        public void UnitSpeed()
        {
            prefabhp.transform.GetChild(2).gameObject.GetComponent<UnitMovement>().speed = new TimeModifiedFloat(UnitSpeedCount);
        }

        //ДАМАГ
        public void Damage()
        {
            prefabhp.transform.GetChild(4).gameObject.GetComponent<UnitAttack>().damage.data.unit = DamageUnitCount;
            prefabhp.transform.GetChild(4).gameObject.GetComponent<UnitAttack>().damage.data.building = DamageBuildingCount;
            prefabhp.transform.GetChild(4).gameObject.GetComponent<UnitAttack>().reloadDuration = AttackSpeedCount;
            prefabhp.transform.GetChild(4).gameObject.GetComponent<UnitAttack>().formation.unitStoppingDistance = new RTSEngine.FloatRange(AttackDistanceCount - 0.5f, AttackDistanceCount + 0.5f);
        }
        public void Armorr()
        {
            prefabhp.GetComponent<UnitHealth>().Armor = Armor;
        }
        public void addRestore()
        {
            if (restoreInsec > 0) RestoreBool = true;
            if (restoreInsec == 0) RestoreBool = false;
            prefabhp.GetComponent<HeelthRestore>().enabled = RestoreBool;
            prefabhp.GetComponent<HeelthRestore>().restoreInSec = restoreInsec;
            
        }

        //ОБНОВЛЕНИЕ UI МЕНЮ
        public void UpdateUI()
        {
            if (energyCount > 500)
            {
                energyCountText.color = new Color(255f, 0f, 0f);
            }
            if (energyCount <= 500)
            {
                energyCountText.color = new Color(208f, 208f, 208f);
            }
            maxHeetPoint = (int)heetPointSlider.value;

            DamageUnitCount = (int)DamageUnitSlider.value;

            DamageBuildingCount = (int)DamageBuildingSlider.value;

            restoreInsec = (int)restoreInsecSlider.value;

            AttackDistanceCount = AttackDistanceSlider.value;

            AttackSpeedCount = AttackSpeedSlider.value;

            UnitSpeedCount = UnitSpeedSlider.value;
            Armor = (int)ArmorSlider.value;

            CalculateEnergy();
            //измениие меню энергии
            energyCountText.text = energyCount.ToString();

            //меню хп
            plaseholderInt = (int)heetPointSlider.value;
            heetPointCountText.text = plaseholderInt.ToString();
            plaseholderInt = 0;

            //меню урона по юнитам
            plaseholderInt = (int)DamageUnitSlider.value;
            DamageUnitCountText.text = plaseholderInt.ToString();
            plaseholderInt = 0;

            //меню урона по зданиям
            plaseholderInt = (int)DamageBuildingSlider.value;
            DamageBuildingCountText.text = plaseholderInt.ToString();
            plaseholderInt = 0;

            plaseholderInt = (int)AttackDistanceSlider.value;
            AttackDistanceText.text = plaseholderInt.ToString();
            plaseholderInt = 0;

            plaseholderInt = (int)AttackSpeedSlider.value;
            AttackSpeedText.text = plaseholderInt.ToString();
            plaseholderInt = 0;

            plaseholderInt = (int)UnitSpeedSlider.value;
            UnitSpeedText.text = plaseholderInt.ToString();
            plaseholderInt = 0;

            plaseholderInt = (int)restoreInsecSlider.value;
            restoreInsecText.text = plaseholderInt.ToString();
            plaseholderInt = 0;
            plaseholderInt = (int)ArmorSlider.value;
            ArmorText.text = plaseholderInt.ToString();
            plaseholderInt = 0;
        }


        //Расчет стоимости энергии
        public void CalculateEnergy()
        {
            energyCount = 0;
            energyCount = maxHeetPoint + (DamageBuildingCount * 2) + (DamageUnitCount * 2);
        }


        //измение энергии в префабе
        public void EqualsEnergy()
        {
            this.GetComponent<UnitCreator>().creationTasks[0].requiredResources[0].value.amount = energyCount;
        }



        public void Start()
        {
            CalculateEnergy();
            FindGameObjects();

            FindUI();


            heetPointSlider.onValueChanged.AddListener(delegate { UpdateUI(); });
            DamageUnitSlider.onValueChanged.AddListener(delegate { UpdateUI(); });
            DamageBuildingSlider.onValueChanged.AddListener(delegate { UpdateUI(); });
            AttackDistanceSlider.onValueChanged.AddListener(delegate { UpdateUI(); });
            AttackSpeedSlider.onValueChanged.AddListener(delegate { UpdateUI(); });
            Submit.onClick.AddListener(delegate { Relise(); });
            UnitSpeedSlider.onValueChanged.AddListener(delegate { UpdateUI(); });
            restoreInsecSlider.onValueChanged.AddListener(delegate { UpdateUI(); });
            ArmorSlider.onValueChanged.AddListener(delegate { UpdateUI(); });

            heetPointSlider.value = maxHeetPoint;
            heetPointCountText.text = maxHeetPoint.ToString();

            DamageUnitSlider.value = DamageUnitCount;
            DamageUnitCountText.text = DamageUnitCount.ToString();

            DamageBuildingSlider.value = DamageBuildingCount;
            DamageBuildingCountText.text = DamageBuildingCount.ToString();

            UnitSpeedSlider.value = UnitSpeedCount;
            UnitSpeedText.text = UnitSpeedCount.ToString();

            restoreInsecSlider.value = restoreInsec;
            restoreInsecText.text = restoreInsec.ToString();

            ArmorSlider.value = Armor;
            ArmorText.text = Armor.ToString();
        }


        public void Update()
        {

            //для примера по кнопке "стрелочка вверх"
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                FindLumfirst();
                UpdateUI();
                HeetPoint();
                Damage();
                UnitSpeed();
                Debug.Log("KeyDown");
            }
        }


        // примениеие изменений и осуществление замены
        public void Relise()
        {

            if (energyCount > 500)
            {
                return;
            }
            FindLumfirst();

            maxHeetPoint = (int)heetPointSlider.value;

            DamageUnitCount = (int)DamageUnitSlider.value;

            DamageBuildingCount = (int)DamageBuildingSlider.value;

            AttackDistanceCount = AttackDistanceSlider.value;

            AttackSpeedCount = AttackSpeedSlider.value;
            Armor =(int) ArmorSlider.value;

            CalculateEnergy();

            UpdateUI();

            HeetPoint();
            Damage();
            EqualsEnergy();
            UnitSpeed();
            addRestore();
            Armorr();

        }


        //поиск 
        public void FindGameObjects()
        {
            UIBar = GameObject.Find("UnitCreator");
            GameManager = GameObject.Find("GameManager");
        }

        //поиск элементов интерфейса конструктора
        public void FindUI()
        {
            energyCountText = UIBar.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<Text>();
            heetPointCountText = UIBar.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(1).gameObject.GetComponent<Text>();
            heetPointSlider = UIBar.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).gameObject.GetComponent<Slider>();
            DamageUnitCountText = UIBar.transform.GetChild(0).GetChild(0).GetChild(3).GetChild(1).gameObject.GetComponent<Text>();
            DamageUnitSlider = UIBar.transform.GetChild(0).GetChild(0).GetChild(3).GetChild(0).gameObject.GetComponent<Slider>();
            DamageBuildingCountText = UIBar.transform.GetChild(0).GetChild(0).GetChild(4).GetChild(1).gameObject.GetComponent<Text>();
            DamageBuildingSlider = UIBar.transform.GetChild(0).GetChild(0).GetChild(4).GetChild(0).gameObject.GetComponent<Slider>();
            Submit = UIBar.transform.GetChild(0).GetChild(0).GetChild(5).gameObject.GetComponent<Button>();
            AttackSpeedText = UIBar.transform.GetChild(0).GetChild(0).GetChild(6).GetChild(1).gameObject.GetComponent<Text>();
            AttackSpeedSlider = UIBar.transform.GetChild(0).GetChild(0).GetChild(6).GetChild(0).gameObject.GetComponent<Slider>();
            AttackDistanceText = UIBar.transform.GetChild(0).GetChild(0).GetChild(7).GetChild(1).gameObject.GetComponent<Text>();
            AttackDistanceSlider = UIBar.transform.GetChild(0).GetChild(0).GetChild(7).GetChild(0).gameObject.GetComponent<Slider>();
            UnitSpeedText = UIBar.transform.GetChild(0).GetChild(0).GetChild(8).GetChild(1).gameObject.GetComponent<Text>();
            UnitSpeedSlider = UIBar.transform.GetChild(0).GetChild(0).GetChild(8).GetChild(0).gameObject.GetComponent<Slider>();
            restoreInsecText = UIBar.transform.GetChild(0).GetChild(0).GetChild(9).GetChild(1).gameObject.GetComponent<Text>();
            restoreInsecSlider = UIBar.transform.GetChild(0).GetChild(0).GetChild(9).GetChild(0).gameObject.GetComponent<Slider>();
            ArmorText = UIBar.transform.GetChild(0).GetChild(0).GetChild(10).GetChild(1).gameObject.GetComponent<Text>();
            ArmorSlider = UIBar.transform.GetChild(0).GetChild(0).GetChild(10).GetChild(0).gameObject.GetComponent<Slider>();
        }

        //поиск префаба юнита
        public void FindLumfirst()
        {
            //поиск родительского класса

            int count = GameManager.transform.childCount;
            for (int i = 0; i < count; i++)
            {
                if (GameManager.transform.GetChild(i).gameObject.name == "PrefabsParent")
                {
                    //поиск юнита
                    Debug.Log("ObjectFound parent");
                    int count2 = GameManager.transform.GetChild(i).childCount;
                    for (int j = 0; j < count2; j++)
                    {
                        if (GameManager.transform.GetChild(i).GetChild(j).gameObject.name == "Lum_lite(Clone)")
                        {
                            prefabhp = GameManager.transform.GetChild(i).GetChild(j).gameObject;
                            Debug.Log("ObjectFound");
                            return;
                        }
                    }
                }
            }

        }

    }
}