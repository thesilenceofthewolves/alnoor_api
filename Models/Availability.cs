public class Availability
{
    public int StaffID { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool Available { get; set; }
    public bool EmergencyAvailable { get; set; }
}
