using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AnimatorController : MonoBehaviour
{
    [SerializeField]
    private Animator animator = null;

    [SerializeField]
    private AnimationClip defaultState = null;

    private RuntimeAnimatorController animatorController;
    private AnimationClip[] animationClips;

    private AnimationClip currentClip;

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (animator != null)
        {
            animatorController = animator.runtimeAnimatorController;
            animationClips = animatorController.animationClips;
        }
    }

    public void StartIdleAnimation()
    {
        StartTransitionTo(defaultState.name);
    }

    public void  StartTransitionTo(string animationName)
    {
        if (animationName != null && animationClips != null)
        {
            IEnumerable<AnimationClip> animationClipQuery =
            from clip in animationClips
            where clip.name == animationName
            select clip;

            List<AnimationClip> clips = animationClipQuery.ToList();

            if (clips.Count > 0)
            {
                AnimationClip playedClip = clips[0];

                if (playedClip != currentClip)
                {
                    currentClip = playedClip;
                    animator.SetTrigger(playedClip.name);
                }
            }
        }
    }
}
