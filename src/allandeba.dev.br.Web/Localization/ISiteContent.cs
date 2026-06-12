namespace allandeba.dev.br.Web.Localization;

public interface ISiteContent
{
    MetaContent Meta { get; }
    NavContent Nav { get; }
    LoaderContent Loader { get; }
    HeroContent Hero { get; }
    AboutContent About { get; }
    ExperienceContent Experience { get; }
    ProjectsContent Projects { get; }
    ContactContent Contact { get; }
    StatusBarContent StatusBar { get; }
}

public record MetaContent(string Title, string Description);

public record NavContent(string About, string Experience, string Projects, string Contact);

public record LoaderContent(string Sub);

public record HeroContent(
    string Path,
    string Cmd,
    string Role,
    string ValueHtml,
    string BtnProjects,
    string BtnContact);

public record AboutContent(
    string Path,
    string Cmd,
    string HeadBio,
    string HeadStack,
    string BioHtml,
    string StackHtml,
    string ServerHtml,
    string MetricLabel);

public record JobContent(
    string Company,
    string Role,
    string Period,
    string Context,
    string[] BulletsHtml,
    string[] Tags);

public record ExperienceContent(string Path, string Cmd, JobContent[] Jobs);

public record ProjectsContent(
    string Path,
    string ErrorLoad,
    string Empty,
    string ViewGithub,
    string OpenApp,
    string DiffBefore);

public record ContactContent(
    string Path,
    string Cmd,
    string Handshake,
    string Waiting,
    string Online,
    string Result,
    string Button);

public record StatusBarContent(
    string Hero,
    string About,
    string Experience,
    string Projects,
    string Contact);
