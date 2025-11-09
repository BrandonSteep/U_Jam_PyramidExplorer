using UnityEngine;

namespace HouseTrap.BadThoughts {
    public class DecisionHandler : IDecisionHandler {
        // State References //
        private State stateStay = new State();

        private State currentMoveState = new State_ChaseDirect();
        private State stateChaseDirect = new State_ChaseDirect();
        private State stateStrafeLeft = new State_StrafeLeft();
        private State stateStrafeRight = new State_StrafeRight();

        private State stateAttackRanged = new State_AttackRanged();
        private State stateAttackMelee = new State_AttackMelee();

        // Misc Variables //
        private bool targetSeen;

        public State MakeAIDecisions(AIStateMachineManager sm) {
            SetTargetSeen(sm);
            // Only run the remaining Decision Logic if the AI is aware of its Target

            if (sm.AwareOfTarget()) {
                // Debug.Log("Aware of Target");

                if (targetSeen) {
                    if (sm.TryAttack()) {
                        if (CheckDistance(sm.transform.position, sm.GetTarget().transform.position) >
                            sm.GetMeleeDistance()) {
                            return stateAttackRanged;
                        } else return stateAttackMelee;
                    }
                }

                if (sm.ChangeMovement()) {
                    currentMoveState = ChooseMovementType(sm);
                    // Debug.Log($"Current Move State Changed to {currentMoveState}");
                }

                return currentMoveState;
            } else return stateStay;
        }

        protected virtual State ChooseMovementType(AIStateMachineManager sm) {
            int randomChoice = UnityEngine.Random.Range(0, 3);
            if (randomChoice == 0) {
                return stateStrafeLeft;
            } else if (randomChoice == 1) {
                return stateStrafeRight;
            } else {
                return stateChaseDirect;
            }
        }

        private void SetTargetSeen(AIStateMachineManager sm) {
            targetSeen = sm.GetFieldOfView();
            // Debug.Log($"Target Seen = {targetSeen}");
            if (targetSeen && !sm.AwareOfTarget()) {
                sm.TargetFound();
            }
        }

        private float CheckDistance(Vector3 a, Vector3 b) {
            return Vector3.Distance(a, b);
        }
    }
}