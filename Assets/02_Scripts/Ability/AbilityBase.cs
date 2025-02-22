using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;


public struct AbilityData
{
    public AbilityEnum abilityID { get; }
    public string abilityName { get; }
    public string description { get; }
    public RankEnum rank { get; }
    public float[] values { get; }

    public AbilityData(AbilityEnum abilityID, string abilityName, string description, RankEnum rank, float[] values)
    {
        this.abilityID = abilityID;
        this.abilityName = abilityName;
        this.description = description;
        this.rank = rank;
        this.values = values;
    }
}


public abstract class AbilityBase : MonoBehaviour
{
    public AbilityData abilityData { get; private set; }

    public bool isUpgraded { get; private set; }


    /// <summary>
    /// ��ų ���� �ʱ�ȭ
    /// </summary>
    /// <param name="abilityDataSO">��ų ���� SO</param>
    public virtual void Init(AbilityDataSO abilityDataSO)
    {
        abilityData = new AbilityData(abilityDataSO.Ability,
            abilityDataSO.AbilityName,
            abilityDataSO.Description,
            abilityDataSO.Rank,
            abilityDataSO.Values
            );
    }

    public abstract void UseSkill();

    public void UpgradeAbility()
    {
        isUpgraded = true;
    }
}
