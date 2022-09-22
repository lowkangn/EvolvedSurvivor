using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeUI : MonoBehaviour
{
    public GameObject primarySlot;
    public GameObject secondarySlot;
    public GameObject outputSlot;

    private bool priAbilityPresent;
    private bool secAbilityPresent;

    void Update() {
        if (!priAbilityPresent) {
            Transform priAbility = primarySlot.transform.Find("Ability");
            if (priAbility != null) {
                priAbilityPresent = true;
            }
        }

        if (!secAbilityPresent) {
            Transform secAbility = secondarySlot.transform.Find("Ability");
            if (secAbility != null) {
                secAbilityPresent = true;
            }
        }
    }
}
