using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.Compilation;
#endif

namespace MiniECS
{

    [CreateAssetMenu(menuName = "MiniECS/Config/MessageSettings")]
    public sealed class MessageSettingsSO : ScriptableObject
    {
        [SerializeField, PathSelector] private string _output = "Assets/_Game/Scripts/Generated";
        [SerializeField] private string _fileName = "messages.g.cs";
        [SerializeField] private string _namespace = "Game";
        [SerializeField] private int _incrementalEventId = 1;
        [SerializeField] private MessageDefinition[] _messages;

        [SerializeField, HideInInspector] private int _eventsHash;

        public MessageDefinition[] Messages { get => _messages; set => _messages = value; }
#if UNITY_EDITOR
        public void Generate()
        {
            if (string.IsNullOrEmpty(_output))
            {
                Debug.LogWarning($"Output path is not set for {name}");
                return;
            }

            var sb = new StringBuilder();
            foreach (var evt in _messages)
            {
                sb.Append(evt.Name);
            }

            var currentEventHash = GenerateHash(sb.ToString());

            if (_eventsHash != currentEventHash)
            {
                _eventsHash = currentEventHash;
                string filePath = Path.Combine(_output, _fileName);

                string classContent = GenerateClassContent(_messages);
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
            for (int i = 0; i < _messages.Length; i++)
            {
                ref MessageDefinition item = ref _messages[i];
                if (item.Id == 0)
                {
                    item.Id = _incrementalEventId;
                    _incrementalEventId++;
                }
            }
        }

        private string GenerateClassContent(MessageDefinition[] eventDefinitions)
        {

            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            var hasSet = new HashSet<MessageDefinition>(eventDefinitions);

            var eventsString = new StringBuilder();

            foreach (MessageDefinition eventDefinition in hasSet)
            {

                string eventName = ToPascalCase(eventDefinition.Name, textInfo);
                var eventStructName = $"{eventName}Message";
                var unityEventStructName = $"{eventName}UnityEvent";
                var eventListenerName = $"{eventName}MessageListener";

                var evt = $@"
    [Serializable]
    public partial struct {eventStructName}: IMessage
    {{
        public readonly int Id => {eventDefinition.Id};
    }}

    [Serializable]
    public sealed class {unityEventStructName} : UnityEvent<{eventStructName}> {{}}

    [Serializable]
    public sealed class {eventListenerName} : MessageListener<{eventStructName}, {unityEventStructName}> {{}}";

                eventsString.Append(evt);

            }

            return
$@"// Auto-generated file. Do not edit.
using System;
using MiniECS; 
using UnityEngine.Events;
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

        [Serializable]
        public struct MessageDefinition
        {
            public string Name;
            public int Id;
            public readonly override int GetHashCode() => Id;
        }
    }
}