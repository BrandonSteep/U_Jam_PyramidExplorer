using System.Collections;
using HouseTrap.BadThoughts;
using UnityEngine;

public class DecisionHandlerRanged : IDecisionHandler {
    // State References //
    private State stateStay = new State();

    private State currentMoveState = new State_ChaseDirect();
    private State stateChaseDirect = new State_ChaseDirect();
    private State stateStrafeLeft = new State_StrafeLeft();
    private State stateStrafeRight = new State_StrafeRight();

    private State stateAttackRanged = new State_AttackRanged();
    private State stateAttackMelee = new State_AttackMelee();

    private State stateStagger = new State_Stagger();
    private bool isStaggered;
    private IEnumerator staggerCoroutine;

    // Misc Variables //
    private bool targetSeen;

    public State MakeAIDecisions(AIStateMachineManager _sm) {
        SetTargetSeen(_sm);
        // Only run the remaining Decision Logic if the AI is aware of its Target
        Debug.Log($"Is Staggered = {isStaggered}");
        if (_sm.AwareOfTarget() && !isStaggered) {
            // Debug.Log("Aware of Target");

            if (targetSeen) {
                if (_sm.TryAttack()) {
                    return CheckDistance(_sm.transform.position, _sm.GetTarget().transform.position) >
                           _sm.GetMeleeDistance()
                        ? stateAttackRanged
                        : stateAttackMelee;
                }
            }

            if (_sm.ChangeMovement()) {
                currentMoveState = ChooseMovementType(_sm);
            }

            return currentMoveState;
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
    
    public void Stagger(AIStateMachineManager _sm) {
        isStaggered = true;
        stateStay = stateStagger;
        _sm.DisableNavigation();
        
        if (staggerCoroutine != null) _sm.StopCoroutine(staggerCoroutine);
        staggerCoroutine = EndStagger(_sm, 1f);
        _sm.StartCoroutine(staggerCoroutine);
    }

    private IEnumerator EndStagger(AIStateMachineManager _sm, float _duration) {
        yield return new WaitForSeconds(_duration);
        isStaggered = false;
        _sm.EnableNavigation();
        stateStay = stateChaseDirect;
    }
    
    private void SetTargetSeen(AIStateMachineManager _sm) {
        targetSeen = _sm.GetFieldOfView();
        // Debug.Log($"Target Seen = {targetSeen}");
        if (targetSeen && !_sm.AwareOfTarget()) {
            _sm.TargetFound();
        }
    }

    private float CheckDistance(Vector3 _a, Vector3 _b) { return Vector3.Distance(_a, _b); }
}