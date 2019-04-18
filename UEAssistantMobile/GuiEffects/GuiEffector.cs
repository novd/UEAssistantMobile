using System;
using System.Threading;
using Xamarin.Forms;

namespace UEAssistantMobile
{
    public static class GuiEffector
    {
        #region Opacity Region

        public static void OpacityMagicToMin(IEffectable effector, float stepSize, int breakTime, bool backToOrginal)
        {

            while (effector.OpacityEffect > 0)
            {
                effector.OpacityEffect -= stepSize;
                Thread.Sleep(breakTime);
            }

            if (backToOrginal)
            {
                while (effector.OpacityEffect < 1)
                {
                    effector.OpacityEffect += stepSize;
                    Thread.Sleep(breakTime);
                }
            }
        }

        public static void OpacityMagicToMax(IEffectable effector, float stepSize, int breakTime, bool backToOrginal)
        {
            effector.OpacityEffect = 0;

            while (effector.OpacityEffect < 1)
            {
                effector.OpacityEffect += stepSize;
                Thread.Sleep(breakTime);
            }

            if (backToOrginal)
            {
                while (effector.OpacityEffect > 0)
                {
                    effector.OpacityEffect -= stepSize;
                    Thread.Sleep(breakTime);
                }
            }
        }
        #endregion

        #region Rotation region
        public static void RotationMagic(IEffectable effector, float stepSize, int stepBreakTime, int repeats, int repeatBreakTime, bool backToOrginal)
        {
            var startState = effector.RotationEffect;

            for (int i = 0; i < repeats; i++)
            {
                effector.RotationEffect = startState;
                while (effector.RotationEffect < 360 + startState)
                {
                    effector.RotationEffect += stepSize;
                    Thread.Sleep(stepBreakTime);
                }

                if (backToOrginal)
                {
                    while (effector.RotationEffect > 0 + startState)
                    {
                        effector.RotationEffect -= stepSize;
                        Thread.Sleep(stepBreakTime);
                    }
                }

                Thread.Sleep(repeatBreakTime);
            }
        }
        #endregion

        public static void ColorMagic(IEffectable effector)
        {
            double r, g, b;
            Random random = new Random();
            while (effector.ColorEffect.R < 255)
            {
                r = random.NextDouble();
                g = random.NextDouble();
                b = random.NextDouble();
                effector.ColorEffect = Color.FromRgb(r, g, b);
                Thread.Sleep(2);
            }
        }
    }
}
