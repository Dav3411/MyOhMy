using UnityEngine;
using System.Collections;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

// Change the material when certain poses are made with the Myo armband.
// Vibrate the Myo armband when a fist pose is made.
public class BehaviourByPose : MonoBehaviour
{
    // Myo game object to connect with.
    // This object must have a ThalmicMyo script attached.s
    public GameObject myo = null;


    // The pose from the last update. This is used to determine if the pose has changed
    // so that actions are only performed upon making them rather than every frame during
    // which they are active.
    private Pose _lastPose = Pose.Unknown;
    public GameObject bulletObject;
    public Transform bulletStartPosition;
    bool shootFlag;

    // Update is called once per frame.
    void Update ()
    {
        // Access the ThalmicMyo component attached to the Myo game object.
        ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();

        // Check if the pose has changed since last update.
        // The ThalmicMyo component of a Myo game object has a pose property that is set to the
        // currently detected pose (e.g. Pose.Fist for the user making a fist). If no pose is currently
        // detected, pose will be set to Pose.Rest. If pose detection is unavailable, e.g. because Myo
        // is not on a user's arm, pose will be set to Pose.Unknown.
        if (thalmicMyo.pose != _lastPose) {
            _lastPose = thalmicMyo.pose;

            // Vibrate the Myo armband when a fist is made.
            if (thalmicMyo.pose == Pose.Fist) {
                thalmicMyo.Vibrate (VibrationType.Short);
                //tendUnlockAndNotifyUserAction (thalmicMyo);
                shootFlag = true;
                if (null != bulletStartPosition)
                {
                    Vector3 vecBulletPos = bulletStartPosition.position;
                    vecBulletPos += (transform.rotation * Vector3.forward);
                    Instantiate(bulletObject, vecBulletPos, transform.rotation);

                }
                else
                    shootFlag = false;

                // Change material when wave in, wave out or double tap poses are made.
            }
            // 안쪽
            else if (thalmicMyo.pose == Pose.WaveIn) {
            //    ExtendUnlockAndNotifyUserAction (thalmicMyo);
            }

            //바깥쪽
            else if (thalmicMyo.pose == Pose.WaveOut) {
                //GetComponent<Renderer>().material = waveOutMaterial;
            //    ExtendUnlockAndNotifyUserAction (thalmicMyo);
                
            }

            //더블탭
            else if (thalmicMyo.pose == Pose.DoubleTap) {
                //ExtendUnlockAndNotifyUserAction (thalmicMyo);
            }
        }
    }

    // Extend the unlock if ThalmcHub's locking policy is standard, and notifies the given myo that a user action was
    // recognized.
    void ExtendUnlockAndNotifyUserAction (ThalmicMyo myo)
    {
        ThalmicHub hub = ThalmicHub.instance;

        if (hub.lockingPolicy == LockingPolicy.Standard) {
            myo.Unlock (UnlockType.Timed);
        }

        myo.NotifyUserAction ();
    }
}
