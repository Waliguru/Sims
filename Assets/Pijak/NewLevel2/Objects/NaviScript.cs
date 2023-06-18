using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaviScript : MonoBehaviour
{
    [Header("Energy")]
    [SerializeField] float InitialEnergyLevel = 0.5f;
    [SerializeField] float BaseEnergyDecayRate = 0.3f;
    [SerializeField] UnityEngine.UI.Slider EnergyDisplay;

    public float CurrentEnergy { get; protected set; }

    private void Awake()
    {
        EnergyDisplay.value = CurrentEnergy = InitialEnergyLevel;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CurrentEnergy = Mathf.Clamp01(CurrentEnergy - BaseEnergyDecayRate * Time.deltaTime);
        EnergyDisplay.value = CurrentEnergy;
    }
}
