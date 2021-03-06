﻿namespace UniTween.Data
{
    using DG.Tweening;
    using Sirenix.OdinInspector;
    using System.Collections.Generic;
    using UniTween.Core;
    using UnityEngine;
    using UnityEngine.UI;

    [CreateAssetMenu(menuName = "Tween Data/Canvas/Outline")]
    public class OutlineTween : TweenData
    {

        public OutlineCommand command;

        [HideIf("HideColor")]
        public Color color;
        [ShowIf("HideColor")]
        public float to;

        /// <summary>
        /// Creates and returns a Tween for all components contained inside the UniTweenTarget.
        /// The Tween is configured based on the attribute values of this TweenData file.
        /// </summary>
        /// <param name="uniTweenTarget">Wrapper that contains a List of the component that this TweenData can tween.</param>
        /// <returns></returns>
        public override Tween GetTween(UniTweenObject.UniTweenTarget uniTweenTarget)
        {
            List<Outline> outlines = (List<Outline>)GetComponent(uniTweenTarget);
            Sequence tweens = DOTween.Sequence();
            if (customEase)
            {
                foreach (var t in outlines)
                {
                    if (t != null)
                        tweens.Join(GetTween(t).SetEase(curve));
                }
            }
            else
            {
                foreach (var t in outlines)
                {
                    if (t != null)
                        tweens.Join(GetTween(t).SetEase(ease));
                }
            }
            return tweens;
        }

        /// <summary>
        /// Creates and returns a Tween for the informed component.
        /// The Tween is configured based on the attribute values of this TweenData file.
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public Tween GetTween(Outline outline)
        {
            switch (command)
            {
                case OutlineCommand.Color:
                    return outline.DOColor(color, duration);
                case OutlineCommand.Fade:
                    return outline.DOFade(to, duration);
                default:
                    return null;
            }
        }

        private bool HideColor()
        {
            return command != OutlineCommand.Color;
        }

        public enum OutlineCommand
        {
            Color,
            Fade
        }
    }
}