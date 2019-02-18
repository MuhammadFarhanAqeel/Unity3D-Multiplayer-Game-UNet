using UnityEngine;
using UnityEngine.Networking;



public class GunPositionSync : NetworkBehaviour
{


    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform handMount;
    [SerializeField] Transform gunPivot;
    [SerializeField] float threshold = 10f;
    [SerializeField] float smoothing = 5f;
    [SerializeField] Transform rightHandHold;
    [SerializeField] Transform leftHandHold;

    [SyncVar] float pitch;

    Vector3 lastOffset;
    float lastSyncedPitch;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        if (isLocalPlayer)
            gunPivot.parent = cameraTransform;
        else
            lastOffset = handMount.position - transform.position;

    }



    void Update()
    {

        if (isLocalPlayer)
        {
            pitch = cameraTransform.localRotation.eulerAngles.x;
            if (Mathf.Abs(lastSyncedPitch - pitch) >= threshold)
            {
                //Cmd
                CmdUpdatePitch(pitch);

                lastSyncedPitch = pitch;
            }
        }
        else
        {
            Quaternion newRotation = Quaternion.Euler(pitch, 0, 0);
            Vector3 currentOffset = handMount.position - transform.position;

            gunPivot.localPosition += currentOffset - lastOffset;
            lastOffset = currentOffset;

            gunPivot.localRotation = Quaternion.Lerp(gunPivot.localRotation,
                newRotation, Time.deltaTime * smoothing);

        }

    }


    [Command]
    void CmdUpdatePitch(float newPitch)
    {

        pitch = newPitch;

    }


    void OnAnimatorIK()
    {

        if (!anim)
            return;


        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
        anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandHold.position);
        anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandHold.rotation);


        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
        anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandHold.position);
        anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandHold.rotation);

    }
}