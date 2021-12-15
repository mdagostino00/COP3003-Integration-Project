//Robert McNiven

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Masked_Enemy : Enemy
{
    public class Masked_EnemyFSMState_Idle : EnemyFSMState_Idle
    {
        public Masked_EnemyFSMState_Idle(Enemy enemy) : base(enemy)
        {
            _id = EnemyFSMStateType.IDLE;
        }

        public override void Update()
        {
            base.Update();

            thisEnemy.timer += Time.deltaTime;

            if (thisEnemy.timer >= thisEnemy.MoveDelay)     // and they have been delayed long enough
            {
                thisEnemy.timer = 0.0f;           // reset the timer
                thisEnemy.enemyFSM.SetCurrentState(EnemyFSMStateType.MOVEMENT);        // set them to start moving
                thisEnemy.IsMoving = true;
            }
        }
    }

    public class Masked_EnemyFSMState_Movement : EnemyFSMState_Movement
    {
        public Masked_EnemyFSMState_Movement(Enemy enemy) : base(enemy)
        {
            _id = EnemyFSMStateType.MOVEMENT;
        }

        public override void Update()
        {
            base.Update();

            thisEnemy.timer += Time.deltaTime;

            if (thisEnemy.timer >= thisEnemy.MoveLength)    // and if they have been moving for long enough
            {
                thisEnemy.timer = 0.0f;           // reset the timer
                thisEnemy.enemyFSM.SetCurrentState(EnemyFSMStateType.IDLE);        // set them to stop moving
                thisEnemy.IsMoving = false;
            }
        }
    }

    protected override void MakeFSMDictionary()
    {

        enemyFSM.Add(new Masked_EnemyFSMState_Idle(this));
        enemyFSM.Add(new Masked_EnemyFSMState_Movement(this));
        //enemyFSM.Add(new Masked_EnemyFSMState_Dead(this));


        enemyFSM.SetCurrentState(EnemyFSMStateType.IDLE);
    }

    protected override void Update()
    {
        //targetPlayer();
        //RotateTowardDirection();
        base.Update();
    }
    protected override void RotateTowardDirection()
    {
        // Need to override this so that the Maked_Enemy does not rotate

        //base.RotateTowardDirection();
        //turn off walking
        if (movement != Vector2.zero) 
        {
            transform.rotation =
                Quaternion.AngleAxis(180, Vector3.up);
            
        }
    }
}
