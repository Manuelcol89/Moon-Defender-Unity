using System;

public static class Actions
{
    public static Action OnBulletDestroyed;

    public static Action<int> OnAsteroidDestroyed;

    public static Action<HealthHandler, int, int> OnHealthChanged;

    public static Action OnGamePaused;

    public static Action OnGameUnpaused;

    public static Action OnPauseButtonPressed;

    public static Action<int, float> OnGameWin;
}
