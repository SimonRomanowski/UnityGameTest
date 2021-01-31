using De.Lasagevo.Gameplay;

namespace De.Lasagevo.GameSystems
{

    public class GameState
    {

        /// <summary>
        /// True if and only if the game is currently runnung.
        /// </summary>
        private static bool gameIsRunning = false;

        /// <summary>
        /// True if and only if the game is currently runnung.
        /// </summary>
        public static bool GameIsRunning
        {
            get { return gameIsRunning; }

            private set { gameIsRunning = value; }
        }

        /// <summary>
        /// The currently equipped weapon.
        /// </summary>
        private static IWeapon currentWeapon = new AssaultRifle();

        /// <summary>
        /// Starts the game.
        /// </summary>
        public static void StartGame()
        {
            gameIsRunning = true;
        }

        /// <summary>
        /// Stops the game.
        /// </summary>
        public static void StopGame()
        {
            gameIsRunning = false;
        }

        /// <summary>
        /// Returns the currently equipped weapon.
        /// </summary>
        internal static IWeapon GetCurrentWeapon()
        {
            return currentWeapon;
        }
    }

}