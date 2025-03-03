using System.Collections;
using UnityEngine;

public class ActiveWeapon : Singlton<ActiveWeapon>
{
    private MonoBehaviour currentActiveWeapon;
    private PlayerControls playerControls;

    private bool isAttaking, attackButonDown = false;

    private float weaponColdaown;

    public bool IsAttaking { get { return isAttaking; } set { isAttaking = value; } }
    public bool AttackButonDown { get { return attackButonDown; } set { attackButonDown = value; } }

    public MonoBehaviour CurrentActiveWeapon { get { return currentActiveWeapon; } private set { currentActiveWeapon = value; } }

    protected override void Awake()
    {
        base.Awake();

        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        if (playerControls != null)
        {
            playerControls.Disable();
        }
    }

    private void Start()
    {
        playerControls.Combat.Atack.started += _ => StartAttacking();
        playerControls.Combat.Atack.canceled += _ => StopAttacking();
        AttackColdown();
    }

    private void Update()
    {
        Attack();
    }

    private void AttackColdown()
    {
        isAttaking = true;
        StartCoroutine(AttackColldownRoutin());
    }

    private IEnumerator AttackColldownRoutin()
    {
        yield return new WaitForSeconds(weaponColdaown);
        isAttaking = false;
    }

    private void StartAttacking()
    {
        attackButonDown = true;
    }

    private void StopAttacking()
    {
        attackButonDown = false;
    }

    private void Attack()
    {
        if (attackButonDown && !isAttaking && currentActiveWeapon)
        {
            (currentActiveWeapon as IWeapon).Attack();
            AttackColdown();
        }
    }

    public void ChangeWeapon(MonoBehaviour weapon)
    {
        CurrentActiveWeapon = weapon;

        if (!weapon)
        {
            return;
        }

        weaponColdaown = (weapon as IWeapon).GetWeaponInfoSo().attackRate;
    }
}
