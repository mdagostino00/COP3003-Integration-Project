// Elijah Nieves
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    // Slime "is an" Enemy. Slime has a subtyping relationship with Enemy.
    // Rather than inheriting just to reuse the implementation and functions of its superclasses, Slime exists as a 'type' of Enemy.
    // All subtypes of Enemy exist with the assumption that they can safely use any Enemy method, and will almost always be required to do so.
    // Every function that can be invoked on Enemy can also be invoked on Slime

    public class SlimeFSMState_Idle : EnemyFSMState_Idle
    {
        public SlimeFSMState_Idle(Enemy enemy) : base(enemy)
        {
            _id = EnemyFSMStateType.IDLE;
        }

        public override void Update()
        {
            base.Update();

            thisEnemy.timer += Time.deltaTime;          // each frame, add how much time has passed.

            if (thisEnemy.timer >= thisEnemy.moveDelay)     // and they have been delayed long enough
            {
                thisEnemy.timer = 0.0f;           // reset the timer
                thisEnemy.enemyFSM.SetCurrentState(EnemyFSMStateType.MOVEMENT);        // set them to start moving
                thisEnemy.isMoving = true;
            }
        }
    }

    public class SlimeFSMState_Movement : EnemyFSMState_Movement
    {
        public SlimeFSMState_Movement(Enemy enemy) : base(enemy)
        {
            _id = EnemyFSMStateType.MOVEMENT;
        }

        public override void Update()
        {
            base.Update();

            thisEnemy.timer += Time.deltaTime;        // each frame, add how much time has passed.

            if (thisEnemy.timer >= thisEnemy.moveLength)    // and if they have been moving for long enough
            {
                thisEnemy.timer = 0.0f;           // reset the timer
                thisEnemy.enemyFSM.SetCurrentState(EnemyFSMStateType.IDLE);        // set them to stop moving
                thisEnemy.isMoving = false;
            }
        }
    }


    protected override void MakeFSMDictionary()
    {
        // add all the enemy states to the FSM dictionary
        enemyFSM.Add(new SlimeFSMState_Idle(this));
        enemyFSM.Add(new SlimeFSMState_Movement(this));

        // set the state to idle by default
        enemyFSM.SetCurrentState(EnemyFSMStateType.IDLE);
    }

    // Use Update() for non-physics (timer)
    protected override void Update()
    {
        if (isMoving)
            enemyFSM.Update();             // only let the slime change directions while it is getting ready to jump
        else
            base.Update();                 // this update includes the targetPlayer function
    }
}
