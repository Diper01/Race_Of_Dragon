using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;

namespace MaxTools
{
    public interface IGameMessage
    {
        int GetCount();

        void Clear();
    }

    public class GameMessage : IGameMessage
    {
        Action action = null;

        List<int> eventList = new List<int>();

        public void AddListener(Action action)
        {
            this.action += action;
        }

        public void RemoveListener(Action action)
        {
            this.action -= action;
        }

        public void Announce()
        {
            action?.Invoke();

            Clear();

            eventList.Add(Time.frameCount);
        }

        public int GetCount()
        {
            int сount = 0;

            for (int i = 0; i < eventList.Count; ++i)
            {
                if (eventList[i] == Time.frameCount - 1)
                {
                    сount++;
                }
            }

            return сount;
        }

        public void Clear()
        {
            for (int i = 0; i < eventList.Count; ++i)
            {
                if (eventList[i] < Time.frameCount - 1)
                {
                    eventList.RemoveAt(i--);
                }
            }
        }
    }

    public class GameMessage<T> : IGameMessage
    {
        Action<T> action = null;

        List<(int frame, T value)> eventList = new List<(int frame, T value)>();

        public void AddListener(Action<T> action)
        {
            this.action += action;
        }

        public void RemoveListener(Action<T> action)
        {
            this.action -= action;
        }

        public void Announce(T value)
        {
            action?.Invoke(value);

            Clear();

            eventList.Add((Time.frameCount, value));
        }

        public int GetCount()
        {
            int сount = 0;

            for (int i = 0; i < eventList.Count; ++i)
            {
                if (eventList[i].frame == Time.frameCount - 1)
                {
                    сount++;
                }
            }

            return сount;
        }

        public void Clear()
        {
            for (int i = 0; i < eventList.Count; ++i)
            {
                if (eventList[i].frame < Time.frameCount - 1)
                {
                    eventList.RemoveAt(i--);
                }
            }
        }

        public T[] GetValues()
        {
            var list = new List<T>();

            for (int i = 0; i < eventList.Count; ++i)
            {
                if (eventList[i].frame == Time.frameCount - 1)
                {
                    list.Add(eventList[i].value);
                }
            }

            return list.ToArray();
        }
    }

    public static class GameMessenger
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Initialize()
        {
            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

            foreach (var type in Tools.GetAllTypes())
            {
                foreach (var field in type.GetFields(bindingFlags))
                {
                    if (typeof(IGameMessage).IsAssignableFrom(field.FieldType))
                    {
                        field.SetValue(null, Activator.CreateInstance(field.FieldType));
                    }
                }
            }
        }
    }
}
