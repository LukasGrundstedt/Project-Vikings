using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Soldier : MonoBehaviour
{
    [SerializeField] private int maxHp = 100;
    [SerializeField] private int hp = 100;
    [SerializeField] private int dmg = 10;
    [SerializeField] private float attackSpeed = 1f;
    public float AttackCooldown { get; private set; }
    public float AttackRange { get; private set; } = 2f;

    [SerializeField] private float angle;

    [SerializeField] private Healthbar healthBar;

    [field: SerializeField]
    public GameObject MainHand { get; set; }

    [field: SerializeField]
    public GameObject OffHand { get; set; }

    [field: SerializeField]
    public GameObject Target { get; set; }

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
        if (SuccessfulAttack())
        {
            target.TakeDamage(dmg);
        }
        AttackCooldown = 1f / attackSpeed;

        PlayAttackSound();
    }

    private bool SuccessfulAttack()
    {
        return true;
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

        Vector3 targetObjectPosition = new(Target.GetComponent<Soldier>().OffHand.transform.position.x, 0f, Target.GetComponent<Soldier>().OffHand.transform.position.z);
        Vector3 ownPosition = new(transform.position.x, 0f, transform.position.z);

        //Target Line
        Debug.DrawLine(transform.position, Target.transform.position, Color.red);

        //Angle Line
        Debug.DrawLine(ownPosition, targetObjectPosition, Color.yellow);
        angle = Mathf.Abs(Vector3.Angle(ownPosition, targetObjectPosition));
    }
}