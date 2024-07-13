namespace Parking.Model.Validation
{
    public class ModelExceptionValidation : Exception
    {
        public ModelExceptionValidation(string error) : base(error) { }

        public static void When(bool hasErros, string error)
        {
            if (hasErros)
            {
                throw new ModelExceptionValidation(error);
            }
        }
    }
}
