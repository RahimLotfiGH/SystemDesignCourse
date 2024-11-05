namespace Hil.Ordering.Models.Models
{
    public class BaseGeneration
    {
        public BaseGeneration()
        {
            CreationTime = DateTime.Now;
        }

        public long Identity { get; set; }

        public DateTime CreationTime { get; set; }

        public void ChangeIdentity()
        {
            Identity = -1;
        }

    }
}
