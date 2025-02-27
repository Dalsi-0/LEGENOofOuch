using UnityEngine;

public class AbilityController : MonoBehaviour
{
    [SerializeField] private AbilityBase abilityBase;

    public AbilityBase AbilityBase { get; private set; }

    /// <summary>
    /// �����Ƽ ���� ���� �Լ� �۵� �� �ʱ�ȭ
    /// </summary>
    /// <param name="abilityDataSO"></param>
    public void Init(AbilityDataSO abilityDataSO)
    {
        if (abilityBase == null)
        {
            return;
        }

        AbilityBase = abilityBase;

        AbilityBase?.Init(abilityDataSO);
    }


    /// <summary>
    /// ���� ��Ʈ�ѷ��� ������ �ִ� Abilitybase�� UseSkill() �۵�
    /// </summary>
    public void UseSkill()
    {
        AbilityBase?.UseSkill();
    }

}
