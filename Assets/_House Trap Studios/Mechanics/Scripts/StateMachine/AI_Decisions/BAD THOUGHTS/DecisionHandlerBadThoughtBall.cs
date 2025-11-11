using UnityEngine;

namespace HouseTrap.BadThoughts {
    public class DecisionHandlerBadThoughtBall : IDecisionHandler {
        // State References //
        private readonly State stateStay = new State();

        private State currentMoveState = new State_ChaseDirect();
        private readonly State stateChaseDirect = new State_ChaseDirect();
        private readonly State stateStrafeLeft = new State_StrafeLeft();
        private readonly State stateStrafeRight = new State_StrafeRight();

        // Misc Variables //
        private bool targetSeen;

        public State MakeAIDecisions(AIStateMachineManager _sm) {
            SetTargetSeen(_sm);
            // Debug.Log($"Running State & Change Movement = {sm.ChangeMovement()} & Aware of Target = {sm.AwareOfTarget()}");

            if (_sm.ChangeMovement()) {
                // Debug.Log("Changing Movement Pattern");
                currentMoveState = ChooseMovementType(_sm);
            }

            return !_sm.AwareOfTarget() ? stateStay : currentMoveState;
        }

        public void Stagger(AIStateMachineManager _sm) {
            throw new NotImplementedException();
        }

        protected virtual State ChooseMovementType(AIStateMachineManager _sm) {
            var randomChoice = UnityEngine.Random.Range(0, 15);
            switch (randomChoice) {
                case <= 4 when CheckDistance(_sm.transform.position, _sm.GetTarget().transform.position) <
                               _sm.GetMeleeDistance():
                    Debug.Log("Chasing down to kill Player");
                    return stateChaseDirect;
                case <= 4:
                    Debug.Log("Move Left");
                    return stateStrafeLeft;
                case <= 7 when CheckDistance(_sm.transform.position, _sm.GetTarget().transform.position) <
                               _sm.GetMeleeDistance():
                    Debug.Log("Chasing down to kill Player");
                    return stateChaseDirect;
                case <= 7:
                    Debug.Log("Move Right");
                    return stateStrafeRight;
                default:
                    Debug.Log("Move to Player");
                    return stateChaseDirect;
            }
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