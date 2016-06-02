
namespace ChangeMachine.Core.DataContract
{
    public class EvaluateChangeRequest : AbstractRequest
    {
        public long InputAmount { get; set; }
        public long PriceAmount { get; set; }

        public EvaluateChangeRequest(long inputAmountInCents, long priceAmountInCents)
            : base()
        {
            this.InputAmount = inputAmountInCents;
            this.PriceAmount = priceAmountInCents;
        }

        public override void ValidateRequest()
        {
            if (this.InputAmount < 0)
            {
                base.ErrorList.Add(new Error("InputAmount", "Input amout must be positive"));
            }
            if (this.PriceAmount < 0)
            {
                base.ErrorList.Add(new Error("PriceAmount", "Price must be positive"));
            }
            if (this.InputAmount < this.PriceAmount)
            {
                base.ErrorList.Add(new Error("InputAmount", "Price is greater than amount provided."));
            }
        }
    }
}
