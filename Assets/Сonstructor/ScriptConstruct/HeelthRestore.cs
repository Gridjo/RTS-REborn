using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RTSEngine.Attack;
using RTSEngine.Entities;
using RTSEngine.Movement;
using RTSEngine.Event;
using RTSEngine.Logging;
using RTSEngine.UI;
using RTSEngine.Determinism;
using RTSEngine.Health;
using RTSEngine.EntityComponent;

namespace Grid.Restore
{
    public class HeelthRestore : MonoBehaviour
    {
        private float timeSec;
        public int restoreInSec;
        public void Restore()
        {
            gameObject.GetComponent<UnitHealth>().CurrHealth += restoreInSec;
        }
        public void Update()
        {
            if (gameObject.GetComponent<UnitHealth>().CurrHealth < gameObject.GetComponent<UnitHealth>().maxHealth && timeSec >= 1f)
            {
                Restore();
                timeSec = 0;
            }
            timeSec += Time.deltaTime;
        }
    }
}