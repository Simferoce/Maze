using UnityEditor;
using UnityEngine;

public static class FrameRateSetter
{
    [MenuItem("Tools/Set Target Frame Rate/5 FPS", false, 0)]
    private static void SetFPS5() => SetTargetFrameRate(5);

    [MenuItem("Tools/Set Target Frame Rate/10 FPS", false, 1)]
    private static void SetFPS10() => SetTargetFrameRate(10);

    [MenuItem("Tools/Set Target Frame Rate/15 FPS", false, 2)]
    private static void SetFPS15() => SetTargetFrameRate(15);

    [MenuItem("Tools/Set Target Frame Rate/20 FPS", false, 3)]
    private static void SetFPS20() => SetTargetFrameRate(20);

    [MenuItem("Tools/Set Target Frame Rate/25 FPS", false, 4)]
    private static void SetFPS25() => SetTargetFrameRate(25);

    [MenuItem("Tools/Set Target Frame Rate/30 FPS", false, 5)]
    private static void SetFPS30() => SetTargetFrameRate(30);

    [MenuItem("Tools/Set Target Frame Rate/60 FPS", false, 6)]
    private static void SetFPS60() => SetTargetFrameRate(60);

    [MenuItem("Tools/Set Target Frame Rate/Reset (Platform Default)", false, 50)]
    private static void ResetFPS() => SetTargetFrameRate(-1);

    private static void SetTargetFrameRate(int fps)
    {
        Application.targetFrameRate = fps;
        Debug.Log(fps > 0
            ? $"Target frame rate set to {fps} FPS"
            : "Target frame rate reset to platform default (-1)");
    }
}
