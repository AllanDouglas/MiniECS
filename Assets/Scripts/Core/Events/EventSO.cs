using System;
using System.Collections.Generic;

#if UNITY_EDITOR
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
#endif
using UnityEngine;
using UnityEngine.Events;

namespace MiniECS
{

    public abstract class EventSO : ScriptableObject, ISignal
    {
#if UNITY_EDITOR
        [SerializeField] protected bool _debug;
        [SerializeField] protected bool _pause;
#endif

        [SerializeField] protected UnityEvent onDispatch;

        private event Action onDispatchEvent;

#if UNITY_EDITOR
        List<Action> _debugActions = new();
#endif

        public virtual void Dispatch(
            [CallerMemberName] string callerName = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerFilePath] string callerFilePath = "")
        {
            onDispatch.Invoke();
#if UNITY_EDITOR
            if (_debug)
            {
                var actions = _debugActions.ToList();
                var sb = new StringBuilder();
                sb.AppendLine($"### Event: {name}");
                sb.AppendLine($"### Caller {callerName}, line {callerLineNumber}, file {callerFilePath}");
                foreach (var action in actions)
                {
                    foreach (var del in action.GetInvocationList())
                    {
                        sb.AppendLine($"### Calling to: {del.Target} - {del.Method.Name}");
                    }
                    action.Invoke();
                }
                UnityEngine.Debug.Log(sb.ToString(), this);
            }
            else
#endif
                onDispatchEvent?.Invoke();
#if UNITY_EDITOR
            Debug();
#endif
        }

        public virtual void Subscribe(Action action)
        {

#if UNITY_EDITOR
            _debugActions.Add(action);
#endif

            onDispatchEvent += action;
        }

        public virtual void Unsubscribe(Action action)
        {
#if UNITY_EDITOR
            _debugActions.Remove(action);
#endif
            onDispatchEvent -= action;
        }

        public virtual void RemoveActions()
        {
            onDispatchEvent = null;
            onDispatch.RemoveAllListeners();
#if UNITY_EDITOR
            _debugActions.Clear();
#endif
        }

        private void OnDestroy() => RemoveActions();
        private void OnDisable() => RemoveActions();


        protected void Debug()
        {
#if UNITY_EDITOR
            if (_debug)
            {
                StackTrace st = new(true);
                StringBuilder log = new();

                log.AppendLine($"DISPATCHING EVENT {name}");
                for (int i = 0; i < st.FrameCount; i++)
                {
                    StackFrame frame = st.GetFrame(i);
                    log.AppendLine($"CAll STACK {frame.GetMethod()} on {frame.GetFileName()}, {frame.GetFileLineNumber()},{frame.GetFileColumnNumber()}");
                }
                UnityEngine.Debug.Log(log, this);

                if (_pause)
                {
                    UnityEngine.Debug.Break();
                }
            }
#endif
        }
    }
}