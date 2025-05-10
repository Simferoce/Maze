using System;

namespace Game.Core
{
    public class GameModeParameter
    {
        public Guid WorldDefinition { get; set; }
        public Guid PlayerCharacterDefinition { get; set; }
        public Guid PlayerDefinition { get; set; }
        public int Seed { get; set; }
    }
}