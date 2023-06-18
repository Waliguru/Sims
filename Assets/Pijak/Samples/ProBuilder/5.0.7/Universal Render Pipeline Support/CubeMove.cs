using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CubeMove : MonoBehaviour
{
    public List<GameObject> targetObjects;
    public GameObject currentTarget;

    private NavMeshAgent agent;
    private Animator animator;

    [Header("Energy")]
    [SerializeField] float InitialEnergyLevel = 0.5f;
    [SerializeField] float BaseEnergyDecayRate = 0.3f;
    [SerializeField] UnityEngine.UI.Slider EnergyDisplay;

    public float idleDistance = 0.5f;
    private int targetCounter = 0;

    public float CurrentEnergy { get; protected set; }
    private void Awake()
    {
        EnergyDisplay.value = CurrentEnergy = InitialEnergyLevel;
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        currentTarget = targetObjects[targetCounter];
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(currentTarget.transform.position);
        if (Vector3.Distance(transform.position, currentTarget.transform.position) > idleDistance)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
            targetCounter++; //TargetCounter +1
            CurrentEnergy = Mathf.Clamp01(CurrentEnergy + 0.1f); 
            if (targetCounter >= targetObjects.Count)
            {
                targetCounter = 0;
            }
            currentTarget = targetObjects[targetCounter];
        }
        CurrentEnergy = Mathf.Clamp01(CurrentEnergy - BaseEnergyDecayRate * Time.deltaTime);
        EnergyDisplay.value = CurrentEnergy;
    }
}
