using DG.Tweening;
using Game.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class GridUIController : MonoBehaviour
    {
        [SerializeField] Image minValueWarning;
        [SerializeField] Text scoreText;
        Tween warningTween;
        #region UNITY_METHODS
        void OnEnable()
        {
            EventManager.StartListening(Events.POP_MIN_VALUE_WARNING, PopMinValueWarning);
            EventManager.StartListening(Events.UPDATE_SCORE_UI, UpdateScoreUI);
        }
        void OnDisable()
        {
            EventManager.StopListening(Events.POP_MIN_VALUE_WARNING, PopMinValueWarning);
            EventManager.StopListening(Events.UPDATE_SCORE_UI, UpdateScoreUI);
        }
        #endregion

        #region METHODS
        private void PopMinValueWarning(object[] obj)
        {
            if (warningTween != null)
            {
                minValueWarning.gameObject.SetActive(false);
                warningTween.Rewind();
            }
            minValueWarning.transform.localScale = Vector3.zero;
            minValueWarning.gameObject.SetActive(true);
            minValueWarning.transform.DOScale(Vector3.one, .2f).SetEase(Ease.OutBounce).OnComplete(()=> 
            {
                minValueWarning.transform.DOScale(Vector3.zero, .2f).SetDelay(1f).SetEase(Ease.OutBounce);
            });
        }
        private void UpdateScoreUI(object[] obj)
        {
            scoreText.text= (string)obj[0];
        }
        #endregion
    }
}