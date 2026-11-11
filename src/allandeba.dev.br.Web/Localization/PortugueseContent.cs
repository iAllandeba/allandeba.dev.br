namespace allandeba.dev.br.Web.Localization;

public class PortugueseContent : ISiteContent
{
    public MetaContent Meta { get; } = new(
        Title: "Allan Debastiani — Full Stack Engineer C# .NET Blazor",
        Description: "Allan Debastiani — Full Stack Engineer. C#, .NET e Blazor para viagens e hotelaria, integrações REST, performance e observabilidade.");

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
        ValueHtml: "Construo aplicações web completas:<br /><strong>integrações, performance e entregas confiáveis</strong> com C#, .NET e Blazor.",
        BtnProjects: "[ ver projetos ]",
        BtnContact: "[ contato ]");

    public AboutContent About { get; } = new(
        Path: "~/sobre",
        Cmd: "./sobre.md",
        HeadBio: "Sobre mim",
        HeadStack: "Stack & Processo",
        BioHtml: "<strong>Full Stack Engineer</strong> brasileiro com mais de 5 anos de experiência em <strong>viagens e hotelaria</strong>. Desenvolvo integrações e aplicações com <strong>C#, .NET e Blazor</strong>, em sistemas com mais de <strong>10.000 usuários mensais</strong> e software hoteleiro utilizado em <strong>10+ países</strong>.",
        StackHtml: "Na <strong>Montreal Viagens</strong>, liderei a entrega de uma plataforma centralizada de integração com fornecedores hoteleiros, dos requisitos e da arquitetura ao alinhamento entre equipes e à produção. Trabalho com <code>REST APIs</code>, <code>SQL Server</code>, <code>PostgreSQL</code> e CI/CD com <strong>Azure DevOps</strong>, além de <strong>Docker</strong> e <strong>GitHub Actions</strong> nos meus projetos.",
        ServerHtml: "Observabilidade com <strong>OpenTelemetry e Grafana</strong>, mentoria e revisão de código antes das entregas. Também contribuo para fluxos de desenvolvimento assistido por IA com <strong>AWS Kiro</strong> e skills específicas da empresa.",
        MetricLabel: "anos de experiência");

    public ExperienceContent Experience { get; } = new(
        Path: "~/experiencia",
        Cmd: "./experiencia.md",
        Jobs: new[]
        {
            new JobContent(
                Company: "Montreal Viagens",
                Role: "Full Stack Engineer",
                Period: "Mar 2026 → Hoje",
                Context: "Integrações hoteleiras · arquitetura, entrega e observabilidade",
                BulletsHtml: new[]
                {
                    "Liderança da <strong>entrega de ponta a ponta</strong> de uma plataforma centralizada de integração com fornecedores hoteleiros: requisitos, arquitetura, alinhamento entre equipes e produção",
                    "Consultas de disponibilidade: de <strong>~20 s para menos de 500 ms</strong> em testes sob as mesmas condições",
                    "Recursos em <strong>Blazor</strong> para gestão de hotéis e cache, além de seleção de hotéis baseada em regras",
                    "Observabilidade com <strong>OpenTelemetry e Grafana</strong>: traces, métricas e logs para diagnóstico de falhas e acompanhamento da saúde dos serviços",
                    "<strong>Mentoria de desenvolvedores</strong> na definição de escopo, critérios de conclusão e revisão de código antes das entregas em produção",
                    "Contribuição para fluxos de <strong>desenvolvimento assistido por IA</strong> com AWS Kiro e skills específicas da empresa",
                },
                Tags: new[] { ".NET Core", "C#", "Blazor", "SQL Server", "REST APIs", "OpenTelemetry", "Grafana", "AWS Kiro" }),
            new JobContent(
                Company: "Montreal Viagens",
                Role: "Full Stack Engineer",
                Period: "Out 2024 → Jun 2026",
                Context: "10.000+ usuários mensais · setor de viagens",
                BulletsHtml: new[]
                {
                    "Tempo de deploy <strong>30% menor</strong> com automação de CI/CD para aplicações .NET via Azure DevOps",
                    "Economia de <strong>~€800 por equipe</strong> com solução web interna (Blazor + SQL Server)",
                    "Coleção <strong>Postman com logins automatizados para múltiplos perfis</strong>, facilitando acesso a dados entre ambientes, colaboração e onboarding",
                    "Batch de 1.000 registros: de <strong>dias para minutos</strong> com automação .NET",
                },
                Tags: new[] { ".NET Core", "C#", "Blazor", "SQL Server", "Azure DevOps", "REST APIs", "Postman" }),
            new JobContent(
                Company: "Desbravador Software",
                Role: "Full Stack Engineer",
                Period: "Abr 2021 → Out 2024",
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
