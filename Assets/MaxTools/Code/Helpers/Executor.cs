using System;
using System.Collections.Generic;

namespace MaxTools
{
    public class Executor : Singleton<Executor>
    {
        public class Item
        {
            public object key;
            public Func<bool> condition;
            public Action runAction;
            public Action breakAction;
            public Action<bool> pauseAction;
            public int priority;

            public bool isBroken { get; private set; }

            bool m_IsPaused;

            public bool isPaused
            {
                get
                {
                    return m_IsPaused;
                }

                set
                {
                    m_IsPaused = value;

                    pauseAction?.Invoke(value);
                }
            }

            public Item() { }
            public Item(object key)
            {
                this.key = key;
            }
            public Item(object key, Func<bool> condition)
            {
                this.key = key;
                this.condition = condition;
            }
            public Item(object key, Func<bool> condition, Action runAction)
            {
                this.key = key;
                this.condition = condition;
                this.runAction = runAction;
            }
            public Item(object key, Func<bool> condition, Action runAction, Action breakAction)
            {
                this.key = key;
                this.condition = condition;
                this.runAction = runAction;
                this.breakAction = breakAction;
            }
            public Item(object key, Func<bool> condition, Action runAction, Action breakAction, Action<bool> pauseAction)
            {
                this.key = key;
                this.condition = condition;
                this.runAction = runAction;
                this.breakAction = breakAction;
                this.pauseAction = pauseAction;
            }
            public Item(object key, Func<bool> condition, Action runAction, Action breakAction, Action<bool> pauseAction, int priority)
            {
                this.key = key;
                this.condition = condition;
                this.runAction = runAction;
                this.breakAction = breakAction;
                this.pauseAction = pauseAction;
                this.priority = priority;
            }

            public void Break()
            {
                if (!isBroken)
                {
                    isBroken = true;

                    instance.items.Remove(this);

                    breakAction?.Invoke();
                }
            }

            public Item SetKey(object key)
            {
                this.key = key;

                return this;
            }
            public Item SetCondition(Func<bool> condition)
            {
                this.condition = condition;

                return this;
            }
            public Item SetRunAction(Action runAction)
            {
                this.runAction = runAction;

                return this;
            }
            public Item SetBreakAction(Action breakAction)
            {
                this.breakAction = breakAction;

                return this;
            }
            public Item SetPauseAction(Action<bool> pauseAction)
            {
                this.pauseAction = pauseAction;

                return this;
            }
            public Item SetPriority(int priority)
            {
                this.priority = priority;

                return this;
            }

            public Item Run()
            {
                instance.items.Add(this);

                return this;
            }
            public Item RunUnique()
            {
                int i = instance.items.FindIndex((item) => item.key.Equals(key));

                if (i < 0)
                {
                    instance.items.Add(this);

                    return this;
                }
                else
                    return instance.items[i];
            }
            public Item RunReplace()
            {
                int i = instance.items.FindIndex((item) => item.key.Equals(key));

                if (i < 0)
                {
                    instance.items.Add(this);

                    return this;
                }
                else
                {
                    Item item = instance.items[i];

                    if (priority >= item.priority)
                    {
                        return instance.items[i] = this;
                    }
                    else
                        return item;
                }
            }
        }

        List<Item> items = new List<Item>();

        public static Item NewItem()
        {
            return new Item();
        }
        public static Item NewItem(object key)
        {
            return new Item(key);
        }
        public static Item NewItem(object key, Func<bool> condition)
        {
            return new Item(key, condition);
        }
        public static Item NewItem(object key, Func<bool> condition, Action runAction)
        {
            return new Item(key, condition, runAction);
        }
        public static Item NewItem(object key, Func<bool> condition, Action runAction, Action breakAction)
        {
            return new Item(key, condition, runAction, breakAction);
        }
        public static Item NewItem(object key, Func<bool> condition, Action runAction, Action breakAction, Action<bool> pauseAction)
        {
            return new Item(key, condition, runAction, breakAction, pauseAction);
        }
        public static Item NewItem(object key, Func<bool> condition, Action runAction, Action breakAction, Action<bool> pauseAction, int priority)
        {
            return new Item(key, condition, runAction, breakAction, pauseAction, priority);
        }

        public static Item FindItem(object key)
        {
            return instance.items.Find((item) => item.key.Equals(key));
        }
        public static List<Item> FindItems(object key)
        {
            return instance.items.FindAll((item) => item.key.Equals(key));
        }

        void Update()
        {
            for (int i = 0; i < items.Count; ++i)
            {
                if (!items[i].isPaused && !items[i].isBroken)
                {
                    if (items[i].condition())
                    {
                        items[i].runAction?.Invoke();
                    }
                    else
                        items[i--].Break();
                }
            }
        }
    }
}
