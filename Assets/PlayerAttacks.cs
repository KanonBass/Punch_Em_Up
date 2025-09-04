using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerAttacks : MonoBehaviour
{
    [Serializable] public struct Attack
    {
        float cooldown;
        GameObject attackPrefab;
    }

    InputAction Punch;
    InputAction Kick;
    InputAction Grab;

    private float punch;

    private float currentTimer = 0;
    [SerializeField] private float punchCooldown = .5f;
    [SerializeField] private float kickCooldown = .5f;
    [SerializeField] private float grabCooldown = .5f;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Punch = InputSystem.actions.FindAction("Punch");
        Kick = InputSystem.actions.FindAction("Kick");
        Grab = InputSystem.actions.FindAction("Grab");
    }

    private void OnPunch(InputValue value)
    {
        punch = value.Get<float>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTimer += Time.deltaTime;
    }
}
