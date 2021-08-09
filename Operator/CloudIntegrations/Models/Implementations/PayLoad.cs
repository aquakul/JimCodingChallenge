namespace Operator.CloudIntegrations.Models
{
    /// <summary>
    /// Payload. Works with AWS Lambda. Need to be revisited for future Cloud integrations
    /// 
    /// </summary>
    public class PayLoad
    {
        public int[] Input { get; set; }
        public string Op { get; set; }
    }
}
