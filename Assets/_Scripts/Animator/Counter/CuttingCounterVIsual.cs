using System.Collections;
using System.Collections.Generic;
using _Scripts.Counter;
using UnityEngine;

public class CuttingCounterVIsual : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private CuttingCounter cuttingCounter;
    private const string CUT = "Cut";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        cuttingCounter.OnCutting += CuttingCounter_OnCutting;
    }

    private void CuttingCounter_OnCutting(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CUT);
    }
}
