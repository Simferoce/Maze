using Game.Core;
using UnityEngine;

namespace Game.Presentation
{
    public class ReplayManager : MonoBehaviour
    {
        private RecordSession recordSession;
        private GameManager gameManager;
        private UnityLogger logger;
        private int commandIndex = 0;

        public ReplayManager(RecordSession recordSession)
        {
            this.recordSession = recordSession;
            logger = new UnityLogger();
            gameManager = new GameManager(logger);
        }

        public void FixedUpdate()
        {
            while (recordSession.Commands.Count > commandIndex && recordSession.Commands[commandIndex].Tick == gameManager.TimeManager.CurrentTick)
            {
                gameManager.CommandManager.Execute(recordSession.Commands[commandIndex]);

                commandIndex++;
            }

            gameManager.Update();
        }
    }
}
