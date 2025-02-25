using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GachaManager : MonoBehaviour
{   
    public static GachaManager Instance { get; private set; }
    public GameObject gachaobject;
    public Gacha gacha;
    public GachaController gachaHandler;
    public int[] selectedAbility;
    public string[] AbilityName {  get; private set; }
    public string[] Abilitydescription {  get; private set; }



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        AbilityName = new string[3]; //�迭 ũ�� �ʱ�ȭ
        Abilitydescription = new string[3]; //�迭 ũ�� �ʱ�ȭ
        gachaobject.gameObject.SetActive(false);
    }

    public void Start()
    {
        //gachaobject.gameObject.SetActive(true);
        //StartGacha();
    }

    /// <summary>
    /// ���ӸŴ������� ��í�� �����ϴ� �Լ�
    /// </summary>
    public void StartGacha()
    {
        gachaHandler.init();
        gacha.SelectRandomAbility();
        gachaHandler.StartGacha();
        GetAbilityName();
        GetAbilitydescription();
    }

    /// <summary>
    /// ���ӸŴ������� ��ų�̸��� �������� �Լ�
    /// </summary>
    public void GetAbilityName()
    {
        //���ӸŴ������� ��������
        AbilityName[0] = "1����ų�̸�";
        AbilityName[1] = "2����ų�̸�";
        AbilityName[2] = "3����ų�̸�";

    }

    /// <summary>
    /// ���ӸŴ������� ��ų������ �������� �Լ�
    /// </summary>
    public void GetAbilitydescription()
    {
        Abilitydescription[0] = "1����ų����";
        Abilitydescription[1] = "2����ų����";
        Abilitydescription[2] = "3����ų����";
    }

    /// <summary>
    /// ��í���� ������ ��ų�� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <param name="abilityEnum"></param>
    /// <returns></returns>
    public AbilityEnum GachaSelect(AbilityEnum abilityEnum)
    {
        Debug.Log(abilityEnum);
        gachaobject.gameObject.SetActive(false);
        return abilityEnum;
    }


}
