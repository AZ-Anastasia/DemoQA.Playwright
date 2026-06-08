using System.ComponentModel;

namespace DemoQA.API.Enums;

public enum BookIsbn
{
    [Description("9781449325862")]
    GitPocketGuide,

    [Description("9781449331818")]
    LearningJavaScriptDesignPatterns,

    [Description("9781449337711")]
    DesigningEvolvableWebAPIswithASPNET
}

public enum BookIsbnToReplace
{
    [Description("9781449365035")]
    SpeakingJavaScript
}