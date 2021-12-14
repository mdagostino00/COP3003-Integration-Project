// Elijah Nieves
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns;     // used to find the FSM scripts


public class Enemy : Entity
{
    // Enemy "is an" Entity. Enemy has a subtyping relationship with Entity.
    // Rather than inheriting just to reuse the implementation and functions of its superclasses, Enemy exists as a 'type' of Entity.
    // All subtypes of Entity exist with the assumption that they can safely use any Entity method, and will almost always be required to do so.
    // Every function that can be invoked on Entity can also be invoked on Enemy

    // implementing a state machine for Enemy using Michael's FSM.
    // his PlayerFSM as a template as well as the resources he provided

    // variables for all the state classes to use
    public bool isMoving = false;
    public float timer = 0.0f;
    [SerializeField]
    public float moveDelay = 0.0f;       //how many seconds the enemy will wait between moving toward the player.
    [SerializeField]
    public float moveLength = 1.0f;       //how many seconds the enemy will attack chase the player before the delay

    // number all the states
    public enum EnemyFSMStateType
    {
        IDLE = 0,
        MOVEMENT,
        ATTACK,
        TAKE_DAMAGE,  // not used yet, as no animation for this state
        DEAD,
    }

    public class EnemyFSMState : State<int>
    {
        // we will keep the ID for state for convenience
        // this id represents the key
        public new EnemyFSMStateType ID { get { return _id; } }

        protected Enemy thisEnemy = null;
        protected EnemyFSMStateType _id;

        public EnemyFSMState(FiniteStateMachine<int> fsm, Enemy enemy) : base(fsm)
        {
            thisEnemy = enemy;
        }

        // convenience constructor with just Enemy
        public EnemyFSMState(Enemy enemy) : base()
        {
            thisEnemy = enemy;
            m_fsm = thisEnemy.enemyFSM;
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

    public class EnemyFSMState_Idle : EnemyFSMState
    {
        public EnemyFSMState_Idle(Enemy enemy) : base(enemy)
        {
            _id = EnemyFSMStateType.IDLE;
        }

        public override void Enter()
        {
            base.Enter();
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

    public class EnemyFSMState_Movement : EnemyFSMState
    {

        public EnemyFSMState_Movement(Enemy enemy) : base(enemy)
        {
            _id = EnemyFSMStateType.MOVEMENT;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            thisEnemy.Movement(thisEnemy.direction);    // move them towards their target
        }
    }

    public class EnemyFSMState_Attack : EnemyFSMState
    {
        public EnemyFSMState_Attack(Enemy enemy) : base(enemy)
        {
            _id = EnemyFSMStateType.ATTACK;
        }

        public override void Enter()
        {
            base.Enter();
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

    public class EnemyFSMState_TakeDamage : EnemyFSMState
    {
        public EnemyFSMState_TakeDamage(Enemy enemy) : base(enemy)
        {
            _id = EnemyFSMStateType.TAKE_DAMAGE;
        }

        public override void Enter()
        {
            base.Enter();
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

    public class EnemyFSMState_Dead : EnemyFSMState
    {
        public EnemyFSMState_Dead(Enemy enemy) : base(enemy)
        {
            _id = EnemyFSMStateType.DEAD;
        }

        public override void Enter()
        {
            base.Enter();
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


    // gives the Enemy class better access to the generic FSM class
    public class EnemyFSM : FiniteStateMachine<int>
    {
        //calls the base constructor, which makes a dictionary of ints
        public EnemyFSM() : base()
        {

        }

        public void Add(EnemyFSMState state)
        {
            m_states.Add((int)state.ID, state);
        }

        public EnemyFSMState GetState(EnemyFSMStateType key)
        {
            return (EnemyFSMState)GetState((int)key);
        }

        public void SetCurrentState(EnemyFSMStateType stateKey)
        {
            State<int> state = m_states[(int)stateKey];
            if (state != null)
            {
                SetCurrentState(state);
            }
        }
    }



    //[SerializeField]
    public Transform target; // we need the player's position

    public EnemyFSM enemyFSM = null;

    protected override void Awake()
    {
        // create the rigidbody and animator components
        base.Awake();

        // create the FSM
        enemyFSM = new EnemyFSM();
        
        // set the target of the enemy to a game object with the "player" tag
        target = GameObject.FindWithTag("Player").transform;

        MakeFSMDictionary();
    }

    protected virtual void MakeFSMDictionary()
    {
        // add all the enemy states to the FSM dictionary
        enemyFSM.Add(new EnemyFSMState_Idle(this));
        enemyFSM.Add(new EnemyFSMState_Movement(this));
        enemyFSM.Add(new EnemyFSMState_Attack(this));
        enemyFSM.Add(new EnemyFSMState_TakeDamage(this));
        enemyFSM.Add(new EnemyFSMState_Dead(this));

        // set the state to idle by default
        enemyFSM.SetCurrentState(EnemyFSMStateType.IDLE);
    }

    // Update() is called every frame
    protected virtual void Update()
    {
        targetPlayer();

        enemyFSM.Update();
    }

    protected void targetPlayer()
    {
        direction = target.position - transform.position; // find direction vector from enemy to player
        RotateTowardDirection(); // rotate enemy sprite to face player
        moveCharacter(ref direction); // normalize the direction vector and set this to the movement vector
    }

    // Use FixedUpdate() for physics
    protected override void FixedUpdate()
    {
        enemyFSM.FixedUpdate();
    }

    public void moveCharacter(ref Vector2 direction)
    {

        direction.Normalize(); // really cool vector normalization function in Unity
        movement = direction; // Vector2D movement is now the normalized vector
    }
}