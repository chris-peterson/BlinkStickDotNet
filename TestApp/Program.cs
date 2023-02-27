using BlinkStickDotNet;

using var blinkstick = BlinkStick.FindAll().Single();
blinkstick.OpenDevice();

var processDetector = new ProcessDetector();
if (processDetector.IsRunning("zoom"))
{
    SetColor(blinkstick, RgbColor.FromString("red"));
}
else
{
    SetColor(blinkstick, RgbColor.FromString("black"));
}
processDetector.OnProcessStarted += (sender, eventArgs) =>
{
    if (eventArgs.ProcessName.Contains("zoom"))
    {
        SetColor(blinkstick, RgbColor.FromString("red"));
    }
};
processDetector.OnProcessEnded +=  (sender, eventArgs) =>
{
    if (eventArgs.ProcessName.Contains("zoom"))
    {
        SetColor(blinkstick, RgbColor.FromString("black"));
    }
};

Thread.Sleep(int.MaxValue);

void SetColor(BlinkStick blinkStick, RgbColor color)
{
    int [] LedsToUse = new [] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, /* 15, */ 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 };
    byte [] LedIndexes = LedsToUse.Select(i => (byte) i).ToArray();
    foreach (var index in LedIndexes)
    {
        blinkStick.SetColor(0, index, color);
    }
}
