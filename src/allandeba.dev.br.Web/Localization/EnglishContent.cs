namespace allandeba.dev.br.Web.Localization;

public class EnglishContent : ISiteContent
{
    public MetaContent Meta { get; } = new(
        Title: "Allan Debastiani — Full Stack C# .NET Blazor Engineer",
        Description: "Allan Debastiani — Full Stack Engineer, Brazil. Complete web applications with C#, .NET, Blazor, PostgreSQL and Docker.");

    public NavContent Nav { get; } = new(
        About: "about",
        Experience: "experience",
        Projects: "projects",
        Contact: "contact");

    public LoaderContent Loader { get; } = new(
        Sub: "Full Stack Engineer");

    public HeroContent Hero { get; } = new(
        Path: "~/whoami",
        Cmd: "./whoami.sh",
        Role: "Full Stack Engineer · Brazil",
        ValueHtml: "I build complete web applications:<br /><strong>back, front, database and deploy</strong> with C# and .NET.",
        BtnProjects: "[ view projects ]",
        BtnContact: "[ contact ]");

    public AboutContent About { get; } = new(
        Path: "~/about",
        Cmd: "./about.md",
        HeadBio: "About me",
        HeadStack: "Stack & Process",
        BioHtml: "Brazilian <strong>Full Stack Engineer</strong> passionate about building software that solves real problems elegantly and reliably. I work with <strong>C# and .NET</strong> at the core, always pursuing clean code, solid architecture and code that is <em>easy to evolve</em>.",
        StackHtml: "Experience across the whole cycle: from database modeling to deployment via <strong>Docker</strong> and <strong>GitHub Actions</strong>. <code>PostgreSQL</code> with <code>Entity Framework Core</code>, automatic migrations, authentication via <code>ASP.NET Core Identity</code> and robust RESTful APIs.",
        ServerHtml: "On the server: containerized architecture, separate environments for staging and production.",
        MetricLabel: "years of experience");

    public ExperienceContent Experience { get; } = new(
        Path: "~/experience",
        Cmd: "./experience.md",
        Jobs: new[]
        {
            new JobContent(
                Company: "Montreal Viagens",
                Role: "Full Stack Engineer",
                Period: "Sep 2024 → Now",
                Context: "10,000+ monthly users · travel industry",
                BulletsHtml: new[]
                {
                    "Deploys <strong>30% faster and safer</strong> via end-to-end CI/CD with Azure DevOps",
                    "Saved <strong>~€800 per team</strong> with an internal web solution (Blazor + SQL Server)",
                    "Robust RESTful APIs for integrations with <strong>third-party systems</strong>",
                    "1,000-record batch: from <strong>days to minutes</strong> with .NET automation",
                },
                Tags: new[] { ".NET Core", "C#", "Blazor", "SQL Server", "Azure DevOps", "REST APIs" }),
            new JobContent(
                Company: "Desbravador Software",
                Role: "Full Stack Engineer",
                Period: "Mar 2021 → Sep 2024",
                Context: "Hotel app in production in 10+ countries · thousands of users",
                BulletsHtml: new[]
                {
                    "Messaging <strong>40% faster</strong> with an automated dispatch system",
                    "PostgreSQL queries <strong>30% faster</strong> via optimized CTEs",
                    "<strong>Mentored</strong> junior devs in Clean Code, architecture and best practices",
                    "<strong>Cross-platform</strong> systems integration with REST APIs",
                },
                Tags: new[] { ".NET Core", "C#", "Delphi", "PostgreSQL", "REST APIs", "JavaScript" }),
        });

    public ProjectsContent Projects { get; } = new(
        Path: "~/projects",
        ErrorLoad: "failed to load repositories — check connection",
        Empty: "no repositories found",
        ViewGithub: "View on GitHub",
        OpenApp: "Open app",
        DiffBefore: "Before:");

    public ContactContent Contact { get; } = new(
        Path: "~/contact",
        Cmd: "./contact.sh",
        Handshake: "Starting handshake...",
        Waiting: "[ waiting ]",
        Online: "[ online ]",
        Result: "All channels online. Ready to connect.",
        Button: "./send-message");

    public StatusBarContent StatusBar { get; } = new(
        Hero: "whoami",
        About: "about",
        Experience: "experience",
        Projects: "projects",
        Contact: "contact");

    public AuthContent Auth { get; } = new(
        LoginFailed: "Could not sign in",
        RegisterFailed: "Could not create the account",
        RegisterSucceeded: "Account created successfully");
}
