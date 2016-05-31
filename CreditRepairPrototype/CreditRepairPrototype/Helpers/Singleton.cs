using System;

namespace CreditRepairPrototype
{
    public class Singleton<T> where T : class, new()
    {
        static T _instance;
        public static T Instance
        {
            get
            {
                Object locker = new object();
                if (_instance == null)
                {
                    lock (locker)
                    {
                        _instance = new T();
                    }
                }
                return _instance;
            }
        }
    }
}

