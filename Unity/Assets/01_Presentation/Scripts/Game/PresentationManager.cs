using Game.Core;
using Newtonsoft.Json;
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
        public SaveManager SaveManager { get; set; }
        public PlatformManager PlatformManager { get; set; }
        public GameManager GameManager { get; set; }

        private UnityLogger unityLogger;
        private EntityVisualHandler entityVisualHandler;

        private void Awake()
        {
            unityLogger = new UnityLogger();
            GameManager = new Core.GameManager(unityLogger);
            entityVisualHandler = new EntityVisualHandler(this);
            PresentationInputManager = new PresentationInputManager(this);
            SaveManager = new SaveManager(this);
            PlatformManager = new PlatformManager();

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

        private void OnApplicationQuit()
        {
            Core.RecordSession currentSession = GameManager.Recorder.CurrentSession;
            RecordSessionSave recordSessionSave = new RecordSessionSave();
            recordSessionSave.GameModeParameter = JsonConvert.SerializeObject(currentSession.GameModeParameter);
            recordSessionSave.Inputs = currentSession.InputActions.Select(x => new RecordSessionSave.InputActionSave() { InputAxisType = x.InputAxisType, InputButtonType = x.InputButtonType, InputType = x.InputType, Tick = x.Tick, Value = x.Value }).ToList();
            SaveManager.Sessions.Add(recordSessionSave, $"{DateTime.Now.Ticks}");
            SaveManager.Sessions.Flush();
        }
    }
}