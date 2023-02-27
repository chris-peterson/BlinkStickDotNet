using BlinkStickDotNet;

var apps = new Dictionary<string, string>()
{
    { "zoom.us", "red" },
    { "rider", "midnightblue" },
    { "code helper", "mediumblue" },
    { "", "black" }
};

using var blinkstick = BlinkStick.FindAll().Single();
blinkstick.OpenDevice();
SetColor(blinkstick, "black");

string previousApp = null;
while (true)
{
    var app = ProcessDetector.CheckForApp(apps.Keys);
    if (app != previousApp)
    {
        SetColor(blinkstick, apps[app]);
    }
    previousApp = app;
}

void SetColor(BlinkStick blinkStick, string color)
{
    var LedsToUse = new [] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 };
    var LedIndexes = LedsToUse.Select(i => (byte) i).ToArray();
    foreach (var index in LedIndexes)
    {
        blinkStick.SetColor(0, index, color);
    }
}
