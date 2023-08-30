using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private ContainerCounter container;
    [SerializeField] private const string OPEN_CLOSE = "OpenClose";
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        container.OnPlayerGrabbedObject += Container_OnPlayerGrabbedObject;
    }

    private void Container_OnPlayerGrabbedObject(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
