using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong.Front_End.Managers {

    //TODO Is it possible to add in key combo support? E.G., Alt+Enter?
    public class InputManager {

        private Keys[] previouslyPressedButtons = { };
        private Dictionary<Keys, List<Action>> keyboardCallbacks = new Dictionary<Keys, List<Action>>();

        public void Update(KeyboardState keyboardState) {
            IEnumerable<Keys> releasedButtons = previouslyPressedButtons.Except(keyboardState.GetPressedKeys());
            foreach (Keys key in releasedButtons) {
                if (keyboardCallbacks.ContainsKey(key)) {
                    List<Action> actions = keyboardCallbacks[key];
                    foreach (Action callback in actions.Reverse<Action>()) {
                        callback();
                    }
                }
            }
            previouslyPressedButtons = keyboardState.GetPressedKeys();
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
    }
}
