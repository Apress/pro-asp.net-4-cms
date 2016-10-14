using System;

namespace CommonLibrary.Permissions
{
    /// <summary>
    /// Defines the possible bucket locations for an embeddable.
    /// </summary>
    [Flags]
    public enum EmbeddablePermissions
    {
        None = 0,
        AllowedInHeader = 2,
        AllowedInPrimaryNav = 4,
        AllowedInContent = 8,
        AllowedInSubNav = 16,
        AllowedInFooter = 32
    }
}
