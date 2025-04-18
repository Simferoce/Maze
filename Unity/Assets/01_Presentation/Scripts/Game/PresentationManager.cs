using Game.Core;
using System;
using System.Linq;
using UnityEngine;

namespace Game.Presentation
{
    public class PresentationManager : MonoBehaviour
    {
        [SerializeField] private PresentationRegistry presentationRegistry;

        public PresentationRegistry PresentationRegistry { get => presentationRegistry; set => presentationRegistry = value; }
        public PresentationInputManager PresentationInputManager { get; set; }
        public GameManager GameManager { get; set; }

        private UnityLogger unityLogger;
        private EntityVisualHandler entityVisualHandler;

        private void Awake()
        {
            unityLogger = new UnityLogger();
            GameManager = new Core.GameManager(unityLogger);
            entityVisualHandler = new EntityVisualHandler(this);
            PresentationInputManager = new PresentationInputManager(this);

            GameManager.Initialize(presentationRegistry.Definitions.Select(x => x.Convert()).ToList());
            GameManager.Start(new Game.Core.GameModeParameter() { PlayerEntityDefinition = new Guid(presentationRegistry.PlayerDefinition.Id) });
        }

        private void Update()
        {
            PresentationInputManager.Update();
        }

        private void FixedUpdate()
        {
            PresentationInputManager.Flush();
            GameManager.Update();
            entityVisualHandler.Update();
        }
    }
}