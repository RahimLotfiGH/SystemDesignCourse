using Hil.Ordering.Models.Models;

namespace Hil.Ordering.Models.Mapping
{
    public static class Mapping
    {
        public static GenerationOneModel ToGenerationOne(this GenerationZeroModel model)
        {
            return new GenerationOneModel
            {
                Identity = model.Identity,
                Data = model.Data,
                CreationTime = DateTime.Now

            };
        }

        public static GenerationTwoModel ToGenerationTwo(this GenerationOneModel model)
        {
            return new GenerationTwoModel
            {
                Identity = model.Identity,
                Data = model.Data,
                CreationTime = DateTime.Now

            };
        }
        public static GenerationZeroModel ToGenerationZero(this GenerationTwoModel model)
        {
            return new GenerationZeroModel
            {
                Identity = model.Identity,
                Data = model.Data,
                CreationTime = DateTime.Now

            };
        }
    }
}
