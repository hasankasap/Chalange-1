using Game.Utils;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace Game
{
    public class PaticleManager : MonoBehaviour
    {
        [SerializeField] ParticleSystem completeParticle;
        #region UNITY_METHODS
        void OnEnable()
        {
            EventManager.StartListening(Events.LEVEL_COMPLETED, OnLevelCompleted);
            EventManager.StartListening(Events.LEVEL_LOADED, OnLevelLoaded);
        }
        void OnDisable()
        {
            EventManager.StopListening(Events.LEVEL_COMPLETED, OnLevelCompleted);
            EventManager.StopListening(Events.LEVEL_LOADED, OnLevelLoaded);
        }
        #endregion

        #region METHODS
        private void ResetParticles()
        {
            if (completeParticle != null)
                completeParticle.Stop();
        }
        #endregion
        #region ACTIONS
        private void OnLevelCompleted(object[] obj)
        {
            if (completeParticle != null)
                completeParticle.Play();
        }
        private void OnLevelLoaded(object[] obj)
        {
            ResetParticles();
        }
        #endregion
    }
}