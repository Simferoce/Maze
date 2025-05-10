using System;
using System.Collections.Generic;

namespace Game.Core
{
    public class UpdateManager
    {
        private GameManager gameManager;
        private List<Action> updatees = new List<Action>();

        public UpdateManager(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public void Update()
        {
            foreach (Action action in updatees)
                action?.Invoke();
        }

        public void Register(Action updatee)
        {
            updatees.Add(updatee);
        }

        public void Unregister(Action updatee)
        {
            updatees.Remove(updatee);
        }
    }
}
