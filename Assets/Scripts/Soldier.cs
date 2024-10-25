using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Soldier : MonoBehaviour
{
    [SerializeField] private int maxHp = 100;
    [SerializeField] private int hp = 100;

    [SerializeField] private int attack = 10;
    [SerializeField] private int dmg = 10;
    [SerializeField] private float attackSpeed = 1f;

    [SerializeField] private int defense = 10;
    [SerializeField] private int armor = 10;
    public float AttackCooldown { get; private set; }
    public float AttackRange { get; private set; } = 2.1f;

    [SerializeField] private float angle;

    [SerializeField] private Healthbar healthBar;

    [SerializeField] private Animator swordAnimator;

    [field: SerializeField]
    public GameObject MainHand { get; set; }

    [field: SerializeField]
    public GameObject OffHand { get; set; }

    [field: SerializeField]
    public GameObject Target { get; set; }

    private enum AttackSuccess
    {
        None,
        Free,
        Intercepted
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DebugLines();

        if (AttackCooldown <= 0f) return;
        AttackCooldown = Mathf.Max(AttackCooldown - Time.deltaTime, 0f);
    }

    public void Attack(Soldier target)
    {
        int random = Random.Range(1, 4);

        if (random == 1) swordAnimator.SetTrigger("Stab");
        if (random == 2) swordAnimator.SetTrigger("Slash1");
        if (random == 3) swordAnimator.SetTrigger("Slash2");


        if (SuccessfulAttack())
        {
            target.TakeDamage(dmg);
        }
        AttackCooldown = 1f / attackSpeed;

        PlayAttackSound();


    }

    private bool SuccessfulAttack()
    {
        DetermineAttackSuccess(out AttackSuccess attackSuccess);

        switch (attackSuccess)
        {
            case AttackSuccess.Free:
                return true;

            case AttackSuccess.Intercepted:
                return TryBreakDefense(Target.GetComponent<Soldier>());

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
        Debug.Log($"{advantage} + {probability}");

        return outcome;
    }

    public void TakeDamage(int value)
    {
        hp = Mathf.Clamp(hp - value, 0, hp);
        healthBar.UpdateHealthBar(hp, maxHp);
    }

    private void PlayAttackSound()
    {

    }

    private void DebugLines()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward, Color.green);
        Debug.DrawLine(OffHand.transform.position, OffHand.transform.position + OffHand.transform.forward, Color.blue);
        
        if (Target == null) return;

        Vector3 targetObjectPosition = Target.GetComponent<Soldier>().OffHand.transform.position/* + Target.GetComponent<Soldier>().OffHand.transform.forward*/;
        targetObjectPosition.y = 0f;
        Vector3 ownPosition = new(transform.position.x, 0f, transform.position.z);

        //Target Line
        Debug.DrawLine(transform.position, Target.transform.position, Color.red);

        //Angle Line
        Debug.DrawLine(ownPosition, targetObjectPosition, Color.yellow);
        angle = (Vector3.Angle(ownPosition - targetObjectPosition, transform.forward));
    }
}