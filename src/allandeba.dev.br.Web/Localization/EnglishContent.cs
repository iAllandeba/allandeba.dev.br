namespace allandeba.dev.br.Web.Localization;

public class EnglishContent : ISiteContent
{
    public MetaContent Meta { get; } = new(
        Title: "Allan Debastiani | Backend .NET Engineer | C# and ASP.NET Core",
        Description: "Backend .NET Engineer with 5+ years of experience. C#, ASP.NET Core, APIs, integrations and performance. Also building full-stack applications with Blazor.");

    public NavContent Nav { get; } = new(
        About: "about",
        Experience: "experience",
        Projects: "projects",
        Contact: "contact");

    public LoaderContent Loader { get; } = new(
        Sub: "Backend .NET Engineer");

    public HeroContent Hero { get; } = new(
        Path: "~/whoami",
        Cmd: "./whoami.sh",
        Role: "Backend .NET Engineer · Brazil",
        ValueHtml: "<strong>5+ years with C# and .NET.</strong> APIs, integrations and performance, from architecture to production. I also build with Blazor.",
        ProofHtml: "Led delivery of a hotel-provider integration platform. Availability queries: <strong>~20 s → under 500 ms</strong> in like-for-like tests.",
        BtnProjects: "[ view projects ]",
        BtnContact: "[ discuss a .NET role ]");

    public AboutContent About { get; } = new(
        Path: "~/about",
        Cmd: "./about.md",
        HeadBio: "About me",
        HeadStack: "Stack & Process",
        BioHtml: "Brazilian <strong>Full Stack Engineer</strong> with 5+ years in <strong>travel and hospitality</strong>. I build integrations and applications with <strong>C#, .NET and Blazor</strong> for systems serving <strong>10,000+ monthly users</strong> and hotel software used in <strong>10+ countries</strong>.",
        StackHtml: "At <strong>Montreal Viagens</strong>, I led delivery of a centralized hotel-provider integration platform, from requirements and architecture to cross-team coordination and production. My stack includes <code>REST APIs</code>, <code>SQL Server</code>, <code>PostgreSQL</code> and <strong>Azure DevOps</strong> CI/CD, plus <strong>Docker</strong> and <strong>GitHub Actions</strong> for personal projects.",
        ServerHtml: "Observability with <strong>OpenTelemetry and Grafana</strong>, developer mentoring and pre-release code reviews. I also help shape AI-assisted workflows with <strong>AWS Kiro</strong> and company-specific skills.",
        MetricLabel: "years of experience");

    public ExperienceContent Experience { get; } = new(
        Path: "~/experience",
        Cmd: "./experience.md",
        Jobs: new[]
        {
            new JobContent(
                Company: "Montreal Viagens",
                Role: "Full Stack Engineer",
                Period: "Mar 2026 → Now",
                Context: "Hotel-provider integrations · architecture, delivery and observability",
                BulletsHtml: new[]
                {
                    "Led <strong>end-to-end delivery</strong> of a centralized hotel-provider integration platform: requirements, architecture, cross-team alignment and production release",
                    "Availability queries: from <strong>~20 s to under 500 ms</strong> in like-for-like tests",
                    "Built <strong>Blazor</strong> features for hotel and cache management and rule-based hotel selection",
                    "Added <strong>OpenTelemetry and Grafana</strong> observability: traces, metrics and logs for troubleshooting and service health",
                    "<strong>Mentored developers</strong> on task scope, completion criteria and code reviews before production releases",
                    "Helped shape <strong>AI-assisted development</strong> workflows with AWS Kiro and company-specific skills",
                },
                Tags: new[] { ".NET Core", "C#", "Blazor", "SQL Server", "REST APIs", "OpenTelemetry", "Grafana", "AWS Kiro" }),
            new JobContent(
                Company: "Montreal Viagens",
                Role: "Full Stack Engineer",
                Period: "Oct 2024 → Jun 2026",
                Context: "10,000+ monthly users · travel industry",
                BulletsHtml: new[]
                {
                    "Cut .NET deployment time by <strong>30%</strong> with Azure DevOps CI/CD automation",
                    "Saved <strong>~€800 per team</strong> with an internal web solution (Blazor + SQL Server)",
                    "Built a <strong>Postman collection with automated multi-role logins</strong> for cross-environment data access, collaboration and onboarding",
                    "1,000-record batch: from <strong>days to minutes</strong> with .NET automation",
                },
                Tags: new[] { ".NET Core", "C#", "Blazor", "SQL Server", "Azure DevOps", "REST APIs", "Postman" }),
            new JobContent(
                Company: "Desbravador Software",
                Role: "Full Stack Engineer",
                Period: "Apr 2021 → Oct 2024",
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
        ErrorLoad: "failed to load repositories | check connection",
        Empty: "no repositories found",
        ViewGithub: "View on GitHub",
        OpenApp: "Open app",
        DiffBefore: "Before:");

    public ContactContent Contact { get; } = new(
        Path: "~/contact",
        Cmd: "./contact.sh",
        Intro: "Have a .NET opportunity? Let's talk by email, LinkedIn or message.",
        EmailButton: "[ send email ]",
        LinkedInButton: "[ connect on LinkedIn ↗ ]",
        Button: "[ send a message ]");

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
