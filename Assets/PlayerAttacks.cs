using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerAttacks : MonoBehaviour
{
    [Serializable] public struct AttackInfo
    {
        public float cooldown;
        public GameObject attackPrefab;
    }

    [SerializeField] AttackInfo PunchInfo;
    [SerializeField] AttackInfo KickInfo;
    [SerializeField] AttackInfo FillerInfo;

    InputAction Punch;
    InputAction Kick;
    InputAction Grab;

    private float currentTimer = 0;
    private AttackInfo CurrentAttack;

    float punch = 0;
    float kick = 0;

    private int direction = 1;
    [SerializeField] private float punchOffset;
    [SerializeField] private float kickOffset;
    public UnityEvent<int> AttackEvent;

    public UnityEvent GameOver;

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

    private void OnKick(InputValue value)
    {
        kick = value.Get<float>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTimer += Time.deltaTime;

        if (currentTimer >= CurrentAttack.cooldown)
        {
            if (Punch.WasPerformedThisFrame())
            {
                Instantiate(PunchInfo.attackPrefab, transform.position + new Vector3(punchOffset*direction,0,0), quaternion.identity);
                CurrentAttack = PunchInfo;
                currentTimer = 0;
                AttackEvent.Invoke(1);
            }
            else if (Kick.WasPerformedThisFrame())
            {
                Instantiate(KickInfo.attackPrefab, transform.position + new Vector3(kickOffset * direction, 0, 0), quaternion.identity);
                CurrentAttack = KickInfo;
                currentTimer = 0;
                AttackEvent.Invoke(2);
            }
        }
    }

    public void UpdateDirection(int dir)
    {
        direction = dir;
    }


}
