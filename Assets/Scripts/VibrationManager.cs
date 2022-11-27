using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
진동값 참고
320hz full amplitude [255, 255, 255, 255, ...]
160hz full amplitude [255, 0, 255, 0, 255, 0, 255, 0, ...]
320hz half amplitude [127, 127, 127, 127, ...]
160hz half amplitude [127, 0, 127, 0, 127, 0, 127, 0, ...]
Single Sharp tick(320hz) [0, 0, 255, 255, 255, 0, 0][delay x ms][0, 0, 255, 255, 255, 0, 0]
Single Blunt tick(160hz) [0, 255, 0, 255, 0, 255, 0][delay x ms][0, 255, 0, 255, 0, 255, 0]
 */

public class VibrationManager : MonoBehaviour
{
    public static VibrationManager singleton;

    void Awake()
    {
        if(singleton && singleton != this)
        {
            Destroy(this);
        }
        else
        {
            singleton = this;
        }
    }

    public void TriggerVibration(AudioClip vibrationAudio, OVRInput.Controller controller)
    {
        OVRHapticsClip clip = new OVRHapticsClip(vibrationAudio);

        if (controller == OVRInput.Controller.LTouch)
            OVRHaptics.LeftChannel.Preempt(clip);
        else if (controller == OVRInput.Controller.RTouch)
            OVRHaptics.RightChannel.Preempt(clip);
    }


    /// <summary>
    /// 진동 ON
    /// </summary>
    /// <param name="iteration">진동 횟수(웅 1번)</param>
    /// <param name="frequency">진동 주기</param>
    /// <param name="strength">진동 세기 0~255</param>
    /// <param name="controller">진동 대상 컨트롤러</param>
    public void TriggerVibration(int iteration, int frequency, int strength, OVRInput.Controller controller)
    {
        OVRHapticsClip clip = new OVRHapticsClip();

        for (int i = 0; i < iteration; i++)
            clip.WriteSample(i % frequency == 0 ? (byte)strength : (byte)0);

        if (controller == OVRInput.Controller.LTouch)
            OVRHaptics.LeftChannel.Preempt(clip);
        else if (controller == OVRInput.Controller.RTouch)
            OVRHaptics.RightChannel.Preempt(clip);
    }
}
