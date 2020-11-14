﻿using System;

namespace DefaultNamespace.Tricks
{
    public interface ITrick
    {
        void StartTrick();
        void UpdateTrick();
        bool FinishTrick();
        void FinishTrickWithTimer();
        string ToString();
        float GetCurrentScore();
        bool IsActive();
    }
}