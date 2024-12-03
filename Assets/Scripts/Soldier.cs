using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//[ExecuteInEditMode]
public class Soldier : Entity
{
    [SerializeField] private Sprite portrait;
    public Sprite Portrait { get => portrait; set => portrait = value; }

    public float AttackCooldown { get; private set; }
    public float AttackRange { get; private set; } = 2.1f;

    private bool unitSoldier;

    //[SerializeField] private float angle;

    [SerializeField] private UnitStatDisplay statDisplay;

    [SerializeField] private Animator swordAnimator;

    [field: SerializeField]
    public MainHand MainHand { get; set; }

    [field: SerializeField]
    public OffHand OffHand { get; set; }

    [field: SerializeField]
    public Soldier Target { get; set; }

    [field: SerializeField]
    public NavMeshAgent EntityAgent { get; set; }

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

        unitSoldier = TryGetComponent<Unit>(out _);


        if (unitSoldier) return;

        statDisplay.DisplayStats((float)stats.Hp / stats.MaxHp, stats.Atk, stats.Dmg, stats.Def, stats.Armor);
    }

    // Update is called once per frame
    void Update()
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
        swordAnimator.SetTrigger("Slash2");

        AttackCooldown = 1f / stats.AttackSpeed;
    }

    private void ResolveAttack()
    {
        if (SuccessfulAttack())
        {
            Target.TakeDamage(stats.Dmg);
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
        Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, AttackRange + 1f);
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
        int advantage = stats.Atk - target.stats.Def;
        advantage = Mathf.Max(1, advantage); //Minimum 1

        int probability = Random.Range(1, 11);

        bool outcome = advantage >= probability;
        Debug.Log($"advantage: {advantage} probability: {probability}");

        return outcome;
    }

    public void TakeDamage(int value)
    {
        stats.Hp = Mathf.Clamp(stats.Hp - value, 0, stats.Hp);
        UpdateUI((float)stats.Hp / stats.MaxHp);
    }

    private void UpdateUI(float hpBarValue, params object[] stats)
    {
        statDisplay.DisplayStats(hpBarValue, stats);
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

    private void OnEnable()
    {
        MainHand.HeldWeapon.OnImpact += ResolveAttack;
    }

    private void OnDisable()
    {
        MainHand.HeldWeapon.OnImpact -= ResolveAttack;
    }
}