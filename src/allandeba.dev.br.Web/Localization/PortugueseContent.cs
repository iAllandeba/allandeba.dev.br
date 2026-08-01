namespace allandeba.dev.br.Web.Localization;

public class PortugueseContent : ISiteContent
{
    public MetaContent Meta { get; } = new(
        Title: "Allan Debastiani — Full Stack Engineer C# .NET Blazor",
        Description: "Allan Debastiani — Full Stack Engineer Brasil. Aplicações web completas com C#, .NET, Blazor, PostgreSQL e Docker.");

    public NavContent Nav { get; } = new(
        About: "sobre",
        Experience: "experiência",
        Projects: "projetos",
        Contact: "contato");

    public LoaderContent Loader { get; } = new(
        Sub: "Full Stack Engineer");

    public HeroContent Hero { get; } = new(
        Path: "~/quem-sou-eu",
        Cmd: "./quem-sou-eu.sh",
        Role: "Full Stack Engineer · Brasil",
        ValueHtml: "Construo aplicações web completas:<br /><strong>back, front, banco e deploy</strong> com C# e .NET.",
        BtnProjects: "[ ver projetos ]",
        BtnContact: "[ contato ]");

    public AboutContent About { get; } = new(
        Path: "~/sobre",
        Cmd: "./sobre.md",
        HeadBio: "Sobre mim",
        HeadStack: "Stack & Processo",
        BioHtml: "<strong>Full Stack Engineer</strong> brasileiro apaixonado por construir software que resolve problemas reais de forma elegante e confiável. Atuo com <strong>C# e .NET</strong> como base, sempre buscando código limpo, arquitetura sólida e código <em>fácil de evoluir</em>.",
        StackHtml: "Experiência com todo o ciclo: da modelagem de banco até o deploy via <strong>Docker</strong> e <strong>GitHub Actions</strong>. <code>PostgreSQL</code> com <code>Entity Framework Core</code>, migrations automáticas, autenticação via <code>ASP.NET Core Identity</code> e APIs RESTful robustas.",
        ServerHtml: "No servidor, arquitetura containerizada, ambientes separados para staging e produção.",
        MetricLabel: "anos de experiência");

    public ExperienceContent Experience { get; } = new(
        Path: "~/experiencia",
        Cmd: "./experiencia.md",
        Jobs: new[]
        {
            new JobContent(
                Company: "Montreal Viagens",
                Role: "Full Stack Engineer",
                Period: "Set 2024 → Hoje",
                Context: "10.000+ usuários mensais · setor de viagens",
                BulletsHtml: new[]
                {
                    "Deploy <strong>30% mais rápido e seguro</strong> via CI/CD end-to-end com Azure DevOps",
                    "Economia de <strong>~€800 por equipe</strong> com solução web interna (Blazor + SQL Server)",
                    "APIs RESTful robustas para integrações com <strong>sistemas de terceiros</strong>",
                    "Batch de 1.000 registros: de <strong>dias para minutos</strong> com automação .NET",
                },
                Tags: new[] { ".NET Core", "C#", "Blazor", "SQL Server", "Azure DevOps", "REST APIs" }),
            new JobContent(
                Company: "Desbravador Software",
                Role: "Full Stack Engineer",
                Period: "Mar 2021 → Set 2024",
                Context: "App hoteleiro em produção em 10+ países · milhares de usuários",
                BulletsHtml: new[]
                {
                    "Mensageria <strong>40% mais rápida</strong> com sistema de envio automatizado",
                    "Queries PostgreSQL <strong>30% mais velozes</strong> via CTEs otimizadas",
                    "<strong>Mentoria</strong> de devs júnior em Clean Code, arquitetura e melhores práticas",
                    "Integração de sistemas <strong>cross-plataforma</strong> com REST APIs",
                },
                Tags: new[] { ".NET Core", "C#", "Delphi", "PostgreSQL", "REST APIs", "JavaScript" }),
        });

    public ProjectsContent Projects { get; } = new(
        Path: "~/projetos",
        ErrorLoad: "falha ao carregar repositórios — verifique a conexão",
        Empty: "nenhum repositório encontrado",
        ViewGithub: "Ver no GitHub",
        OpenApp: "Abrir aplicação",
        DiffBefore: "Antes:");

    public ContactContent Contact { get; } = new(
        Path: "~/contato",
        Cmd: "./contato.sh",
        Handshake: "Iniciando handshake...",
        Waiting: "[ aguardando ]",
        Online: "[ online ]",
        Result: "Todos os canais online. Pronto para conectar.",
        Button: "./enviar-mensagem");

    public StatusBarContent StatusBar { get; } = new(
        Hero: "quem-sou-eu",
        About: "sobre",
        Experience: "experiencia",
        Projects: "projetos",
        Contact: "contato");

    public AuthContent Auth { get; } = new(
        LoginFailed: "Não foi possível efetuar o login",
        RegisterFailed: "Não foi possível criar o usuário",
        RegisterSucceeded: "Usuário criado com sucesso");
}
