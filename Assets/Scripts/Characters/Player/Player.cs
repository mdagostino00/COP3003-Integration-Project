///<summary>
/// <c>Michael D'Agostino</c>
/// This file contains the code for the Player object.
/// It includes the Player class, along with the Player's States that are 
/// necessary for the state machine.
/// </summary>

///</summary>
/* 
 *  LO5:
 *  Inheritance refers to when a class reuses code from another class. You can create a subclass
 *  from a base class by inheriting from that base class. The functions in the base class can then be reused or
 *  changed as the subclass needs it.
 *  
 *  Subtyping refers to the compatibility of classes and interfaces. A class is a subtype of another class
 *  if the subtyping class can invoke every function that the other class can invoke.
 * 
 *  In C++/#, inheritance implies subtyping because a subclass will, by default, have the same
 *  functions as the base class. However, subtyping doesn't necessarily mean inheritance.
 *  
 *  The state machine below is an example of inheritance and subtyping.
 *  Each hard-coded state, such as PlayerFSMState_MOVEMENT, inherits from the 
 *  PlayerFSMState, which in turn inherits from a generic state. Since they use the exact
 *  same functions, these hard-coded states are also subtypes of the PlayerFSMState. 
 *  
 *  These states are stored in a dictionary aggregate in the FiniteStateMachine class, which we can then swap
 *  between by calling a function that returns the correct state when we pass it the right ID based
 *  on the input of the player.
 *  
 *  The idea behind this is that we can change the Update() and FixedUpdate() functions 
 *  called in the Player class based on the state of the player character. For example, when we 
 *  press the attack button, we can change the player character's state from free movement
 *  to its attack state, since the player can only be in one state at a time. We might want the player to
 *  be able to move during the movement state, but not able to move during an attack.
 *  
 *  https://www.youtube.com/watch?v=dQw4w9WgXcQs
 *  https://www.youtube.com/watch?v=m5WsmlEOFiA
 *  https://youtu.be/Vt8aZDPzRjI
 *  https://faramira.com/implementing-a-finite-state-machine-using-c-in-unity-part-1/
 */
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Patterns;  // finite state machine

/// <summary>
/// <c>PlayerFSMStateType</c> Enumeration list for easy references to all of the available states.
/// </summary>
// number all the states
public enum PlayerFSMStateType
{
    MOVEMENT = 0,
    ATTACK,
    DEFEND,
    SKILL,
    TAKE_DAMAGE,
    DEAD,
}

/// <summary>
/// <c>PlayerFSMState</c> A template state with functions needed for the Player's FSM states.
/// /// <see cref="State{T}">
/// </summary>
public class PlayerFSMState : State<int>
{
    // we will keep the ID for state for convenience
    // this id represents the key
    public new PlayerFSMStateType ID { get { return _id; } }
    protected Player _player = null;
    protected PlayerFSMStateType _id;

    public PlayerFSMState(FiniteStateMachine<int> fsm, Player player) : base(fsm)
    {
        _player = player;
    }

