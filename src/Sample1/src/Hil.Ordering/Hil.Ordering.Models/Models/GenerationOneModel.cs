namespace Hil.Ordering.Models.Models
{
    public class GenerationOneModel : BaseGeneration
    {
        public required string Data { get; set; }

        public override string ToString()
        {
            return $"{Identity} Date : [ {CreationTime.ToLongTimeString()} ]{Data}{Environment.NewLine}";
        }
    }
}
