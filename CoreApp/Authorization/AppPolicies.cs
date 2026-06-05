namespace CoreApp.Authorization;

public enum AppPolicies
{
    AdminOnly,
    DeanOfficeWorkerOnly,
    LecturerOnly,
    StudentOnly,
    ActiveUser
}

public static class AppPoliciesExtensions
{
    public static string Name(this AppPolicies policy)
    {
        return policy.ToString();
    }
}