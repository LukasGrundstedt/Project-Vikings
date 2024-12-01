using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Soldier : MonoBehaviour
{
    [SerializeField] private Sprite portrait;
    public Sprite Portrait { get => portrait; set => portrait = value; }


    [SerializeField] private int maxHp = 100;
    [SerializeField] private int hp = 100;

    [SerializeField] private int attack = 10;
    [SerializeField] private int dmg = 10;
    [SerializeField] private float attackSpeed = 1f;

    [SerializeField] private int defense = 10;
    [SerializeField] private int armor = 10;
    public float AttackCooldown { get; private set; }
    public float AttackRange { get; private set; } = 2.1f;

    //[SerializeField] private float angle;

    [SerializeField] private UnitStatDisplay statDisplay;

    [SerializeField] private Animator swordAnimator;

    [field: SerializeField]
    public MainHand MainHand { get; set; }

    [field: SerializeField]
    public OffHand OffHand { get; set; }

    [field: SerializeField]
    public Soldier Target { get; set; }

    private enum AttackSuccess
    {
        None,
        Free,
        Intercepted
    }

    // Start is called before the first frame update
    void Start()
    {
        statDisplay.DisplayStats((float)hp / maxHp, attack, dmg, defense, armor);
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

        AttackCooldown = 1f / attackSpeed;
    }

    private void ResolveAttack()
    {
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
        int advantage = attack - target.defense;
        advantage = Mathf.Max(1, advantage); //Minimum 1

        int probability = Random.Range(1, 11);

        bool outcome = advantage >= probability;
        Debug.Log($"advantage: {advantage} probability: {probability}");

        return outcome;
    }

    public void TakeDamage(int value)
    {
        hp = Mathf.Clamp(hp - value, 0, hp);
        UpdateUI((float)hp / maxHp);
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

    //private void OnEnable()
    //{
    //    MainHand.HeldWeapon.OnImpact += ResolveAttack;
    //}

    //private void OnDisable()
    //{
    //    MainHand.HeldWeapon.OnImpact -= ResolveAttack;
    //}
}