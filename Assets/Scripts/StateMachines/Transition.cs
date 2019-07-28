using System;

namespace StateMachines
{
    public abstract class Transition
    {
        public abstract Type From { get; }
        public abstract Type To { get; }
    }
    
    public class Transition<TFrom, TTo> : Transition
        where TFrom : State
        where TTo : State
    {
        public override Type From => typeof(TFrom);
        public override Type To => typeof(TTo);
    }
}