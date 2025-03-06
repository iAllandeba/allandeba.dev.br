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
            Primary = new MudColor("#202326"),
            PrimaryContrastText = new MudColor("#000000"),
            TextPrimary = new MudColor("#202326"),

            Secondary = new MudColor("#E7E9EB"),
            Tertiary = new MudColor("#C08C43"),

            Background = new MudColor("#314157"),
            Surface = new MudColor("#E7E9EB"),

            AppbarBackground = new MudColor("#E7E9EB"),
            AppbarText = new MudColor("#000000"),

            DrawerBackground = new MudColor("#E7E9EB"),
            DrawerText = new MudColor("#000000"),
        },
        PaletteDark = new PaletteDark
        {
            Primary = new MudColor("#152831"),
            TextPrimary = new MudColor("#bfc4c9"),

            Secondary = new MudColor("#C08c43"),

            Tertiary = new MudColor("#1B6F8E"),

            Background = new MudColor("#202326"),
            Surface = new MudColor("#30353a"),

            AppbarBackground = new MudColor("#bb873e"),
            AppbarText = new MudColor("#E7E9EB"),

            DrawerBackground = new MudColor("#bb873e"),
            DrawerText = new MudColor("#E7E9EB"),
        }
    };
}