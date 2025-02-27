using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class AbilityRepositoy : MonoBehaviour
{
    [SerializeField] private List<AbilityController> ownedAbilities = new List<AbilityController>();

    private Dictionary<AbilityEnum, GameObject> dicAbilityPrefabs = new Dictionary<AbilityEnum, GameObject>();
    private Dictionary<AbilityEnum, AbilityDataSO> dicAbilityDataSO = new Dictionary<AbilityEnum, AbilityDataSO>();

    [SerializeField] private AbilityDataSO[] abilityDataSOs;
    [SerializeField] private GameObject[] abilityPrefabs;

    private void Awake()
    {
        InitDictionary();
    }

    /// <summary>
    /// abilityDataSOs ���� ����
    /// </summary>
    /// <param name="abilityDataSOs"></param>
    public void SetabilityDataSOs(AbilityDataSO[] abilityDataSOs)
    {
        this.abilityDataSOs = abilityDataSOs;
    }

    /// <summary>
    /// dicAbilityPrefabs, dicAbilityDataSO�ʱ�ȭ
    /// </summary>
    private void InitDictionary()
    {
        dicAbilityPrefabs.Clear();
        dicAbilityDataSO.Clear();

        for (int i = 0; i < abilityDataSOs.Length; i++)
        {
            AbilityEnum key = abilityDataSOs[i].Ability;
            GameObject prefab = abilityPrefabs[i];
            AbilityDataSO data = abilityDataSOs[i];

            dicAbilityPrefabs[key] = prefab;
            dicAbilityDataSO[key] = data;
        }
    }

    /// <summary>
    /// �������� ��� �����Ƽ �Ҹ�
    /// </summary>
    public void ClearOwnedAbilities()
    {
        for (int i = ownedAbilities.Count - 1; i >= 0; i--)
        {
            Destroy(ownedAbilities[i].gameObject);
            ownedAbilities.RemoveAt(i); 
        }
    }

    /// <summary>
    /// �ش� �����Ƽ�� Dic���� �˻� �� ������Ʈ �����Ͽ� ���� �����Ƽ ��Ͽ� �߰� �Ǵ� ��ȭ
    /// </summary>
    /// <param name="ability">�����Ƽ�� ID</param>
    /// <returns>������ �����Ƽ ������Ʈ</returns>
    public GameObject SetAbility(AbilityEnum ability)
    {
        // �̹� ���� ���� ��� ��ȭ
        foreach (var varAbilityController in ownedAbilities)
        {
            if (varAbilityController.AbilityBase != null && varAbilityController.AbilityBase.abilityData.abilityID == ability)
            {
                UpgradeOwnedAbility(ability);
                return null;
            }
        }

        GameObject abilityPrefab = Instantiate(dicAbilityPrefabs[ability]);
        AbilityController abilityController = abilityPrefab.transform.GetComponent<AbilityController>();

        ownedAbilities.Add(abilityController);
        abilityController.Init(dicAbilityDataSO[ability]);

        return abilityPrefab;
    }

    /// <summary>
    /// ����Ʈ ���� ���� ReadOnlyCollection ��ȯ, ���� �Ұ�
    /// </summary>
    public ReadOnlyCollection<AbilityController> GetOwnedAbilities()
    {
        return ownedAbilities.AsReadOnly();
    }

    /// <summary>
    /// �������� �����Ƽ ���� �ش� �����Ƽ ��ȭ
    /// </summary>
    /// <param name="ability"></param>
    public void UpgradeOwnedAbility(AbilityEnum ability)
    {
        foreach (var abilityController in ownedAbilities)
        {
            if (abilityController.AbilityBase.abilityData.abilityID == ability)
            {
                abilityController.AbilityBase.UpgradeAbility();
                return;
            }
        }
    }


    /// <summary>
    /// ��� �����Ƽ �� ���ϴ� �����Ƽ�� AbilityData ã�Ƽ� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <param name="abilityEnum">���ϴ� �����Ƽ�� ID</param>
    /// <returns></returns>
    public AbilityDataSO FindAbilityData(AbilityEnum abilityEnum)
    {
        return dicAbilityDataSO[abilityEnum];
    }
}