    // convenience constructor with just Player
    public PlayerFSMState(Player player) : base()
    {
        _player = player;
        m_fsm = _player.playerFSM;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

}

/// <summary>
/// 
/// </summary>
public class PlayerFSMState_MOVEMENT : PlayerFSMState
{
    public PlayerFSMState_MOVEMENT(Player player) : base(player)
    {
        _id = PlayerFSMStateType.MOVEMENT;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        //Debug.Log("In movement function");
        // when we press the attack button, this should switch to the attack state
        // Unity input handling sends messages to functions similar to these
        // this might need to be moved
        if (_player.attackPressed == true)
        {
            _player.playerFSM.SetCurrentState(PlayerFSMStateType.ATTACK);
        }
        else if (_player.defendPressed)
        {
            _player.playerFSM.SetCurrentState(PlayerFSMStateType.DEFEND);

        }
        else if (_player.skillOnePressed)
        {
            _player.playerFSM.SetCurrentState(PlayerFSMStateType.SKILL);
        }
        else if (_player.skillTwoPressed)
        {
            _player.playerFSM.SetCurrentState(PlayerFSMStateType.SKILL);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        _player.Move();
    }
}

/// <summary>
/// 
/// </summary>
public class PlayerFSMState_ATTACK : PlayerFSMState
{
    public PlayerFSMState_ATTACK(Player player) : base(player)
    {
        _id = PlayerFSMStateType.ATTACK;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("PlayerFSMState_ATTACK");
        _player.anim.SetBool("attack", true);

        Vector2 playerPos = _player.transform.position;
        Vector2 playerDirection = _player.transform.forward;
        Quaternion playerRotation = _player.transform.rotation;
        float spawnDistance = 100;

        Vector2 spawnPos = playerPos + (playerDirection * spawnDistance);
        _player.sword.transform.position = spawnPos;
        _player.sword.col.enabled = true;
    }
    public override void Exit()
    {
        base.Exit();
        _player.anim.SetBool("attack", false);

        _player.sword.col.enabled = false;
    }
    public override void Update()
    {
        base.Update();
        //Debug.Log("In attack function");
        if (_player.attackPressed == false)
        {
            _player.anim.SetBool("attack", false);
            _player.playerFSM.SetCurrentState(PlayerFSMStateType.MOVEMENT);
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}

/// <summary>
/// 
/// </summary>
public class PlayerFSMState_SKILL : PlayerFSMState
{
    public PlayerFSMState_SKILL(Player player) : base(player)
    {
        _id = PlayerFSMStateType.SKILL;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("PlayerFSMState_SKILL");
        if (_player.skillOnePressed)
        {
            _player.anim.SetBool("skill_1", true);
        }
        else if (_player.skillTwoPressed)
        {
            _player.anim.SetBool("skill_2", true);
        }

    }
    public override void Exit()
    {
        _player.anim.SetBool("skill_1", false);
        _player.anim.SetBool("skill_2", false);
    }

    public override void Update()
    {
        //Debug.Log("In skill function");
        if ((_player.skillOnePressed == false) && (_player.skillTwoPressed == false))
        {
            _player.anim.SetBool("skill_1", false);
            _player.anim.SetBool("skill_2", false);
            _player.playerFSM.SetCurrentState(PlayerFSMStateType.MOVEMENT);
        }
    }
    public override void FixedUpdate() { }
}

/// <summary>
/// 
/// </summary>
public class PlayerFSMState_DEFEND : PlayerFSMState
{
    public PlayerFSMState_DEFEND(Player player) : base(player)
    {
        _id = PlayerFSMStateType.DEFEND;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("PlayerFSMState_DEFEND");
        _player.anim.SetBool("defend", true);
    }
    public override void Exit()
    {
        _player.anim.SetBool("defend", false);
    }
    public override void Update()
    {
        //Debug.Log("In defend function");
        if (_player.defendPressed == false)
        {
            _player.anim.SetBool("defend", false);
            _player.playerFSM.SetCurrentState(PlayerFSMStateType.MOVEMENT);
        }

    }
    public override void FixedUpdate() { }
}

/// <summary>
/// 
/// </summary>
public class PlayerFSMState_TAKE_DAMAGE : PlayerFSMState
{
    public PlayerFSMState_TAKE_DAMAGE(Player player) : base(player)
    {
        _id = PlayerFSMStateType.TAKE_DAMAGE;
    }

    public override void Enter() {
        _player.HealthReduce(10);
    }
    public override void Exit() { }
    public override void Update()
    {
        Debug.Log("In take_damage function");
    }
    public override void FixedUpdate() {
        _player.playerFSM.SetCurrentState(PlayerFSMStateType.MOVEMENT);
    }
}

/// <summary>
/// 
/// </summary>
public class PlayerFSMState_DEAD : PlayerFSMState
{
    public PlayerFSMState_DEAD(Player player) : base(player)
    {
        _id = PlayerFSMStateType.DEAD;
    }

    public override void Enter()
    {
        Debug.Log("Player dead");
        _player.anim.SetTrigger("die");
        //Destroy(_player);
    }
    public override void Exit() { }
    public override void Update()
    {
        Debug.Log("In dead function");
    }
    public override void FixedUpdate() {
        Debug.Log("Player also dead");
    }
}

/// <summary>
/// 
/// </summary>
// gives the Player class better access to the generic FSM class
public class PlayerFSM : FiniteStateMachine<int>
{
    //calls the base constructor, which makes a dictionary of ints
    public PlayerFSM() : base() { }

    public void Add(PlayerFSMState state)
    {
        m_states.Add((int)state.ID, state);
    }

    public PlayerFSMState GetState(PlayerFSMStateType key)
    {
        return (PlayerFSMState)GetState((int)key);
    }

    public void SetCurrentState(PlayerFSMStateType stateKey)
    {
        State<int> state = m_states[(int)stateKey];
        if (state != null)
        {
            SetCurrentState(state);
        }
    }
}

/// <summary>
/// This class allows for the Player object to interact with the Unity Engine.
/// LO1
/// </summary>
public class Player : Entity
{
    //public PlayerMovement playerMovement;
    public PlayerFSM playerFSM = null;
    public bool attackPressed;
    public bool defendPressed;
    public bool skillOnePressed;
    public bool skillTwoPressed;

    public SwordHitbox sword;

    // Awake is called when the object is created
    protected override void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        CurrentHealth = HealthTotal;

        //create the FSM
        playerFSM = new PlayerFSM();

        //add all the states we have to the fsm dictionary we created
        playerFSM.Add(new PlayerFSMState_MOVEMENT(this));
        playerFSM.Add(new PlayerFSMState_ATTACK(this));
        playerFSM.Add(new PlayerFSMState_SKILL(this));
        playerFSM.Add(new PlayerFSMState_DEFEND(this));
        playerFSM.Add(new PlayerFSMState_TAKE_DAMAGE(this));
        playerFSM.Add(new PlayerFSMState_DEAD(this));

        //set the state to movement by default
        playerFSM.SetCurrentState(PlayerFSMStateType.MOVEMENT);
        Debug.Log(playerFSM.GetCurrentState().ID);
    }

    // call the current state's FixedUpdate()
    // Update() is called once per frame
    // e: ^^this is a lie, it's like 200 times a second, use for input
    protected void Update()
    {
        playerFSM.Update();
    }

    // call the current state's FixedUpdate()
    // e: vv this is like 50 times a second, use for physics
    protected override void FixedUpdate()
    {
        playerFSM.FixedUpdate();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Player Hit");
            if (this.CurrentHealth > 0)
            {
                playerFSM.SetCurrentState(PlayerFSMStateType.TAKE_DAMAGE);
            }
            if (this.CurrentHealth == 0) {
                playerFSM.SetCurrentState(PlayerFSMStateType.DEAD);
            }
            //this.HealthReduce(10);
            //Destroy(gameObject);
        }
    }

    public void Move()
    {
        RotateTowardDirection();
        Movement();
    }

    void OnMove(InputValue value)
    {
        // get the player's input as a float vector
        movement = value.Get<Vector2>();
    }

    void OnAttack(InputValue value)
    {
        attackPressed = value.isPressed;
    }

    void OnDefend(InputValue value)
    {
        defendPressed = value.isPressed;
    }

    void OnSkill_1(InputValue value)
    {
        skillOnePressed = value.isPressed;
    }

    void OnSkill_2(InputValue value)
    {
        skillTwoPressed = value.isPressed;
    }

    protected override void RotateTowardDirection()
    {
        //turn off walking
        if (movement != Vector2.zero) // if we have player movement input
        {
            // rotate sprite to face direction of movement
            transform.rotation =
                Quaternion.LookRotation(Vector3.forward, movement);
            // turn on walking animation
            anim.SetBool("walking", true);
        }
        else
        {
            //turn off walking
            anim.SetBool("walking", false);
        }
    }
}
