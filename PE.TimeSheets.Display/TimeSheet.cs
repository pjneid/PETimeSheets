using System;

namespace PE.TimeSheets.Display
{
    public enum State
    {
        Active,
        Submitted
    };

    public enum Type
    {
        TelephoneCall,
        Research,
        DraftingDocument
    };

    public class TimeSheet
    {
        public int ID { get; set; }
        public State State { get; set; }
        public string Title { get; set; }
        public Type Type { get; set; }
        public string Duration { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal Total { get; set; }

        public TimeSheet(int id, State state, string title, Type type, TimeSpan duration, decimal hourlyRate)
        {
            ID = id;
            State = state;
            Title = title;
            Type = type;
            Duration = duration.ToString(@"h\:mm");
            HourlyRate = hourlyRate;
            Total = GetTotal(duration, hourlyRate);
        }

        public TimeSheet(int id, decimal hourlyRate)
        {
            ID = id;
            State = State.Active;
            Title = string.Empty;
            Type = Type.DraftingDocument;
            Duration = new TimeSpan(0, 0, 0).ToString(@"h\:mm");
            HourlyRate = hourlyRate;
            Total = 0;
        }

        private decimal GetTotal(TimeSpan duration, decimal hourlyRate)
        {
            DateTime dt = new DateTime() + duration;
            string[] time = RoundUp(dt, TimeSpan.FromMinutes(15)).Split(':');

            decimal total = Convert.ToDecimal(time[0]) * hourlyRate;

            if (time.Length > 1)
                total += Convert.ToDecimal(time[1]) * hourlyRate / 60;

            return total;
        }

        string RoundUp(DateTime dt, TimeSpan d)
        {
            return new DateTime((dt.Ticks + d.Ticks - 1) / d.Ticks * d.Ticks, dt.Kind).ToString(@"h\:mm");
        }
    }
}