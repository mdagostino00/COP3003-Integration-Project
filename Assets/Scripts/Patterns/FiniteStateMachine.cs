/// <summary>
/// Michael D'Agostino
/// FiniteStateMachine.cs
/// 
/// This file contains a generic FiniteStateMachine class meant to be used with the State{T} class.
/// Finite State Machine
///  - a set of states
///  - at any given time, the FSM can only be in one of these states
///  - we can control the logic of each state
/// we will use a dictionary to hold the possible states
/// 
/// Bugs:
/// No known bugs.
/// 
/// Sources:
/// https://faramira.com/implementing-a-finite-state-machine-using-c-in-unity-part-1/
/// https://faramira.com/generic-finite-state-machine-using-csharp/
/// </summary>

using System.Collections;
using System.Collections.Generic;

/// <summary>
/// <c>Patterns</c>namespace containing the FiniteStateMachine{T} class and the State{T} class
/// </summary>
namespace Patterns
{
    /// <summary>
    /// <c>FiniteStateMachine</c>Generic class to support a finite state machine control flow algorithm.
    /// 
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FiniteStateMachine<T>
    {
        // dictionary for all possible states
        protected Dictionary<T, State<T>> m_states;
        // the current state
        protected State<T> m_currentState;

        public FiniteStateMachine()
        {
            m_states = new Dictionary<T, State<T>>();
        }

        public void Add(State<T> state)
        {
            m_states.Add(state.ID, state);
        }

        public void Add(T stateID, State<T> state)
        {
            m_states.Add(stateID, state);
        }

        public State<T> GetState(T stateID)
        {
            if (m_states.ContainsKey(stateID))
            {
                return m_states[stateID];
            }
            return null;
        }

        public void SetCurrentState(T stateID)
        {
            State<T> state = m_states[stateID];
            SetCurrentState(state);
        }

        public State<T> GetCurrentState()
        {
            return m_currentState;
        }

        public void SetCurrentState(State<T> state)
        {
            //if no state change, don't set anything
            if (m_currentState == state)
            {
                return;
            }

            // if we have left the previous state, trigger Exit()
            if (m_currentState != null)
            {
                m_currentState.Exit();
            }

            // set the current state to the actual current state
            m_currentState = state;

            // now trigger the Enter() function
            if (m_currentState != null)
            {
                m_currentState.Enter();
            }
        }

        public void Update()
        {
            if (m_currentState != null)
            {
                m_currentState.Update();
            }
        }

        public void FixedUpdate()
        {
            if (m_currentState != null)
            {
                m_currentState.FixedUpdate();
            }
        }
    }
}
