namespace SolutionShared
{
    public static class InputValidationCheck
    {
        public static bool Nick(string nick)
        {
            return nick != null && nick.Length >= 4 && nick.Length <= 20;
        }
    }
}
