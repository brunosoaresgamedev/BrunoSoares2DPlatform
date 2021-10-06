using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.Enemies.FSM.States
{
    [CreateAssetMenu(fileName ="IdleState",menuName ="Bruno2DActionPlat/States/Idle",order =1)]
    public class IdleState : AbstractFSMState
    {
        public override bool EnterState()
        {
            base.EnterState();
            Debug.Log("Entered idle state");
            return true;

        }

        public override void UpdateState()
        {
            Debug.Log("Updating Idle State");
        }

        public override bool ExitState()
        {
            base.ExitState();
            Debug.Log("Exiting idle state");
            return true;
        }
    }
}