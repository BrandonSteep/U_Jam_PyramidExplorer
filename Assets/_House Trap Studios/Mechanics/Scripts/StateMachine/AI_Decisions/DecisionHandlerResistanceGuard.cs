using UnityEngine;

namespace HouseTrap.BadThoughts {
    public class DecisionHandlerResistanceGuard : IDecisionHandler {
        // State References //
        private readonly State stateStay = new State();

        private State currentMoveState = new State_ChaseDirect();
        private readonly State stateChaseDirect = new State_ChaseDirect();
        private readonly State stateStrafeLeft = new StateStrafeLeftRootMotion();
        private readonly State stateStrafeRight = new StateStrafeRightRootMotion();

        private readonly State stateAttackRanged = new State_AttackRanged();
        private readonly State stateAttackMelee = new State_AttackMelee();

        // Misc Variables //
        private bool targetSeen;

        public State MakeAIDecisions(AIStateMachineManager _sm) {
            SetTargetSeen(_sm);
            // Only run the remaining Decision Logic if the AI is aware of its Target

            if (_sm.AwareOfTarget()) {
                // Debug.Log("Aware of Target");

                if (targetSeen) {
                    if (_sm.TryAttack()) {
                        return CheckDistance(_sm.transform.position, _sm.GetTarget().transform.position) >
                               _sm.GetMeleeDistance()
                            ? stateAttackRanged
                            : stateAttackMelee;
                    }
                }

                if (_sm.CanMove()) {
                    if (_sm.ChangeMovement()) {
                        currentMoveState = ChooseMovementType(_sm);
                        // Debug.Log($"Current Move State Changed to {currentMoveState}");
                    }

                    return currentMoveState;
                } else return stateStay;
            } else return stateStay;
        }

        protected virtual State ChooseMovementType(AIStateMachineManager _sm) {
            var randomChoice = UnityEngine.Random.Range(0, 3);
            return randomChoice switch {
                0 => stateStrafeLeft,
                1 => stateStrafeRight,
                _ => stateChaseDirect
            };
        }

        private void SetTargetSeen(AIStateMachineManager _sm) {
            targetSeen = _sm.GetFieldOfView();
            // Debug.Log($"Target Seen = {targetSeen}");
            if (targetSeen && !_sm.AwareOfTarget()) {
                _sm.TargetFound();
            }
        }

        private float CheckDistance(Vector3 _a, Vector3 _b) {
            return Vector3.Distance(_a, _b);
        }
    }
}