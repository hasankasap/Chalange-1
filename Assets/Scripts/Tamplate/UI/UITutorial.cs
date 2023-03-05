using DG.Tweening;
using Game.Utils;
using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class UITutorial : MonoBehaviour
    {
        [SerializeField, ReadOnly]bool isPressed;
        [SerializeField] GameObject tutorial, animatons;
        [SerializeField] CanvasGroup canvasGroup;
        Tween fadeOutTween;
        #region UNITY_METHODS
        void Start()
        {
            ResetTutorial();
            animatons.SetActive(true);
        }
        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !isPressed)
            {
                isPressed = true;
                if (fadeOutTween != null)
                    fadeOutTween.Kill();
                fadeOutTween = canvasGroup.DOFade(0, .5f)
                .SetEase(Ease.OutCirc)
                .OnComplete(() => {
                    OnTutorialClosed();
                });
            }
        }
        #endregion

        #region METHODS
        public void ResetTutorial()
        {
            if (fadeOutTween != null)
                fadeOutTween.Kill();
            isPressed = false;
            canvasGroup.alpha = 1;
            tutorial.SetActive(true);
        }
        #endregion

        #region ACTIONS
        public void OnTutorialClosed()
        {
            tutorial.SetActive(false);
            EventManager.TriggerEvent(Events.GAME_STARTED, false);
        }
        #endregion
    }
}