using Game.Core;
using System;
using System.Linq;
using UnityEngine;

namespace Game.Presentation
{
    public class PresentationManager : MonoBehaviour
    {
        [SerializeField] private PresentationRegistry presentationRegistry;

        public GameManager GameManager { get => gameManager; set => gameManager = value; }
        public PresentationRegistry PresentationRegistry { get => presentationRegistry; set => presentationRegistry = value; }

        private Game.Core.GameManager gameManager;
        private UnityLogger unityLogger;
        private EntityVisualHandler entityVisualHandler;

        private void Awake()
        {
            unityLogger = new UnityLogger();
            gameManager = new Core.GameManager(unityLogger);
            entityVisualHandler = new EntityVisualHandler(this);

            gameManager.Initialize(presentationRegistry.Definitions.Select(x => x.Convert()).ToList());
            gameManager.Start(new Guid(presentationRegistry.PlayerDefinition.Id));
        }

        private void FixedUpdate()
        {
            gameManager.Update();
            entityVisualHandler.Update();
        }
    }
}