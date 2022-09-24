using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SpawnManager", menuName = "ScriptableObjects/GUIUpdaterScriptableObject")]
public class GUIUpdaterScriptableObject : ScriptableObject
{
    private UnityEvent<float, float> xpChangeEvent;
    private UnityEvent levelUpEvent;

    public UnityEvent<float, float> XpChangeEvent => xpChangeEvent;
    public UnityEvent LevelUpEvent => levelUpEvent;

    private void OnEnable()
    {
        xpChangeEvent = new UnityEvent<float, float>();
        levelUpEvent = new UnityEvent();
    }

    public void OnXpChange(float currentXp, float maxXp)
    {
        xpChangeEvent.Invoke(currentXp, maxXp);
    }

    public void OnLevelUp()
    {
        levelUpEvent.Invoke();
    }
}
