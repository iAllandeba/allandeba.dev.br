using MudBlazor;
using MudBlazor.Utilities;

namespace allandeba.dev.br.Web;

public class Configuration
{
    public const string HttpClientName = "allandeba.dev.br";
    public static string BackendUrl { get; set; } = "http://localhost:5025";

    public static MudTheme Theme = new()
    {
        Typography = new Typography
        {
            Default = new DefaultTypography
            {
                FontFamily = new[] { "Outfit", "sans-serif" },
            }
        },
        PaletteLight = new PaletteLight
        {
            Primary = new MudColor("#E2E6E9"),
            PrimaryContrastText = new MudColor("#1A2530"),
            Secondary = new MudColor("#C08C43"),    // gold
            Tertiary = new MudColor("#155A72"),     // teal
            Info = new MudColor("#1A7A9A"),         // teal2
            Background = new MudColor("#EEF1F3"),
            Surface = new MudColor("#E2E6E9"),
            BackgroundGray = new MudColor("#C8D0D8"),
            TextPrimary = new MudColor("#1A2530"),
            TextSecondary = new MudColor("#3A5A6E"),
            TextDisabled = new MudColor("#6A8A9E"),
            Success = new MudColor("#2D7A38"),
            Error = new MudColor("#A03030"),
            AppbarBackground = new MudColor("#E2E6E9"),
            AppbarText = new MudColor("#1A2530"),
            DrawerBackground = new MudColor("#E2E6E9"),
            DrawerText = new MudColor("#1A2530"),
        },
        PaletteDark = new PaletteDark
        {
            Primary = new MudColor("#132230"),
            PrimaryContrastText = new MudColor("#DFE4E0"),
            Secondary = new MudColor("#C08C43"),    // gold
            Tertiary = new MudColor("#1B6F8E"),     // teal
            Info = new MudColor("#2A8FB2"),         // teal2
            Background = new MudColor("#0D1B24"),
            Surface = new MudColor("#132230"),
            BackgroundGray = new MudColor("#1E3040"),
            TextPrimary = new MudColor("#DFE4E0"),
            TextSecondary = new MudColor("#7A9BAE"),
            TextDisabled = new MudColor("#3A5A6E"),
            Success = new MudColor("#5FAD6A"),
            Error = new MudColor("#E07070"),
            AppbarBackground = new MudColor("#132230"),
            AppbarText = new MudColor("#DFE4E0"),
            DrawerBackground = new MudColor("#132230"),
            DrawerText = new MudColor("#DFE4E0"),
        }
    };
}