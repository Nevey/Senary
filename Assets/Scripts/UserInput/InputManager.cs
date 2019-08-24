using System.Collections.Generic;
using UnityEngine;
using Utilities;
using MonoBehaviour = DI.MonoBehaviour;

namespace UserInput
{
    [DefaultExecutionOrder(-100)]
    public abstract class InputManager : MonoBehaviour
    {
        private readonly List<ActionSet> actionSets = new List<ActionSet>();

        protected override void Awake()
        {
            DontDestroyOnLoad(this);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            Unbind();
        }

        protected virtual void Update()
        {
            for (int i = 0; i < actionSets.Count; i++)
            {
                ActionSet actionSet = actionSets[i];
                
                if (!actionSet.IsBound)
                {
                    continue;
                }
                
                actionSet.Update();
            }
        }

        protected void AddActionSet(ActionSet actionSet)
        {
            if (actionSets.Contains(actionSet))
            {
                Log.Warn($"ActionSet {actionSet.GetType().Name} was already registered...");
                return;
            }
            
            actionSets.Add(actionSet);
        }

        public void Bind()
        {
            for (int i = 0; i < actionSets.Count; i++)
            {
                actionSets[i].Bind();
            }
        }

        public void Unbind()
        {
            for (int i = 0; i < actionSets.Count; i++)
            {
                actionSets[i].Unbind();
            }
        }
    }
}