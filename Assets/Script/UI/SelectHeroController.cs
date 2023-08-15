using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectHeroController : MonoBehaviour
{
    [SerializeField] List<RuntimeAnimatorController> controller = new List<RuntimeAnimatorController>();
    [SerializeField] int index = 0;
    [SerializeField] Animator animator;
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        animator.runtimeAnimatorController = controller[0];
    }
    public void Prev()
    {
        if (index <= 0)
            return;
        animator.runtimeAnimatorController = controller[--index];
    }
    public void Next()
    {
        if (index >= controller.Count - 1)
            return;
        animator.runtimeAnimatorController = controller[++index];
    }
}
