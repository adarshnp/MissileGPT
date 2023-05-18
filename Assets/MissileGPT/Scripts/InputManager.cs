using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace missilegpt
{
    public class InputManager : MonoBehaviour
    {
        public event Action OnMoveDirectionKeyPress;
        private void Update()
        {
            if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            {
                OnMoveDirectionKeyPress?.Invoke();
            }
        }
    }
}