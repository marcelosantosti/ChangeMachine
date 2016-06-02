
namespace ChangeMachine.Core
{
    public class Error
    {
        public string Property { get; set; }

        public string MensagemError { get; set; }

        public Error(string property, string mensagemError)
        {
            this.Property = property;
            this.MensagemError = mensagemError;
        }
    }
}
