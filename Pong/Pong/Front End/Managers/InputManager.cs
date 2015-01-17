using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong.Front_End.Managers {

    public enum MouseButtons {
        LeftButton, RightButton, MiddleButton
    }
    //TODO Is it possible to add in key combo support? E.G., Alt+Enter?
    public class InputManager {

        private Keys[] previouslyPressedKeys = { };
        private List<MouseButtons> previouslyPressedMouseButtons = new List<MouseButtons> { };
        private Dictionary<Keys, List<Action>> keyboardCallbacks = new Dictionary<Keys, List<Action>>();
        private Dictionary<MouseButtons, List<Tuple<Rectangle, Action>>> mouseCallbacks = new Dictionary<MouseButtons, List<Tuple<Rectangle, Action>>>();

        public void Update(KeyboardState keyboardState, MouseState mouseState) {
            DoKeyboardUpdate(keyboardState);
            DoMouseUpdate(mouseState);

        }

        private void DoKeyboardUpdate(KeyboardState keyboardState) {
            IEnumerable<Keys> releasedButtons = previouslyPressedKeys.Except(keyboardState.GetPressedKeys());
            foreach (Keys key in releasedButtons) {
                if (keyboardCallbacks.ContainsKey(key)) {
                    List<Action> actions = keyboardCallbacks[key];
                    foreach (Action callback in actions.Reverse<Action>()) {
                        callback();
                    }
                }
            }
            previouslyPressedKeys = keyboardState.GetPressedKeys();
        }
        private void DoMouseUpdate(MouseState mouseState) {
            List<MouseButtons> pressedButtons = new List<MouseButtons> { };
            if (mouseState.LeftButton == ButtonState.Pressed) {
                pressedButtons.Add(MouseButtons.LeftButton);
            }
            if (mouseState.MiddleButton == ButtonState.Pressed) {
                pressedButtons.Add(MouseButtons.MiddleButton);
            }
            if (mouseState.RightButton == ButtonState.Pressed) {
                pressedButtons.Add(MouseButtons.RightButton);
            }
            IEnumerable<MouseButtons> releasedButtons = previouslyPressedMouseButtons.Except(pressedButtons);
            foreach (MouseButtons button in releasedButtons) {
                if (mouseCallbacks.ContainsKey(button)) {
                    List<Tuple<Rectangle, Action>> boundsAndActions = mouseCallbacks[button];
                    foreach (Tuple<Rectangle, Action> boundActionTuple in boundsAndActions.Reverse<Tuple<Rectangle, Action>>()) {
                        Rectangle bounds = boundActionTuple.Item1;
                        Point clickedPoint = new Point(mouseState.X, mouseState.Y);
                        if (bounds.Contains(clickedPoint)) {
                            boundActionTuple.Item2();
                        }
                    }
                }
            }
            previouslyPressedMouseButtons = pressedButtons;
        }

        public void Register(Keys key, Action callback) {
            List<Action> callbacksForKey;
            if (keyboardCallbacks.ContainsKey(key)) {
                callbacksForKey = keyboardCallbacks[key];
            } else {
                callbacksForKey = new List<Action>();
            }
            callbacksForKey.Add(callback);
            keyboardCallbacks[key] = callbacksForKey;
        }

        public void Unregister(Keys key, Action callback) {
            if (keyboardCallbacks.ContainsKey(key)) {
                keyboardCallbacks[key].Remove(callback);
            }
        }
        public void Register(MouseButtons button, Rectangle bounds, Action callback) {
            List<Tuple<Rectangle, Action>> callbacksForMouseButton;
            if (mouseCallbacks.ContainsKey(button)) {
                callbacksForMouseButton = mouseCallbacks[button];
            } else {
                callbacksForMouseButton = new List<Tuple<Rectangle, Action>>();
            }
            callbacksForMouseButton.Add(new Tuple<Rectangle, Action>(bounds, callback));
            mouseCallbacks[button] = callbacksForMouseButton;
        }
        public void Unregister(MouseButtons button, Rectangle bounds, Action callback) {
            if (mouseCallbacks.ContainsKey(button)) {
                mouseCallbacks[button].Remove(new Tuple<Rectangle, Action>(bounds, callback));
            }
        }

    }
}
