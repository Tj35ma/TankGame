using UnityEngine;
using UnityEngine.UI;

public class SliderExp : SliderController
{
    [SerializeField] protected PlayerLevel playerLevel;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerLevel();
    }

    protected virtual void LoadPlayerLevel()
    {
        if (playerLevel != null) return;
        this.playerLevel = Object.FindFirstObjectByType<PlayerLevel>();
        //this.playerLevel = PlayerManager.Instance.PlayerLevel;
        Debug.Log(transform.name + ": LoadPlayerLevel", gameObject);
    }

    protected override float GetValue()
    {
        if (playerLevel == null)
            return 0f;

        int currentExp = playerLevel.GetCurrentExp();
        int nextLevelExp = playerLevel.GetNextLevelExp();

        if (nextLevelExp <= 0)
            return 1f;

        return Mathf.Clamp01((float)currentExp / nextLevelExp);
    }
}
