using System;
using UnityEngine;
using System.Collections.Concurrent;


public class TimerManager : MonoBehaviour
{
    private ObjectPool<Timer> timerPool;

    public static TimerManager Instance;

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        timerPool = new ObjectPool<Timer>(20);
    }


    void FixedUpdate()
    {
        foreach (var timer in timerPool.getAll()) {
            if (timer.Active) {
                timer.updateTimer(Time.deltaTime);
            }
        }
    }


    public void releaseFromPool(Timer timer)
    {
        timerPool.release(timer);
        timer.resetTimer();
    }

    public void startTimer(float time, Func<bool> timerEndEvent)
    {
        Timer timer = timerPool.get();
        timer.Action = timerEndEvent;
        timer.EndTime = time;
        timer.TimerManager = this;
        timer.Active = true;
    }


    /*
            Timer class
            You can move it to a different file
    */
    public class Timer
    {
        private float endTime;
        private Func<bool> action;
        private TimerManager timerManager;
        private bool active;


        public TimerManager TimerManager
        {
            set => timerManager = value;
        }

        public float EndTime
        {
            set => endTime = value;
        }

        public Func<bool> Action
        {
            set => action = value;
        }

        public bool Active
        {
            get => active;
            set => active = value;
        }

        public void updateTimer(float time)
        {
            endTime -= time;
            if (endTime < 0) {
                action?.Invoke();
                Debug.Log(action?.Target.ToString());
                if (timerManager != null) {
                    timerManager.releaseFromPool(this);
                }
            }
        }

        public void resetTimer()
        {
            endTime = 0;
            action = null;
            timerManager = null;
            active = false;
        }
    }


    /*
            Object Pool
            It's generic so you can use for other uses
            You can move it to a different file
    */
    public class ObjectPool<T> where T : new()
    {
        private readonly ConcurrentBag<T> items = new ConcurrentBag<T>();
        private int counter = 0;
        private readonly int MAX;

        public ObjectPool(int max)
        {
            MAX = max;
        }

        public void release(T item)
        {
            if (counter < MAX) {
                items.Add(item);
                counter++;
            }
        }

        public T get()
        {
            if (items.TryTake(out var item)) {
                counter--;
                return item;
            }
            else {
                T obj = new T();
                items.Add(obj);
                counter++;
                return obj;
            }
        }

        public ConcurrentBag<T> getAll()
        {
            return items;
        }
    }


}