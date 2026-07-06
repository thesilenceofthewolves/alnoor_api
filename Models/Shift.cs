public class Shift
{
    public int ShiftID { get; set; }
    public int ClientID { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string SpecialRequest { get; set; }
    public bool Emergency { get; set; }
}
