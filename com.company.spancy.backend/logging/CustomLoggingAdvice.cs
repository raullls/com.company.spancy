using Spring.Aspects.Logging;

namespace com.company.spancy.backend.logging
{
    public class CustomLoggingAdvice : SimpleLoggingAdvice
    {
        public string UniqueIdentifier { get; set; }
        protected override string CreateUniqueIdentifier()
        {
            this.UniqueIdentifier = base.CreateUniqueIdentifier();
            return this.UniqueIdentifier;
        }
    }
}