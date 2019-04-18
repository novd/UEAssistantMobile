using System;
using System.Drawing;

namespace UEAssistantMobile
{
    public interface IEffectable
    {
        float OpacityEffect { get; set; }
        float RotationEffect { get; set; }
        Color ColorEffect { get; set; }
    }
}
