using System;
using DI;

namespace AppManagement
{
    [Serializable]
    public class AppBootState : AppState
    {        
        protected override void OnEnter()
        {
            // 
            // Load injection layers etc...
        }

        protected override void OnExit()
        {
            
        }
    }
}