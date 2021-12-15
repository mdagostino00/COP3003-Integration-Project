using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Patterns
{
    public class State<T>
    {
        protected FiniteStateMachine<T> m_fsm;

        // name of state
        public string Name { get; set; }
        // ID of state
        public T ID { get; private set; }

        public State()
        {
            //Debug.Log("I think something went wrong in the parameterless State() constructor.");
        }

        public State(FiniteStateMachine<T> fsm)
        {
            m_fsm = fsm;
        }

        public State(T id)
        {
            ID = id;
        }

        public State(T id, string name) : this(id)
        {
            Name = name;
        }

        // delegates are basically C# function pointers from C++
        // a delegate is a reference type variable that holds the reference to a method.
        public delegate void DelegateNoArg();

        public DelegateNoArg OnEnter;
        public DelegateNoArg OnExit;
        public DelegateNoArg OnUpdate;
        public DelegateNoArg OnFixedUpdate;

        public State(T id,
            DelegateNoArg onEnter,
            DelegateNoArg onExit = null,
            DelegateNoArg onUpdate = null,
            DelegateNoArg onFixedUpdate = null) : this(id)
        {
            OnEnter = onEnter;
            OnExit = onExit;
            OnUpdate = onUpdate;
            OnFixedUpdate = onFixedUpdate;
        }
        public State(T id,
            string name,
            DelegateNoArg onEnter,
            DelegateNoArg onExit = null,
            DelegateNoArg onUpdate = null,
            DelegateNoArg onFixedUpdate = null) : this(id, name)
        {
            OnEnter = onEnter;
            OnExit = onExit;
            OnUpdate = onUpdate;
            OnFixedUpdate = onFixedUpdate;
        }

        public virtual void Enter()
        {
            OnEnter?.Invoke();
        }
        public virtual void Exit()
        {
            OnExit?.Invoke();
        }
        public virtual void Update()
        {
            OnUpdate?.Invoke();
        }
        public virtual void FixedUpdate()
        {
            OnFixedUpdate?.Invoke();
        }
    }
}
