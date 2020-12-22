using System;

namespace CyberTimer.Data
{
    public class SplitItem
    {
        public string Name { get; set; }
        public int SecondEstimated { get; set; }
        public int MinuteEstimated { get; set; }
        public int HourEstimated { get; set; }
        public int SecondElapse { get; set; }
        public int MinuteElapse { get; set; }
        public int HourElapse { get; set; }
        public bool DeltaSign { get; set; }
        public int SecondDelta { get; set; }
        public int MinuteDelta { get; set; }
        public int HourDelta { get; set; }
        public bool EditMode { get; set; }

        public string FormattedEstimatedTime()
        {
            return FormatTime(HourEstimated, MinuteEstimated, SecondEstimated);
        }
        public string FormatActualTime()
        {
            return FormatTime(HourElapse, MinuteElapse, SecondElapse);
        }
        public string FormatDelta()
        {
            return FormatTime(HourDelta, MinuteDelta, SecondDelta);
        }
        public void Increment()
        {

            SecondElapse += 1;

            if (SecondElapse == 60)
            {
                MinuteElapse += 1;
                SecondElapse = 0;
            }
            if (MinuteElapse == 60)
            {
                MinuteElapse = 0;
                HourElapse += 1;
            }
            UpdateDelta();

        }
        public void ResetSplit()
        {
            DeltaSign = true;
            SecondElapse = 0;
            SecondDelta = 0;
            MinuteElapse = 0;
            MinuteDelta = 0;
            HourElapse = 0;
            HourDelta = 0;
        }
        public void CleanEstimation()
        {
            int seconds = 0;
            int minutes = 0;
            int hours = 0;

            //convert everything to seconds
            seconds += SecondEstimated;
            minutes += MinuteEstimated;
            hours += HourEstimated;

            seconds += minutes * 60;
            seconds += hours * 60 * 60;

            //then back to hours/min/seconds
            HourEstimated = seconds / (60 * 60);
            seconds = seconds - (HourEstimated * 60 * 60);// take out the seconds that account for the deltaHour
            MinuteEstimated = seconds / (60);
            SecondEstimated = seconds - (MinuteEstimated * 60);// take out the seconds that account for the deltaMinute

        }
        public void UpdateDelta()
        {
            // convert everything to seconds
            int estTotalSeconds = (HourEstimated * 60 * 60) + (MinuteEstimated * 60) + SecondEstimated;
            int actualTotalSeconds = (HourElapse * 60 * 60) + (MinuteElapse * 60) + SecondElapse;
            // get difference
            int deltaTotalSeconds = estTotalSeconds - actualTotalSeconds;
            // convert back to HH:MM:SS format
            DeltaSign = deltaTotalSeconds > 0; //used to render the delta as red/green
            deltaTotalSeconds = Math.Abs(deltaTotalSeconds); //pulled sign out, capped in public property
            HourDelta = deltaTotalSeconds / (60 * 60);
            deltaTotalSeconds = deltaTotalSeconds - (HourDelta * 60 * 60);// take out the seconds that account for the deltaHour
            MinuteDelta = deltaTotalSeconds / (60);
            SecondDelta = deltaTotalSeconds - (MinuteDelta * 60);// take out the seconds that account for the deltaMinute
        }
        private string FormatTime(int hour, int min, int sec)
        {
            // if time is 0,0,0, return "-"
            // else if hours is 0, don't render hour
            // else if minute is 0 don't render 
            // else render all
            if (hour != 0)
            {
                return $"{hour.ToString()}:{min.ToString().PadLeft(2, '0')}:{sec.ToString().PadLeft(2, '0')}";
            }
            if (min != 0 || sec!=0)
            {
                return $"{min.ToString()}:{sec.ToString().PadLeft(2, '0')}";
            }
            //if (sec != 0)
            //{
            //    return $"{sec.ToString()}";
            //}
            return "";
        }

    }
}
