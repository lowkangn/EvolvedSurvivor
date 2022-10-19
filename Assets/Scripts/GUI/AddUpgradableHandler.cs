using UnityEngine;
using TeamOne.EvolvedSurvivor;

public class AddUpgradableHandler : MonoBehaviour
{
    [SerializeField] private NewUpgradableOptionUI[] NewUpgradablesButtons;
    [SerializeField] private GameObject confirmButton;

    private Upgradable currentSelectedUpgradable;

    private Upgradable[] NewUpgradables;

    private AbilityManager abilityManager;
    private AbilityGenerator abilityGenerator;
    private PassiveAbilityManager passiveAbilityManager;
    private int numOfUpgradableOptions;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        abilityManager = player.GetComponentInChildren<AbilityManager>();
        abilityGenerator = player.GetComponentInChildren<AbilityGenerator>();
        passiveAbilityManager = player.GetComponentInChildren<PassiveAbilityManager>();

        numOfUpgradableOptions = NewUpgradablesButtons.Length;
        NewUpgradables = new Upgradable[numOfUpgradableOptions];
    }

    private void OnEnable()
    {
        GenerateNewUpgradables();
    }

    private void OnDisable()
    {
        ClearUnselectedUpgradables();
    }

    // Generate new upgradables in Level Up screen
    public void GenerateNewUpgradables()
    {
        for (int i = 0; i < numOfUpgradableOptions; i++)
        {
            if (i == 0 && Random.value < 0.4f && !passiveAbilityManager.IsAllPassivesMaxedOut() && !abilityManager.IsEmpty())
            {
                PassiveAbility passiveAbility = passiveAbilityManager.GetRandomUpgradableAbility();
                NewUpgradablesButtons[i].AddUpgradableToButton(passiveAbility);
                NewUpgradables[i] = passiveAbility;
            }
            else
            {
                Ability ability = abilityGenerator.GenerateAbility(1);
                NewUpgradablesButtons[i].AddUpgradableToButton(ability);
                NewUpgradables[i] = ability;
            }


        }
    }

    public void SetCurrentSelectedUpgradable(Upgradable upgradable)
    {
        if (upgradable.IsPassiveAbility() || (upgradable.IsAbility() && abilityManager.CanAddAbility()))
        {
            this.currentSelectedUpgradable = upgradable;
            confirmButton.SetActive(true);
        }
    }

    public void ClearCurrentSelectedUpgradable()
    {
        this.currentSelectedUpgradable = null;
        confirmButton.SetActive(false);
    }

    // Save selected ability to player's current abilities or upgrade select passive
    public void SaveSelectedUpgradable()
    {
        if (this.currentSelectedUpgradable.IsAbility())
        {
            Ability ability = (Ability)currentSelectedUpgradable;
            abilityManager.AddAbility(ability);
        }
        else if (this.currentSelectedUpgradable.IsPassiveAbility())
        {
            PassiveAbility passiveAbility = (PassiveAbility)currentSelectedUpgradable;
            passiveAbilityManager.UpgradeAbility(passiveAbility);
        }

    }

    private void ClearUnselectedUpgradables()
    {
        foreach (Upgradable upgradable in NewUpgradables)
        {
            if (upgradable.IsAbility() && upgradable != currentSelectedUpgradable)
            {
                Ability ability = (Ability)upgradable;
                Destroy(ability.gameObject);
            }
        }

        NewUpgradables = new Upgradable[numOfUpgradableOptions];
    }
}
