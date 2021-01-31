namespace De.Lasagevo.Gameplay
{
    class PlayerStats
    {

        /// <summary>
        /// The players health points.
        /// </summary>
        private static int health = 100;

        /// <summary>
        /// The players health points.
        /// </summary>
        public static int Health
        {
            get { return health; }
            private set { health = value; }
        }

        /// <summary>
        /// True if and only if the player is alive.
        /// </summary>
        public static bool IsAlive
        {
            get { return Health > 0; }
        }

        public static void TakeDamage(int amount)
        {
            if (IsAlive)
            {
                health -= amount;
            }
        }

    }
}
