namespace Lab_2.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string DeviceCode { get; set; }
        public string DeviceName { get; set; }
        public int CategoryId { get; set; }
        public string Status { get; set; }
        public DateTime DateOfEntry { get; set; }
        public Category Category { get; set; }

    }
}
