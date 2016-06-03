
namespace ChangeMachine.Core.DataContract
{
    public enum ReportType { UNDEFINED, ERROR, WARN, INFO };

    public sealed class Report
    {
        public ReportType Type { get; set; }

        public string Property { get; set; }

        public string Message { get; set; }

        public Report()
        {

        }

        public Report(string property, string message, ReportType type)
        {
            this.Property = property;
            this.Message = message;
            this.Type = type;
        }
    }
}
