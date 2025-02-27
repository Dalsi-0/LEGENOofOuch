using UnityEngine;

public class BloodThirst : AbilityBase
{
    public override void Init(AbilityDataSO abilityDataSO)
    {
        base.Init(abilityDataSO);
        UpdateAbility();
    }

    protected override void UpdateAbility()
    {
        // �ִ� ü���� {n}% ��ŭ ȸ��
        float healPercentage = isUpgraded ? abilityData.values[1] : abilityData.values[0];
        GameManager.Instance.healReward = Mathf.Max(1, Mathf.RoundToInt(GameManager.Instance.player.MaxHp * (healPercentage * 0.01f)));
    }
}