using Game.Core;
using UnityEngine;

namespace Game.Presentation
{
    public class ReplayManager : MonoBehaviour
    {
        private RecordSession recordSession;
        private GameManager gameManager;
        private UnityLogger logger;
        private int inputIndex = 0;

        public ReplayManager(RecordSession recordSession)
        {
            this.recordSession = recordSession;
            logger = new UnityLogger();
            gameManager = new GameManager(logger);
        }

        public void FixedUpdate()
        {
            while (recordSession.InputActions.Count > inputIndex && recordSession.InputActions[inputIndex].Tick == gameManager.TimeManager.CurrentTick)
            {
                gameManager.InputManager.Execute(recordSession.InputActions[inputIndex]);

                inputIndex++;
            }

            gameManager.Update();
        }
    }
}
