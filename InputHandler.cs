using Microsoft.Xna.Framework.Input;

namespace ControllerIndexingExample
{
    class InputHandler
    {
        /* My example game can handle four players at once.
         * I'm going to create an integer array that can handle four players 
         * and assign each of them a controller index.       
         * It is probably safe to assume no physical gamepad should return a negative index,
         * so a -1 by default will mean unassigned for the sake of our context.*/

        int[] playerControllerIndexes = new int[4] { -1, -1, -1, -1 };

        /* This Update function is being called outside of the class automatically, every frame.
         * I assume GameMaker has something similar.
         * I will call my control handling functions inside of this.
         * First, I will check for new assignments. Then do controls per player w/controller.*/
        public void Update()
        {
            // first thing I am going to do is check to see if there are any new controller assignments.
            CheckForControllerAssignments();

            // Then I am going to iterate through each player's controller index . . .
            for (int i = 0; i < playerControllerIndexes.Length; i++)
            {
                /* . . . to see if its index is not a negative number. 
                Remember, in our context, this would mean a controller has been assigned to player.*/
                if (playerControllerIndexes[i] > -1)
                {
                    // If true, do the player controls. i = the player number, playerControllerIndexes[i] = their controller.
                    DoPlayerControls(i, playerControllerIndexes[i]);
                }
            }
        }
         
        private void CheckForControllerAssignments()
        {
            // This is how your snippet of code did things.
            var maxpads = gamepad_get_device_count();

            // I am going to iterate through each pad, to see if any button is pressed.
            for (int i = 0; i < maxpads; i++)
            {
                // My engine handles some things differently than yours but hopefully this is clear enough to understand.

                // I am going to get the gamepad state of the controller index representing my 'i' variable.
                var state = GamePad.GetState(i);

                // Going to make sure this gamepad has a connected state.
                if (state.IsConnected)
                {
                    // Going to check if the Start button has been pressed.
                    // This can be different in your code, it doesn't have to be
                    // a particular button. It's just how I'm doing it.
                    if (state.Buttons.Start == ButtonState.Pressed)
                    {
                        // Ask to assign the next controller with i;
                        AssignNextContoller(i);
                    }
                }
            }
        }

        /* This function will assign a player's controller index with the index of the controller that will take exclusive control over them */
        private void AssignNextContoller(int controllerIndex)
        {
            /*I will iterate through each player controller index slot . . .  */
            for (int i = 0; i < playerControllerIndexes.Length; i++)
            {
                /* . . . and check  to see if it is already assigned. 
                 * Remember, for our context, a negative number is unassigned.*/
                if (0 > playerControllerIndexes[i])
                {
                    // assign the player to the controller's index.
                    playerControllerIndexes[i] = controllerIndex;
                    // breat out of this loop, so that we don't assign all unassigned controllers after this one.
                    break;
                }
            }
        }

        // This is a function that you are reference in your code.
        // I do not know the full details of how it works, so you can
        // ignore the code inside of it.
        private int gamepad_get_device_count()
        {
            // Just returning 4, for the sake of simplicity.
            return 4;
        }

        private void DoPlayerControls(int player, int controllerIndex)
        {
            // Do whatever you need to, to whatever player, based on the player and controller index.

            // Example:
            if (GamePad.GetState(controllerIndex).Buttons.A == ButtonState.Pressed)
            {
                //Jump(player);
            }
        }
    }
}
