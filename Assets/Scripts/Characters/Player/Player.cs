///<summary>
/// Michael D'Agostino
/// Player.cs
/// 
/// This file contains the code for the Player object.
/// It includes the Player class, along with the Player's States that are 
/// necessary for the state machine.
/// 
/// Bugs and Features:
/// No known bugs
/// Some states aren't utilized in the game yet.
/// </summary>

///</summary>
/* 
 *  Michael LO5. Explain the relationship between object-oriented inheritance (code-sharing and overriding) 
 *  and subtyping (the idea of a subtype being usable in a context that expects the supertype).
 *  
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
 *  Michael LO4. Include a comment in which you compare and contrast the procedural/functional 
 *  approach and the object-oriented approach
 *  
 *  In procedural programming, we're reading through the program line-by-line in order to make the program work. All
 *  data is usually shared throughout the program, and while it's possible to add OOP-styled encapsulation
 *  into this code, it's not used for it. It's a lot easier to write procedural code, as you typically never need
 *  to worry about sending messages between objects like in OOP.
 *  
 *  In Object-oriented programming, we're structuring our code based on conceptual objects, and tying our code to these objects.
 *  These objects communicate through messages sent between each other, which meant to protect data from being accessed
 *  without communicating with the class. This encapsulation is the main draw of OOP, as it adds an additional layer
 *  of security that the programmer needs to follow.
 *  
 *  Sources:
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
/// Adds Player object specific functionality to a generic State{T} class.
/// 
/// Michael LO2. Use subclassing to design simple class hierarchies that allow code to be reused for distinct subclasses.
/// Each State we code should directly inherit from this PlayerFSMState class.
/// 
/// LO2a. Include comments describing the visibility inheritance model
/// In this class, we define a few fields and functions that each substate should inherit from.
/// The methods are made public so any other class can access them. For example, Unity needs to see the Update(),
/// and FixedUpdate() functions. All of our states need these functions, but they will be overriden in those states.
/// The _player and _id fields are protected because each of our substates needs to be able to have a reference to a Player
/// and the ID in the PlayerFSMStateType enum. However, code outside of those states will not be able to see those functions.
/// There aren't any private members in here because this is supposed to be a template for the states we code.
/// 
/// <remarks>Template State for Player class</remarks>
/// /// <see cref="State{T}">
/// </summary>
public class PlayerFSMState : State<int>
{
    // we will keep the ID for state for convenience
    // this id represents the key
    public new PlayerFSMStateType ID { get { return _id; } }
    protected Player _player = null;
    protected PlayerFSMStateType _id;

    /// <summary>
    /// Constructor taking an FSM object and a Player object, while calling the base State{T} constructor.
    /// 
    /// Michael LO1b. Overload a constructor 
    /// </summary>
    /// <param name="fsm">The Player's FiniteStateMachine object</param>
    /// <param name="player">The player object</param>
    public PlayerFSMState(FiniteStateMachine<int> fsm, Player player) : base(fsm)
    {
        _player = player;
    }


    /// <summary>
    /// Convenience constructor with just Player.
    /// Assigns the player object we passed to our null _player initializer, along with the Player's FSM
    /// 
    /// Michael LO1b. Overload a constructor
    /// 
    /// Michael LO1c. Utilize an initialization list:
    /// C# doesn't support initialization lists, so we can only do assignments.
    /// https://stackoverflow.com/questions/2435175/when-initializing-in-c-sharp-constructors-whats-better-initializer-lists-or-as
    /// </summary>
    /// <param name="player">A player object</param>
    public PlayerFSMState(Player player) : base()
    {
        _player = player;
        m_fsm = _player.playerFSM;
    }

    /// <inheritdoc/>
    /// <summary>
    /// <c>Enter</c>What's called when we enter the state.
    /// </summary>
    public override void Enter()
    {
        base.Enter();
    }

    /// <inheritdoc/>
    /// <summary>
    /// <c>Exit</c>What's called when we leave the state.
    /// </summary>
    public override void Exit()
    {
        base.Exit();
    }

    /// <inheritdoc/>
    /// <summary>
    /// <c>Update</c>Update is called nearly 200 times a second.
    /// </summary>
    public override void Update()
    {
        base.Update();
    }

    /// <inheritdoc/>
    /// <summary>
    /// <c>FixedUpdate</c>FixedUpdate is called exactly 50 times a second on any machine.
    /// </summary>
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

}

/// <summary>
/// The Movement State for the player.
/// </summary>
public class PlayerFSMState_MOVEMENT : PlayerFSMState
{
    /// <summary>
    /// Call the base constructor and assign the ID for Movement
    /// </summary>
    /// <param name="player">the Player Object</param>
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
/// Attack State for Player
/// </summary>
public class PlayerFSMState_ATTACK : PlayerFSMState
{
    /// <summary>
    /// sets ID for the state and calls base contructor
    /// </summary>
    /// <param name="player">The Player Object</param>
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
/// Skill State for player (no function yet)
/// </summary>
public class PlayerFSMState_SKILL : PlayerFSMState
{
    /// <summary>
    /// grabs ID for state and calls base contructor
    /// </summary>
    /// <param name="player">The player object</param>
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
/// The Defend state for player (No function)
/// </summary>
public class PlayerFSMState_DEFEND : PlayerFSMState
{
    /// <summary>
    /// Grabs ID for state
    /// </summary>
    /// <param name="player">the Player object</param>
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
/// the Take_Damage state, where player takes damage
/// </summary>
public class PlayerFSMState_TAKE_DAMAGE : PlayerFSMState
{
    /// <summary>
    /// Grabs ID for state
    /// </summary>
    /// <param name="player">The Player Object</param>
    public PlayerFSMState_TAKE_DAMAGE(Player player) : base(player)
    {
        _id = PlayerFSMStateType.TAKE_DAMAGE;
    }

    public override void Enter() {
        //reduces player health
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
/// The Dead State for player when health reaches 0
/// </summary>
public class PlayerFSMState_DEAD : PlayerFSMState
{
    /// <summary>
    /// ID constructor
    /// </summary>
    /// <param name="player">Player Object</param>
    public PlayerFSMState_DEAD(Player player) : base(player)
    {
        _id = PlayerFSMStateType.DEAD;
    }

    public override void Enter()
    {
        Debug.Log("Player dead");
        _player.anim.SetTrigger("die");

        // Need to use this to change scenes within uniity
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lose");
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
/// Gives the Player class better access to the generic FSM class
/// 
/// Michael LO3. Correctly reason about control flow in a program using dynamic dispatch.
/// 
/// We can control what Update() and FixedUpdate() functions are called in Unity using the Finite State Machine.
/// Based on the desired program logic, we can change an object's state and the methods that are called in those
/// states. The FiniteStateMachine has a dictionary containing each state. By passing the state's reference id
/// into our calling functions, we can determine which state's methods we need to call.
/// 
/// For example, using the Player class, The Player will always start in the Movement state and call Movement's
/// Update and FixedUpdate() functions. This is because we have the Movement state's ID passed into our calling
/// function. Whenever the attack button is pressed, we change the state of the player 
/// to the Attack state by changing the ID called from the dictionary. This will change the Update() and 
/// FixedUpdate() functions that the Player object calls to the functions found in the Attack state. 
/// 
/// https://chodounsky.com/2014/01/29/dynamic-dispatch-in-c-number/
/// </summary>
public class PlayerFSM : FiniteStateMachine<int>
{
    /// <summary>
    /// <c>PlayerFSM</c>
    /// calls the base constructor, which makes a dictionary of ints
    /// </summary>
    public PlayerFSM() : base() { }

    /// <summary>
    /// <c>Add</c>adds the state to the dictionary of states
    /// </summary>
    /// <param name="state">the state we want to add</param>
    public void Add(PlayerFSMState state)
    {
        m_states.Add((int)state.ID, state);
    }

    /// <summary>
    /// <c>GetState</c>Returns the state when given a key in the PlayerFSMStateType enum.
    /// Basically a helper function for the Player object
    /// </summary>
    /// <param name="key">the state listed in the <see cref="PlayerFSMStateType"/></param>
    /// <returns>The state casted to PlayerFSMState</returns>
    public PlayerFSMState GetState(PlayerFSMStateType key)
    {
        return (PlayerFSMState)GetState((int)key);
    }

    /// <summary>
    /// <c>SetCurrentState</c>sets the current state in the PlayerFSM to the desired state in the <see cref="PlayerFSMStateType"/>
    /// </summary>
    /// <param name="stateKey">the key in the <see cref="PlayerFSMStateType"/></param>
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
/// 
/// Michael LO1. Design and implement a class. 
/// 
/// 
/// 
/// </summary>
public class Player : Entity
{
    public PlayerFSM playerFSM = null;

    // Input bools for the player
    public bool attackPressed;
    public bool defendPressed;
    public bool skillOnePressed;
    public bool skillTwoPressed;

    public SwordHitbox sword;

    /// <summary>
    /// <c>Awake</c>Called in Unity when the object is enabled.
    /// This is basically the Unity equivalent of a constructor for objects
    /// that inherit from MonoBehaviour.
    /// </summary>
    protected override void Awake()
    {
        //get the Rigidbody2D and Animator in Unity
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //set the currentHealth to the Total Health
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

    /// <summary>
    /// <c>Update</c>call the current state's Update()
    /// </summary>
    protected void Update()
    {
        playerFSM.Update();
    }

    /// <summary>
    /// <c>FixedUpdate</c>call the current state's FixedUpdate()
    /// </summary>
    protected override void FixedUpdate()
    {
        playerFSM.FixedUpdate();
    }

    /// <summary>
    /// <c>OnCollisionEnter2D</c>When Player's Collision2D overlaps with another Collision2D, trigger this
    /// </summary>
    /// <param name="collision">the Collision2D that we are colliding with</param>
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        // check the Unity tag for the Collision2D object we are touching
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Player Hit");
            if (this.CurrentHealth > 0)
            {
                playerFSM.SetCurrentState(PlayerFSMStateType.TAKE_DAMAGE);
            }
            if (this.CurrentHealth == 0)
            {
                playerFSM.SetCurrentState(PlayerFSMStateType.DEAD);
            }
        }
    }

    /// <summary>
    /// <c>Move</c>Convenience Method to allow for correct player movement
    /// </summary>
    public void Move()
    {
        RotateTowardDirection();
        Movement();
    }

    /// <summary>
    /// <c>OnMove</c>Unity sends messages to this function every frame to check WASD direction
    /// </summary>
    /// <param name="value">Unity's InputValue</param>
    void OnMove(InputValue value)
    {
        // get the player's input as a float vector
        movement = value.Get<Vector2>();
    }

    /// <summary>
    /// <c>OnMove</c>Unity sends messages to this function every frame to check attack button
    /// </summary>
    /// <param name="value">Unity's InputValue</param>
    void OnAttack(InputValue value)
    {
        attackPressed = value.isPressed;
    }

    /// <summary>
    /// <c>OnMove</c>Unity sends messages to this function every frame to check defend button
    /// </summary>
    /// <param name="value">Unity's InputValue</param>
    void OnDefend(InputValue value)
    {
        defendPressed = value.isPressed;
    }

    /// <summary>
    /// <c>OnMove</c>Unity sends messages to this function every frame to check skill button
    /// </summary>
    /// <param name="value">Unity's InputValue</param>
    void OnSkill_1(InputValue value)
    {
        skillOnePressed = value.isPressed;
    }

    /// <summary>
    /// <c>OnMove</c>Unity sends messages to this function every frame to check skill 2 button
    /// </summary>
    /// <param name="value">Unity's InputValue</param>
    void OnSkill_2(InputValue value)
    {
        skillTwoPressed = value.isPressed;
    }

    /// <summary>
    /// <c>RotateTowardDirection</c>Rotate the sprite towards direction being moved, and update the Player's Animator
    /// </summary>
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
