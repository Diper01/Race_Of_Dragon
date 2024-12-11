using UnityEngine;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MaxTools.Extensions
{
    public static class EX_Debug
    {
        public static void DebugLogWarning(this object sender, object message, [CallerMemberName] string mn = "")
        {
            Debug.LogWarning($"[{sender.GetType().Name}-{mn}]\n{message.ToString()}\n", sender as Object);
        }
        public static void DebugLog(this object sender, object message, [CallerMemberName] string mn = "")
        {
            Debug.Log($"[{sender.GetType().Name}-{mn}]\n{message.ToString()}\n", sender as Object);
        }
        public static void DebugLogError(this object sender, object message, [CallerMemberName] string mn = "")
        {
            Debug.LogError($"[{sender.GetType().Name}-{mn}]\n{message.ToString()}\n", sender as Object);
        }

        public static void DebugLogWarning(this object sender, object[] messages, [CallerMemberName] string mn = "")
        {
            sender.DebugLogWarning(messages.Aggregate("", (a, b) => a + b + "\n", (s) => s.Remove(s.Length - 1)), mn);
        }
        public static void DebugLog(this object sender, object[] messages, [CallerMemberName] string mn = "")
        {
            sender.DebugLog(messages.Aggregate("", (a, b) => a + b + "\n", (s) => s.Remove(s.Length - 1)), mn);
        }
        public static void DebugLogError(this object sender, object[] messages, [CallerMemberName] string mn = "")
        {
            sender.DebugLogError(messages.Aggregate("", (a, b) => a + b + "\n", (s) => s.Remove(s.Length - 1)), mn);
        }
    }
}
