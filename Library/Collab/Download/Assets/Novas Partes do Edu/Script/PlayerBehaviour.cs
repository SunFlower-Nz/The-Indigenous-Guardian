using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum TypeCharacter
{
    Warrior = 0,
    Wizard = 1,
    Archer = 2,
}

public enum PlayerStates
{
    Movement,
   Dead,

}



public class PlayerBehaviour : CharacterBase
{
    private TypeCharacter type;

    private AnimationController animationController;

    //StateMachine
    private PlayerStates currentState = PlayerStates.Movement;
    //

    //Movimentation
    private float speed = 3.0f;
    private float speedSideWalk;
    public float speedRun;
    public float runStaminaCost;
    public float speedWalk;
    public float speedRotation;
    public float rotateSpeed = 3.0f;
    public float speedDodge;
    public float dodgeStaminaCost;
    public float timeRecoverDodge;
    private float currentTimeRecoverDodge;
    private CharacterController controller;
    public Transform focusCamera;
    private float horizontal;
    private float vertical;
    private bool inDodge;
    private float currentStamina;
    private float maxStamina;
    public float staminaRecover;
    //

    //Attack
    private int currentAttack = 0;
    public int totalAttackAnimations;
    private float rangeAttack;
    //

    //UI
    public UIController UI;
    //
    // modificações do edu
    public ItemBase goItem;
    public  GameObject mao;
    protected void Start()
    {

        PlayerStatsController.SetTypeCharacter(TypeCharacter.Warrior);
        currentLevel = PlayerStatsController.GetCurrentLevel();
        type = PlayerStatsController.GetTypeCharacter();

        basicStats = PlayerStatsController.intance.GetBasicStats(type);

        animationController = GetComponent<AnimationController>();
        speed = speedWalk;

        controller = GetComponent<CharacterController>();

        currentAttack = basicStats.baseAttack;

        base.Start();

        maxStamina = basicStats.baseStamina * basicStats.agillity;
        currentStamina = maxStamina;
        currentAttackRate = attackRate;
    }

    private void LookAt()
    {
        Vector3 positionCamera = new Vector3(focusCamera.position.x, transform.position.y, focusCamera.position.z);
        Quaternion newRotation = Quaternion.LookRotation(positionCamera - transform.position);

        if (Mathf.Abs(horizontal) > 0 || vertical > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * speedRotation);
        }
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.T))
        {
            currentLife -= UnityEngine.Random.Range(5,5);
        }

        UI.SetLife(basicStats.startLife, currentLife);
        UI.SetStamina(maxStamina, currentStamina);

        

        switch (currentState)
        {
            case PlayerStates.Movement:
                {
                    horizontal = (Input.GetAxis("Horizontal"));
                    vertical = (Input.GetAxis("Vertical"));
                    

                    if (!inDodge)
                    {
                        if (Input.GetKey(KeyCode.LeftShift) && vertical != 0 && currentStamina > 0)
                        {
                            speed = speedRun;
                            currentStamina -= runStaminaCost;
                            animationController.PlayAnimation(AnimationStates.RUN);
                        }
                        else
                        {
                            speed = speedWalk;
                            if (vertical != 0)
                                animationController.PlayAnimation(AnimationStates.WALK);
                            else if (horizontal != 0)
                            {
                                animationController.PlayAnimation(AnimationStates.SIDE_WALK);
                            }
                            else
                                animationController.PlayAnimation(AnimationStates.IDLE);
                        }
                    }
                    else
                    {
                        currentTimeRecoverDodge += Time.deltaTime;
                        if(currentTimeRecoverDodge > timeRecoverDodge)
                        {
                            currentTimeRecoverDodge = 0;
                            inDodge = false;
                        }
                    }

                    if (Input.GetKey(KeyCode.Space) && horizontal != 0 && !inDodge && currentStamina >= dodgeStaminaCost)
                    {
                        animationController.animator.SetTrigger("dodge");
                        inDodge = true;
                        speed = speedDodge;
                        horizontal = horizontal > 0 ? 1 : -1;
                        currentStamina -= dodgeStaminaCost;
                    }

                    currentStamina += maxStamina * Time.deltaTime;
                    currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

                    if (currentStamina < 0) currentStamina = 0;

                    animationController.animator.SetFloat("Horizontal", horizontal);

                    Vector3 cameraForward = Vector3.Scale(focusCamera.forward, new Vector3(1, 0, 1)).normalized;
                    Vector3 moveDirection = vertical * cameraForward + horizontal * focusCamera.right;

                    moveDirection.y = Physics.gravity.y;
                    LookAt();
                    

                    //Attack 
                    if (Input.GetButton("Fire1") && !inDodge)
                    {
                        Attack();
                    }

                    currentAttackRate += Time.deltaTime;

                    controller.Move(moveDirection * speed * Time.deltaTime);
                }
                break;

        }

    }

    private void Attack()
    {
        if (currentAttackRate >= attackRate)
        {
            currentAttackRate = 0;
            animationController.CallAttackAnimation(currentAttack);
            currentAttack++;

            if (currentAttack > totalAttackAnimations-1)
            {
                currentAttack = 0;
            }

            Vector3 currentPositionPlayer = transform.position;
            currentPositionPlayer.y += controller.height / 2;

            Ray rayAttack = new Ray(transform.position, transform.forward);

            RaycastHit hitInfo = new RaycastHit();

            rangeAttack = basicStats.baseRange;

            if (Physics.Raycast(rayAttack, out hitInfo, rangeAttack))
            {
                
                if (hitInfo.collider.GetComponent<DestructiveBase>() != null)
                {
                    if (hitInfo.collider != GetComponent<Collider>())
                    {
                        hitInfo.collider.GetComponent<DestructiveBase>().ApplyDamage(currentAttack);
                    }
                }
            }

        }

    }

    public override void OnApplyDamage()
    {
        animationController.animator.SetTrigger("hit");
    }

    public override void OnDestroyed()
    {
        animationController.animator.SetTrigger("die");
        currentState = PlayerStates.Dead;
    }
}
