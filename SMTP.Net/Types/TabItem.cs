namespace SMTP.Net.Types;

/// <summary>
/// Represents a tab heading
/// </summary>
public class TabItem
{
    /// <summary>
    /// Gets or sets the title for the tab
    /// </summary>
    public string Title { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the font awesome icon class for the tab
    /// </summary>
    public string IconClass { get; set; } = string.Empty;
}
