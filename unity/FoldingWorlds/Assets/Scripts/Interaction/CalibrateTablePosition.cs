using UnityEngine;
using UnityEngine.InputSystem;

public class CalibrateTablePosition : MonoBehaviour
{
    // This is the position relative to the controller
    [SerializeField] private Transform PhysicalReferenceTransform;
    
    [SerializeField] private Transform TabletopRootTransform;

    // TODO: Add some filtering to force the controller to have a specific position for calibration to happen.
    [SerializeField] private InputActionReference calibrateActionReference;
    private InputAction calibrateAction;

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
        var originalParent = TabletopRootTransform.parent;
        // TODO: Get the angle on XZ plane then use an upward Y axis to be sure we're flat.
        TabletopRootTransform.SetParent(PhysicalReferenceTransform);
        TabletopRootTransform.localPosition = Vector3.zero;
        TabletopRootTransform.localRotation = Quaternion.identity;
        TabletopRootTransform.SetParent(originalParent, true);
    }
}
