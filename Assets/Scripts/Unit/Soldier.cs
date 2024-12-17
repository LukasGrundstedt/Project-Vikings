using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

//[ExecuteInEditMode]
public class Soldier : Entity
{
    private ClassPreset classPreset;
    [field: SerializeField]
    public ClassPreset ClassPreset { get; private set; }

    [field: SerializeField] 
    public SoldierFaction FactionID { get; private set; }

    //[SerializeField] private float angle;

    private Animator mainHandAnimator;

    [SerializeField] private LayerMask mask;

    [SerializeField] private int maxHp;
    [SerializeField] private int hp;
    [SerializeField] private int atk;
    [SerializeField] private float attackSpeed;
    [SerializeField] private int dmg;
    [SerializeField] private int def;
    [SerializeField] private int armor;
    public float AttackCooldown { get; private set; }
    public float AttackRange { get; private set; } = 2.1f;

    [SerializeField] private AggroTrigger aggroTrigger;

    public Action<Soldier> OnAttacked;

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

    public void ApplyPreset(ClassPreset classPreset)
    {
        this.classPreset = classPreset;

        Portrait = classPreset.Portrait;
        maxHp = classPreset.MaxHp;
        hp = classPreset.Hp;
        atk = classPreset.Atk;
        dmg = classPreset.Dmg;
        def = classPreset.Def;
        armor = classPreset.Armor;
        attackSpeed = classPreset.AttackSpeed;
        AttackRange = classPreset.AttackRange;

#if UNITY_EDITOR
        if (MainHand.transform.childCount == 0)
        {
            PrefabUtility.InstantiatePrefab(classPreset.MainHand, MainHand.transform);
        }
        if (OffHand.transform.childCount == 0)
        {
            PrefabUtility.InstantiatePrefab(classPreset.OffHand, OffHand.transform);
        }
#else
        if (MainHand.transform.childCount == 0)
        {
             Instantiate(classPreset.MainHand, MainHand.transform);
        }
        if (OffHand.transform.childCount == 0)
        {
            Instantiate(classPreset.OffHand, OffHand.transform);
        }
#endif
    }

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        
        Setup();
    }

    protected virtual void Setup()
    {
        aggroTrigger.SoldierFaction = FactionID;
        aggroTrigger.OnTriggered += AddTarget;
        EntityAgent.stoppingDistance = 0.1f;
        mainHandAnimator = MainHand.HeldWeapon.WeaponAnimator;
        MainHand.HeldWeapon.OnImpact += ResolveAttack;
        UpdateHealthBars();

        OnAttacked += SetEnemy;
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
                mainHandAnimator.SetTrigger("Stab");
                break;
            case 2:
                mainHandAnimator.SetTrigger("Slash1");
                break;
            case 3:
                mainHandAnimator.SetTrigger("Slash2");
                break;
        }

        AttackCooldown = 1f / attackSpeed;
    }

    private void ResolveAttack()
    {
        Target.OnAttacked?.Invoke(this);

        if (SuccessfulAttack())
        {
            Target.TakeDamage(dmg);
        }
    }

    private void SetEnemy(Soldier target)
    {
        GetComponent<BehaviourStateMachine>().SetAction(ActionType.Attack, target.gameObject);
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

    protected virtual void Die()
    {
        GetComponent<Animation>().Play();
    }

    private void DetermineAttackSuccess(out AttackSuccess attackSuccess)
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, AttackRange + 1f, mask);
        RaycastHit hitInfo = hits[0];

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.transform.root.GetComponent<Soldier>().FactionID != FactionID)
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
        Debug.Log(attackSuccess);
    }

    private bool TryBreakDefense(Soldier target)
    {
        int advantage = atk - target.def;
        advantage = Mathf.Max(1, advantage); //Minimum 1

        int probability = Random.Range(1, 11);

        bool outcome = advantage >= probability;
        Debug.Log($"Attaker: {gameObject.name}, advantage roll: {advantage}, advantage requiered: {probability}");

        return outcome;
    }

    public void TakeDamage(int value)
    {
        hp = Mathf.Clamp(hp - value, 0, hp);

        UpdateHealthBars();

        if (hp == 0) Die();
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

    private void AddTarget(GameObject target)
    {
        GetComponent<BehaviourStateMachine>().SetAction(ActionType.Attack, target);
    }

    private void OnEnable()
    {
        classPreset = ClassPreset;
    }

    private void OnValidate()
    {
        if (!ClassPreset)
        {
            classPreset = null;
            return;
        }
        if (ClassPreset != classPreset)
        {
            ApplyPreset(ClassPreset);
        }
    }

    private void OnDisable()
    {
        MainHand.HeldWeapon.OnImpact -= ResolveAttack;
        OnAttacked -= SetEnemy;
    }
}