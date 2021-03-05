using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

public class setPose : MonoBehaviour, IMixedRealitySourceStateHandler, IMixedRealityHandJointHandler
{
    public Transform LeftH, RightH;
    private void OnEnable()
    {
        CoreServices.InputSystem?.RegisterHandler<IMixedRealitySourceStateHandler>(this);
        CoreServices.InputSystem?.RegisterHandler<IMixedRealityHandJointHandler>(this);
    }

    private void OnDisable()
    {
        CoreServices.InputSystem?.UnregisterHandler<IMixedRealitySourceStateHandler>(this);
        CoreServices.InputSystem?.UnregisterHandler<IMixedRealityHandJointHandler>(this);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnSourceDetected(SourceStateEventData eventData)
    {
    }

    public void OnSourceLost(SourceStateEventData eventData)
    {
        if (eventData?.Controller?.ControllerHandedness == Handedness.Left)
        {
            LeftH.gameObject.SetActive(false);
        }

        if (eventData?.Controller?.ControllerHandedness == Handedness.Right)
        {
            RightH.gameObject.SetActive(false);
        }
            
    }

    public void OnHandJointsUpdated(InputEventData<IDictionary<TrackedHandJoint, MixedRealityPose>> eventData)
    {
        MixedRealityPose palmPose;
        if (eventData.Handedness == Handedness.Left)
        {
            MixedRealityPose leftPose;
            eventData.InputData.TryGetValue(TrackedHandJoint.ThumbProximalJoint, out leftPose);
            LeftH.gameObject.SetActive(true);
            LeftH.position = leftPose.Position;
            LeftH.rotation = leftPose.Rotation* Quaternion.Euler(0,40,0);
            // Debug.Log(eventData.InputSource.SourceName + " " +
            //           eventData.InputData.TryGetValue(TrackedHandJoint.IndexTip, out palmPose));
        }

        if (eventData.Handedness == Handedness.Right)
        {
            MixedRealityPose rightPose;
            eventData.InputData.TryGetValue(TrackedHandJoint.ThumbProximalJoint, out rightPose);
            RightH.gameObject.SetActive(true);
            RightH.position = rightPose.Position;
            RightH.rotation = rightPose.Rotation * Quaternion.Euler(0,40,0);
            
            // Debug.Log(eventData.InputSource.SourceName + " " +
            //           eventData.InputData.TryGetValue(TrackedHandJoint.IndexTip, out palmPose));
        }
    }
}