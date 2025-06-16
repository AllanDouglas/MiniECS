using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.Compilation;
#endif

namespace MiniECS.Framework
{

    [CreateAssetMenu(menuName = "ClapClap/Events/Data/GenericEventSettings")]
    public sealed class GameEventSettingsSO : ScriptableObject
    {
        [SerializeField, PathSelector] private string _output = "Assets/_Game/Scripts/Generated";
        [SerializeField, PathSelector] private string _namespace = "ClapClap.Game";
        [SerializeField] private GameEventDefinition[] _events;

        [SerializeField, HideInInspector] private int _eventsHash;
        [SerializeField, HideInInspector] private int _incrementalEventId = 1;

        public GameEventDefinition[] Events { get => _events; set => _events = value; }
#if UNITY_EDITOR
        public void Generate()
        {
            if (string.IsNullOrEmpty(_output))
            {
                Debug.LogWarning($"Output path is not set for {name}");
                return;
            }

            var sb = new StringBuilder();
            foreach (var evt in _events)
            {
                sb.Append(evt.Name);
            }

            var currentEventHash = GenerateHash(sb.ToString());

            if (_eventsHash != currentEventHash)
            {
                _eventsHash = currentEventHash;
                string filePath = Path.Combine(_output, "GameEvents.g.cs");

                string classContent = GenerateClassContent(_events);
                try
                {
                    string directory = Path.GetDirectoryName(filePath);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    File.WriteAllText(filePath, classContent);

                    Debug.Log($"Generated: {filePath}");
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"Failed to generate file at {filePath}: {e.Message}");
                }

                CompilationPipeline.RequestScriptCompilation();

            }

        }

        public int GenerateHash(string input)
        {

            int hash = 12345;

            foreach (char c in input)
            {
                hash = hash * 31 + c;
            }

            return hash;
        }

        private void OnValidate()
        {
            for (int i = 0; i < _events.Length; i++)
            {
                ref GameEventDefinition item = ref _events[i];
                if (item.Id == 0)
                {
                    item.Id = _incrementalEventId;
                    _incrementalEventId++;
                }
            }
        }

        private string GenerateClassContent(GameEventDefinition[] eventDefinitions)
        {

            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            var hasSet = new HashSet<GameEventDefinition>(eventDefinitions);

            var eventsString = new StringBuilder();

            foreach (GameEventDefinition eventDefinition in hasSet)
            {

                string eventName = ToPascalCase(eventDefinition.Name, textInfo);
                var eventStructName = $"{eventName}Event";

                var evt = $@"
                [Serializable]
                public partial struct {eventStructName}: IGameEvent, IEquatable<{eventStructName}>, IEquatable<IGameEvent>
                {{

                    public readonly int Id => {eventDefinition.Id};
                    public readonly string Name => ""{eventDefinition.Name}"";

                    public bool Equals({eventStructName} other) => other.Id == Id;
                    public bool Equals(IGameEvent other) => other.Id == Id;
                }}";

                eventsString.Append(evt);

            }

            return $@"// Auto-generated file. Do not edit.
            using System;
            using MiniECS; 

            namespace {_namespace} {{
                {eventsString}
            }}";
        }

        public string ToPascalCase(string str, TextInfo textInfo)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            var words = new List<string>();
            var currentWord = new StringBuilder();

            foreach (char c in str)
            {
                if (char.IsUpper(c) && currentWord.Length > 0)
                {
                    words.Add(currentWord.ToString());
                    currentWord.Clear();
                }
                currentWord.Append(c);
            }

            if (currentWord.Length > 0)
            {
                words.Add(currentWord.ToString());
            }

            for (int i = 0; i < words.Count; i++)
            {
                words[i] = textInfo.ToTitleCase(words[i].ToLower());
            }

            return string.Join(string.Empty, words);
        }

#endif
    }
}