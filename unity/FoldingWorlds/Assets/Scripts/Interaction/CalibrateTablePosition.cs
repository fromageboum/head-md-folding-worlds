using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CalibrateTablePosition : MonoBehaviour
{
    [SerializeField] private NavMeshAgent playerAgent;
        
    // This is the position relative to the controller
    [SerializeField] private Transform PhysicalReferenceTransform;
    
    [SerializeField] private Transform TabletopRootTransform;
    
    // TODO: Add some filtering to force the controller to have a specific position for calibration to happen.
    [SerializeField] private InputActionReference calibrateActionReference;
    private InputAction calibrateAction;

    [SerializeField] private UnityEvent OnCalibration;

    private void Awake()
    {
        calibrateAction = calibrateActionReference.ToInputAction();
    }

    private void OnEnable()
    {
        calibrateAction.Enable();
        calibrateAction.performed += CalibrateAction_Onperformed;
    }

    private void OnDisable()
    {
        calibrateAction.performed -= CalibrateAction_Onperformed;
        calibrateAction.Disable();
    }

    private void CalibrateAction_Onperformed(InputAction.CallbackContext ctx)
    {
        if (!ControllerPositionIsValid()) return;

        playerAgent.enabled = false;
        
        // We only use the flat forward for direction + Vector3.up as up
        var physicalForward = PhysicalReferenceTransform.forward;
        physicalForward.y = 0f;
        physicalForward.Normalize();
        var calibrationRotation = Quaternion.LookRotation(physicalForward, Vector3.up);
        TabletopRootTransform.position = PhysicalReferenceTransform.position;
        TabletopRootTransform.rotation = calibrationRotation;
        TabletopRootTransform.gameObject.SetActive(true);
        
        // Previous version
        // var originalParent = TabletopRootTransform.parent;
        // TabletopRootTransform.SetParent(PhysicalReferenceTransform);
        // TabletopRootTransform.localPosition = Vector3.zero;
        // TabletopRootTransform.localRotation = Quaternion.identity;
        // TabletopRootTransform.SetParent(originalParent, true);
        
        playerAgent.enabled = true;
        
        OnCalibration?.Invoke();
    }

    private bool ControllerPositionIsValid()
    {
        // a dot product of 0.95 is ~= 18°
        return Vector3.Dot(Vector3.up, PhysicalReferenceTransform.up) > 0.95f;
    }
}
