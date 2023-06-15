using UnityEngine;

public class tipconstraint : MonoBehaviour
{
    public Transform rootBone;
    public Transform tipBone;
    public int chainLength = 3;
    public Transform target;

    private Transform[] bones;
    private Vector3[] bonePositions;
    private float totalLength;

    private void Start()
    {
        bones = new Transform[chainLength + 1];
        bonePositions = new Vector3[chainLength + 1];
        totalLength = 0f;

        Transform currentBone = tipBone;
        for (int i = chainLength; i >= 0; i--)
        {
            bones[i] = currentBone;
            bonePositions[i] = currentBone.position;
            totalLength += Vector3.Distance(currentBone.position, currentBone.parent.position);
            currentBone = currentBone.parent;
        }
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            // Calculate IK chain
            Vector3 targetPosition = target.position;
            bonePositions[0] = targetPosition;

            for (int i = 0; i < chainLength; i++)
            {
                Vector3 currentBonePosition = bonePositions[i];
                Vector3 nextBonePosition = bonePositions[i + 1];
                float boneLength = Vector3.Distance(currentBonePosition, nextBonePosition);
                float blend = Mathf.Clamp01((totalLength - boneLength) / totalLength);
                bonePositions[i + 1] = Vector3.Lerp(nextBonePosition, currentBonePosition, blend);
            }

            // Apply IK chain positions to bones
            for (int i = chainLength - 1; i >= 0; i--)
            {
                Quaternion targetRotation = Quaternion.FromToRotation(bones[i + 1].position - bones[i].position, bonePositions[i + 1] - bonePositions[i]) * bones[i].rotation;
                bones[i].rotation = targetRotation;
            }
        }
    }
}
