using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

//[ExecuteInEditMode]
public class Soldier : Entity
{
    [SerializeField] private SoldierFaction factionID;
    //[SerializeField] private float angle;

    [SerializeField] private Animator swordAnimator;

    [SerializeField] private LayerMask mask;

    [SerializeField] protected int maxHp;
    [SerializeField] protected int hp;
    [SerializeField] protected int atk;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected int dmg;
    [SerializeField] protected int def;
    [SerializeField] protected int armor;
    public float AttackCooldown { get; private set; }
    public float AttackRange { get; private set; } = 2.1f;

    public Action OnAttacked;


    [field: SerializeField]
    public MainHand MainHand { get; set; }

    [field: SerializeField]
    public OffHand OffHand { get; set; }

    [field: SerializeField]
    public Soldier Target { get; set; }

    [field: SerializeField]
    public NavMeshAgent EntityAgent { get; set; }

    [SerializeField] private Healthbar healthbar;

    private enum AttackSuccess
    {
        None,
        Free,
        Intercepted
    }

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        
        Setup();
    }

    protected virtual void Setup()
    {
        EntityAgent.stoppingDistance = 0.1f;
        UpdateHealthBars();
    }

    // Update is called once per frame
    protected void Update()
    {
        DebugLines();

        if (AttackCooldown <= 0f) return;
        AttackCooldown = Mathf.Max(AttackCooldown - Time.deltaTime, 0f);
    }

    public void Attack()
    {
        int animationIndex = Random.Range(1, 4);
        switch (animationIndex)
        {
            case 1:
                swordAnimator.SetTrigger("Stab");
                break;
            case 2:
                swordAnimator.SetTrigger("Slash1");
                break;
            case 3:
                swordAnimator.SetTrigger("Slash2");
                break;
        }

        AttackCooldown = 1f / attackSpeed;
    }

    private void ResolveAttack()
    {
        Target.OnAttacked?.Invoke();

        if (SuccessfulAttack())
        {
            Target.TakeDamage(dmg);
        }
    }

    private bool SuccessfulAttack()
    {
        DetermineAttackSuccess(out AttackSuccess attackSuccess);

        switch (attackSuccess)
        {
            case AttackSuccess.Free:
                return true;

            case AttackSuccess.Intercepted:
                return TryBreakDefense(Target);

            case AttackSuccess.None:
                return false;

            default: 
                return false;
        }
    }

    private void DetermineAttackSuccess(out AttackSuccess attackSuccess)
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, AttackRange + 1f, mask);
        RaycastHit hitInfo = hits[0];

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.transform.root.GetComponent<Soldier>().factionID != factionID)
            {
                hitInfo = hits[i];
                break;
            }
            else
            {
                continue;
            }
        }

        int layer = hitInfo.collider.gameObject.layer;

        switch ((Layer)layer)
        {
            case Layer.Attackable:
                attackSuccess = AttackSuccess.Free;
                break;

            case Layer.Shield:
                attackSuccess = AttackSuccess.Intercepted;
                break;

            default:
                attackSuccess = AttackSuccess.None;
                break;
        }
    }

    private bool TryBreakDefense(Soldier target)
    {
        int advantage = atk - target.def;
        advantage = Mathf.Max(1, advantage); //Minimum 1

        int probability = Random.Range(1, 11);

        bool outcome = advantage >= probability;
        Debug.Log($"advantage: {advantage} probability: {probability}");

        return outcome;
    }

    public void TakeDamage(int value)
    {
        hp = Mathf.Clamp(hp - value, 0, hp);

        UpdateHealthBars();
    }

    protected void UpdateHealthBars()
    {
        float hpBarValue = (float)hp / maxHp;

        OnDamageTaken?.Invoke(hpBarValue);
        healthbar.UpdateHealthBar(hpBarValue);
    }

    private void DebugLines()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward, Color.green);
        Debug.DrawLine(OffHand.transform.position, OffHand.transform.position + OffHand.transform.forward, Color.blue);
        
        if (Target == null) return;

        Vector3 targetObjectPosition = Target.OffHand.transform.position/* + Target.OffHand.transform.forward*/;
        targetObjectPosition.y = 0f;
        Vector3 ownPosition = new(transform.position.x, 0f, transform.position.z);

        //Target Line
        Debug.DrawLine(transform.position, Target.transform.position, Color.red);

        //Angle Line
        Debug.DrawLine(ownPosition, targetObjectPosition, Color.yellow);
        //angle = (Vector3.Angle(ownPosition - targetObjectPosition, transform.forward));
    }

    public override float DisplayableHp()
    {
        return (float)hp / maxHp;
    }

    public override object[] DisplayableStats()
    {
        return new object[]
        {
            atk, dmg, def, armor, attackSpeed, AttackRange
        };
    }

    private void OnEnable()
    {
        MainHand.HeldWeapon.OnImpact += ResolveAttack;
    }

    private void OnDisable()
    {
        MainHand.HeldWeapon.OnImpact -= ResolveAttack;
    }
}