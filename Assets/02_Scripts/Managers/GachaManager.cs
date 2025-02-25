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
        gachaobject.gameObject.SetActive(true);
        StartGacha();
    }

    /// <summary>
    /// ���ӸŴ������� ��í�� �����ϴ� �Լ�
    /// </summary>
    public void StartGacha()
    {
        gachaHandler.init();
        gacha.SelectRandomAbility();
        GameManager.Instance.SetAbilityText();
        gachaHandler.StartGacha();
    }

    /// <summary>
    /// ���ӸŴ������� ��ų�̸��� �������� �Լ�
    /// </summary>
    public void GetAbilityName(string[] name)
    {
        //���ӸŴ������� ��������
        gacha.SelectRandomAbility();
        AbilityName[0] = name[0];
        AbilityName[1] = name[1];
        AbilityName[2] = name[2];

    }

    /// <summary>
    /// ���ӸŴ������� ��ų������ �������� �Լ�
    /// </summary>
    public void GetAbilitydescription(string[] description)
    {
        Abilitydescription[0] = description[0];
        Abilitydescription[1] = description[1];
        Abilitydescription[2] = description[2];
    }

    /// <summary>
    /// ��í���� ������ ��ų�� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <param name="abilityEnum"></param>
    /// <returns></returns>
    public void GachaSelect(AbilityEnum abilityEnum)
    {
        Debug.Log(abilityEnum);
        gachaobject.gameObject.SetActive(false);
        GameManager.Instance.GetAbility(abilityEnum);
    }


}
