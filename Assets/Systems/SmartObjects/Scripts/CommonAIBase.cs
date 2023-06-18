using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EStat
{
    Energy,
    Thirst
}

[RequireComponent(typeof(BaseNavigation))]
public class CommonAIBase : MonoBehaviour
{
    [Header("Thirst")]
    [SerializeField] float InitialThirstLevel = 0.5f;
    [SerializeField] float BaseThirstDecayRate = 0.005f;
    [SerializeField] UnityEngine.UI.Slider ThirstDisplay;

    [Header("Energy")]
    [SerializeField] float InitialEnergyLevel = 0.5f;
    [SerializeField] float BaseEnergyDecayRate = 0.005f;
    [SerializeField] UnityEngine.UI.Slider EnergyDisplay;

    protected BaseNavigation Navigation;

    protected BaseInteraction CurrentInteraction = null;
    protected bool StartedPerforming = false;

    public float CurrentThirst { get; protected set; }
    public float CurrentEnergy { get; protected set; }

    protected virtual void Awake()
    {
        ThirstDisplay.value = CurrentThirst = InitialThirstLevel;
        EnergyDisplay.value = CurrentEnergy = InitialEnergyLevel;

        Navigation = GetComponent<BaseNavigation>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (CurrentInteraction != null)
        {
            if (Navigation.IsAtDestination && !StartedPerforming)
            {
                StartedPerforming = true;
                CurrentInteraction.Perform(this, OnInteractionFinished);
            }
        }

        CurrentThirst = Mathf.Clamp01(CurrentThirst - BaseThirstDecayRate * Time.deltaTime);
        ThirstDisplay.value = CurrentThirst;

        CurrentEnergy = Mathf.Clamp01(CurrentEnergy - BaseEnergyDecayRate * Time.deltaTime);
        EnergyDisplay.value = CurrentEnergy;
    }

    protected virtual void OnInteractionFinished(BaseInteraction interaction)
    {
        interaction.UnlockInteraction();
        CurrentInteraction = null;
        Debug.Log($"Finished {interaction.DisplayName}");
    }

    public void UpdateIndividualStat(EStat target, float amount)
    {
        switch(target)
        {
            case EStat.Energy: CurrentEnergy += amount; break;
            case EStat.Thirst:    CurrentThirst += amount; break;
        }
    }    
}
