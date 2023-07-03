﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Veldrid;
using Vortice.Direct3D11;

namespace SandAndStonesEngine.GameInput
{
    public class InputDevicesState
    {
        private HashSet<Key> prevKeys = new HashSet<Key>();
        private HashSet<Key> currentKeys = new HashSet<Key>();

        private HashSet<MouseButton> prevMouseKeys = new HashSet<MouseButton>();
        private HashSet<MouseButton> currentMouseKeys = new HashSet<MouseButton>();


        private Vector2 lastMousePosition;
        public Vector2 MousePosition;
        public Vector2 MouseDelta;

        public InputDevicesState()
        {

        }

        public void Update(InputSnapshot snapshot)
        {
            UpdateKeys(snapshot);
            UpdateMouse(snapshot);
        }

        private bool UpdateKeys(InputSnapshot snapshot)
        {
            bool keyAdded = false;
            currentKeys.Clear();
            var keyEvents = snapshot.KeyEvents;
            foreach(var keyEvent in keyEvents)
            {
                keyAdded = keyEvent.Down ? KeyDown(keyEvent.Key) : KeyUp(keyEvent.Key);
            }

            return keyAdded;
        }

        private bool UpdateMouse(InputSnapshot snapshot)
        {
            bool mouseButtonAdded = false;
            currentMouseKeys.Clear();

            lastMousePosition = MousePosition;
            MousePosition = snapshot.MousePosition;
            MouseDelta = MousePosition - lastMousePosition;
            var mouseEvents = snapshot.MouseEvents;
            foreach (var mouseEvent in mouseEvents)
            {
                mouseButtonAdded = mouseEvent.Down ? MouseDown(mouseEvent.MouseButton) : MouseUp(mouseEvent.MouseButton);
            }

            return mouseButtonAdded;
        }

        public bool GetMouseButton(MouseButton button)
        {
            return prevMouseKeys.Contains(button);
        }

        public bool GetMouseButtonDown(MouseButton button)
        {
            return currentMouseKeys.Contains(button);
        }

        public bool MouseDown(MouseButton mouseButton)
        {
            if (prevMouseKeys.Add(mouseButton) && currentMouseKeys.Add(mouseButton))
                return true;
            return false;
        }

        public bool MouseUp(MouseButton mouseButton)
        {
            if (prevMouseKeys.Remove(mouseButton) && currentMouseKeys.Remove(mouseButton))
                return true;
            return false;
        }

        public bool GetKey(Key key)
        {
            return prevKeys.Contains(key);
        }

        public bool GetKeyDown(Key key)
        {
            return currentKeys.Contains(key);
        }

        public bool KeyUp(Key key)
        {
            if (prevKeys.Remove(key) && currentKeys.Remove(key))
                return true;
            return false;
        }

        public bool KeyDown(Key key)
        {
            if (prevKeys.Add(key) && currentKeys.Add(key))
                return true;
            return false;
        }
    }
}
