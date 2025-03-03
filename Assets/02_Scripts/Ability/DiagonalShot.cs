using UnityEngine;

public class DiagonalShot : AbilityBase
{
    GameManager gameManager;
    ProjectileManager projectileManager;
    PlayerCharacter player;
    float[] angles;

    public override void Init(AbilityDataSO abilityDataSO)
    {
        base.Init(abilityDataSO);
        gameManager = GameManager.Instance;
        projectileManager = gameManager.ProjectileManager;
        player = gameManager.player;
        UpdateAbility();
    }

    protected override void UpdateAbility()
    {
        // 대각선 화살 각도 설정
        angles = isUpgraded ? new float[] { 30f, 60f } : new float[] { 45f };
    }

    public override void UseSkill()
    {
        Vector3 lookDir = player.LookDir;
        PlayerClassEnum pClass = player.GetPlayerClass();

        // 기본 화살 (전방 1개)
        ShootArrow(player.transform.position, lookDir, pClass);

        foreach (float angle in angles)
        {
            Vector3 leftDir = Quaternion.Euler(0, 0, angle) * lookDir;
            Vector3 rightDir = Quaternion.Euler(0, 0, -angle) * lookDir;

            ShootArrow(player.transform.position, leftDir, pClass);
            ShootArrow(player.transform.position, rightDir, pClass);
        }
    }

    private void ShootArrow(Vector3 position, Vector3 direction, PlayerClassEnum pClass)
    {
        projectileManager.ShootPlayerProjectile(position, direction, pClass);
    }
}