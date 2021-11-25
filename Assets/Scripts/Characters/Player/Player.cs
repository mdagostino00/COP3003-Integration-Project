using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Patterns;  // finite state machine


public enum PlayerFSMStateType
{
    MOVEMENT = 0,
    ATTACK,
    DEFEND,
    SKILL,
    TAKE_DAMAGE,  // not used, as no animation for this state
    DEAD,
}

public class PlayerFSMState : State<int>
{
    // we will keep the ID for state for convenience
    // this id represents the key
    public PlayerFSMStateType ID { get { return _id; } }

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

        // call player's move method
        //_player.playerMovement.Move();

        /*
        if (input.value)
        {
            PlayerFSMState_ATTACK attackState = (PlayerFSMState_ATTACK)_player.playerFSM.GetState(PlayerFSMStateType.ATTACK);
            attackState.AttackId = 0;
            _player.playerFSM.SetCurrentState(PlayerFSMStateType.ATTACK);
        }

        /*
        if (Input.GetButton("Fire2"))
        {
            PlayerFSMState_ATTACK attackState = (PlayerFSMState_ATTACK)_player.playerFSM.GetState(PlayerFSMStateType.ATTACK);
            attackState.AttackId = 1;
            _player.playerFSM.SetCurrentState(PlayerFSMStateType.ATTACK);
        }
        if (Input.GetButton("Fire3"))
        {
            PlayerFSMState_ATTACK attackState = (PlayerFSMState_ATTACK)_player.playerFSM.GetState(PlayerFSMStateType.ATTACK);
            attackState.AttackId = 2;
            _player.playerFSM.SetCurrentState(PlayerFSMStateType.ATTACK);
        }
        if (Input.GetButton("Crouch"))
        {
            _player.playerFSM.SetCurrentState(PlayerFSMStateType.CROUCH);
        }
        */

        
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        _player.playerMovement.Move();
        Debug.Log("In movement function");
    }
}

public class PlayerFSMState_ATTACK : PlayerFSMState
{
    public PlayerFSMState_ATTACK(Player player) : base(player)
    {
        _id = PlayerFSMStateType.ATTACK;
    }

    public override void Enter() {
        Debug.Log("PlayerFSMState_ATTACK");
        _player.anim.SetBool("attack", true);
    }
    public override void Exit() {
        _player.anim.SetBool("attack", false);
    }
    public override void Update() { 
        
    }
    public override void FixedUpdate() { }
}

public class PlayerFSMState_SKILL : PlayerFSMState
{
    public PlayerFSMState_SKILL(Player player) : base(player)
    {
        _id = PlayerFSMStateType.SKILL;
    }

    public override void Enter() { }
    public override void Exit() { }
    public override void Update() { }
    public override void FixedUpdate() { }
}

public class PlayerFSMState_DEFEND : PlayerFSMState
{
    public PlayerFSMState_DEFEND(Player player) : base(player)
    {
        _id = PlayerFSMStateType.DEFEND;
    }

    public override void Enter() { }
    public override void Exit() { }
    public override void Update() { }
    public override void FixedUpdate() { }
}

public class PlayerFSMState_TAKE_DAMAGE : PlayerFSMState
{
    public PlayerFSMState_TAKE_DAMAGE(Player player) : base(player)
    {
        _id = PlayerFSMStateType.TAKE_DAMAGE;
    }

    public override void Enter() { }
    public override void Exit() { }
    public override void Update() { }
    public override void FixedUpdate() { }
}

public class PlayerFSMState_DEAD : PlayerFSMState
{
    public PlayerFSMState_DEAD(Player player) : base(player)
    {
        _id = PlayerFSMStateType.DEAD;
    }

    public override void Enter()
    {
        Debug.Log("Player dead");
        _player.anim.SetTrigger("Die");
    }
    public override void Exit() { }
    public override void Update() { }
    public override void FixedUpdate() { }
}

public class PlayerFSM : FiniteStateMachine<int>
{
    public PlayerFSM() : base()
    {

    }

    public void Add (PlayerFSMState state)
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

public class PlayerMovement : Entity
{


    protected override void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        RotateTowardDirection();
        Movement();
    }

    protected void OnMove(InputValue value)
    {
        // get the player's input as a float vector
        movement = value.Get<Vector2>();
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

public class Player : Entity
{

    public PlayerMovement playerMovement;
    public PlayerFSM playerFSM = new PlayerFSM();
    public bool attackPressed;

    // State is called before the first frame update
    protected override void Awake()  // Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //create the FSM
        playerFSM = new PlayerFSM();

        playerFSM.Add(new PlayerFSMState_MOVEMENT(this));
        playerFSM.Add(new PlayerFSMState_ATTACK(this));
        playerFSM.Add(new PlayerFSMState_SKILL(this));
        playerFSM.Add(new PlayerFSMState_DEFEND(this));
        playerFSM.Add(new PlayerFSMState_TAKE_DAMAGE(this));
        playerFSM.Add(new PlayerFSMState_DEAD(this));

        playerFSM.SetCurrentState(PlayerFSMStateType.MOVEMENT);
    }

    // Update is called once per frame
    void Update()
    {
       playerFSM.Update();
    }

    /*
    void FixedUpdate()
    {
        playerFSM.FixedUpdate();
    }
    */
    
    protected override void FixedUpdate()
    {
        Move();
    }
    

    void Move()
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

    /*
    public abstract void attack_button();
    public abstract void defense_button();
    public abstract void item_use();
    public abstract void skill_button();
    */

}
