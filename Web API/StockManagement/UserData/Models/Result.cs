using System;

namespace UserData.Models
{
    public class Result
    {
        public string Data { get; set; }
        public bool isSuccess { get; set; } = false;
        public string Message { get; set; }
        public int RowCount { get; set; }
        public Exception Exception { get; set; }
    }
}
