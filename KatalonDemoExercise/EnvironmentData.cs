using NUnit.Framework;

namespace KatalonDemoExercise
{
    class EnvironmentData
    {
        public static string baseUrl { get; } = TestContext.Parameters["baseUrl"];
    }
}
