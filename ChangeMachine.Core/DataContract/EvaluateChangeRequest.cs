
namespace ChangeMachine.Core.DataContract
{
    public sealed class EvaluateChangeRequest : AbstractRequest
    {
        public long InputAmount { get; set; }
        public long PriceAmount { get; set; }

        public EvaluateChangeRequest() : base()
        {

        }

        public EvaluateChangeRequest(long inputAmountInCents, long priceAmountInCents)
            : base()
        {
            this.InputAmount = inputAmountInCents;
            this.PriceAmount = priceAmountInCents;
        }

        internal override void ValidateRequest()
        {
            if (this.InputAmount < 0)
            {
                base.ErrorList.Add(new Report("InputAmount", "Input amout must be positive", ReportType.ERROR));
            }
            if (this.PriceAmount < 0)
            {
                base.ErrorList.Add(new Report("PriceAmount", "Price must be positive", ReportType.ERROR));
            }
            if (this.InputAmount < this.PriceAmount)
            {
                base.ErrorList.Add(new Report("InputAmount", "Price is greater than amount provided.", ReportType.ERROR));
            }
        }
    }
}
