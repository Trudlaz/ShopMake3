using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Test_NavMesh : TestBase
{
    public NavMeshAgent agent;
    Transform target;
    TestInputActions inputActions;

    private void Awake()
    {
        target = transform.GetChild(0);
        inputActions = new TestInputActions();
    }

    private void OnEnable()
    {
        inputActions.Test.Enable();
        inputActions.Test.LClick.performed += OnLClick;
        inputActions.Test.RClick.performed += OnRClick;
    }


    private void OnDisable()
    {
        inputActions.Test.RClick.performed -= OnRClick;
        inputActions.Test.LClick.performed -= OnLClick;
        inputActions.Test.Disable();
    }

    private void OnLClick(InputAction.CallbackContext _)
    {
        Vector2 screen = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(screen);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            agent.SetDestination(hitInfo.point);
        }
    }


    private void OnRClick(InputAction.CallbackContext _)
    {
        Vector2 screen = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(screen);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            //agent.Warp(hitInfo.point);
            target.position = hitInfo.point;
            Factory.Instance.GetNoise(10.0f, target, target.position);
        }
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        OnA();
        Invoke("OnB", 0.5f);
    }

    private void OnA()
    {
        target.position = new Vector3(0, 0, 5);
        Factory.Instance.GetNoise(10.0f, target, target.position);
    }
    private void OnB()
    {
        target.position = new Vector3(0, 0, -5);
        Factory.Instance.GetNoise(10.0f, target, target.position);
    }
}
