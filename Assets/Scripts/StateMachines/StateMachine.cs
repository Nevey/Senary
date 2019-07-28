using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace StateMachines
{
    public abstract class StateMachine
    {
        private readonly List<Transition> transitions = new List<Transition>();
        private readonly Dictionary<Type, State> states = new Dictionary<Type, State>();

        private State initialState;
        private State currentState;

        private bool HasTransition<TFrom, TTo>()
            where TFrom : State
            where TTo : State
        {
            return transitions.Any(transition => transition.From == typeof(TFrom) && transition.To == typeof(TTo));
        }

        private Transition GetTransition<TTo>() where TTo : State
        {
            return transitions.FirstOrDefault(t => t.From == currentState.GetType() && t.To == typeof(TTo));
        }

        private bool HasState<T>() where T : State
        {
            return states.ContainsKey(typeof(T));
        }

        private State GetState<T>() where T : State
        {
            if (HasState<T>())
            {
                return states[typeof(T)];
            }

            throw Log.Exception($"State {typeof(T).Name} was not created!");
        }

        private void SetNewState(State newState)
        {
            if (currentState != null)
            {
                currentState.Exit();
            }

            currentState = newState;
            currentState.Enter();
        }

        protected void AddTransition<TFrom, TTo>()
            where TFrom : State, new()
            where TTo : State, new()
        {
            Type fromType = typeof(TFrom);
            Type toType = typeof(TTo);
            
            if (HasTransition<TFrom, TTo>())
            {
                Log.Warn($"Transition from state {fromType.Name} to " +
                         $"state {toType.Name} already exists, not creating a new one...");
                return;
            }

            if (!HasState<TFrom>())
            {
                states[fromType] = new TFrom();
            }

            if (!HasState<TTo>())
            {
                states[toType] = new TTo();
            }
            
            transitions.Add(new Transition<TFrom, TTo>());
        }

        protected void SetInitialState<T>() where T : State, new()
        {
            if (!HasState<T>())
            {
                states[typeof(T)] = new T();
            }

            initialState = states[typeof(T)];
        }

        public void Start()
        {
            if (initialState == null)
            {
                throw Log.Exception($"Cannot start StateMachine {GetType().Name}. Initial State is not set!");
            }

            foreach (var state in states)
            {
                state.Value.Initialize();
            }

            SetNewState(initialState);
        }

        public void Stop()
        {
            foreach (var state in states)
            {
                state.Value.Cleanup();
            }
            
            states.Clear();
            transitions.Clear();
        }
        
        public void To<T>() where T : State
        {
            Transition transition = GetTransition<T>();

            if (transition == null)
            {
                throw Log.Exception($"No Transition available from State {currentState.GetType().Name} to " +
                                    $"State {typeof(T).Name}");
            }

            SetNewState(GetState<T>());
        }

    }
}