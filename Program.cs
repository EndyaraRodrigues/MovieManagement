// See https://aka.ms/new-console-template for more information
using MovieManagement.Domain.Interfaces;
using MovieManagement.Data.Repositories;
using MovieManagement.Repositories.SQLite;
using MovieManagement.Business.Services;

// Muda para false para usar memória
bool usarSQLite = true;

ICategoryRepository categoryRepo;
IDirectorRepository directorRepo;
IMovieRepository movieRepo;

if (usarSQLite)
{
    categoryRepo = new CategorySQLiteRepository();
    directorRepo = new DirectorSQLiteRepository();
    movieRepo    = new MovieSQLiteRepository();
}
else
{
    categoryRepo = new CategoryRepository();
    directorRepo = new DirectorRepository();
    movieRepo    = new MovieRepository();
}

var movieService    = new MovieServices(movieRepo, categoryRepo, directorRepo);
var categoryService = new CategoryServices(categoryRepo);
var directorService = new DirectorServices(directorRepo);

bool sair = false;
while (!sair)
{
    Console.Clear();
    Console.WriteLine("     MovieManagement       ");
    Console.WriteLine("══════════════════════════════");
    Console.WriteLine("1. Filmes");
    Console.WriteLine("2. Categorias");
    Console.WriteLine("3. Realizadores");
    Console.WriteLine("0. Sair");
    Console.Write("\nEscolha: ");
    switch (Console.ReadLine())
    {
        case "1": MenuFilmes(); break;
        case "2": MenuCategorias(); break;
        case "3": MenuRealizadores(); break;
        case "0": sair = true; break;
    }
}

void MenuFilmes()
{
    bool voltar = false;
    while (!voltar)
    {
        Console.Clear();
        Console.WriteLine("── Filmes ──");
        Console.WriteLine("1. Adicionar  2. Listar  3. Procurar  4. Remover  0. Voltar");
        Console.Write("Escolha: ");
        switch (Console.ReadLine())
        {
            case "1": AdicionarFilme(); break;
            case "2": ListarFilmes(); break;
            case "3": ProcurarFilme(); break;
            case "4": RemoverFilme(); break;
            case "0": voltar = true; break;
        }
    }
}

void AdicionarFilme()
{
    Console.Clear();
    Console.WriteLine("── Categorias disponíveis ──");
    categoryService.Listar().ForEach(c => Console.WriteLine($"  [{c.ID}] {c.Nome}"));
    Console.WriteLine("\n── Realizadores disponíveis ──");
    directorService.Listar().ForEach(d => Console.WriteLine($"  [{d.ID}] {d.Nome} ({d.Pais})"));

    Console.WriteLine("\n── Adicionar Filme ──");
    Console.Write("Título: "); var titulo = Console.ReadLine() ?? "";
    Console.Write("Ano: "); int.TryParse(Console.ReadLine(), out int ano);
    Console.Write("Língua: "); var lingua = Console.ReadLine() ?? "";
    Console.Write("Classificação (0-5): "); int.TryParse(Console.ReadLine(), out int clas);
    Console.Write("ID da Categoria: "); int.TryParse(Console.ReadLine(), out int catId);
    Console.Write("ID do Realizador: "); int.TryParse(Console.ReadLine(), out int dirId);

    try
    {
        movieService.Adicionar(titulo, ano, lingua, clas, catId, dirId);
        Console.WriteLine("✅ Filme adicionado!");
    }
    catch (Exception ex) { Console.WriteLine($"❌ {ex.Message}"); }
    Pausa();
}

void ListarFilmes()
{
    Console.Clear();
    var filmes = movieService.Listar();
    if (filmes.Count == 0)
        Console.WriteLine("Nenhum filme.");
    else
        filmes.ForEach(f => Console.WriteLine(
            $"[{f.ID}] {f.Titulo} ({f.Ano}) | {f.Lingua} | ⭐{f.Classificacao}/5 | Cat:{f.CategoriaID} | Real:{f.RealizadorID}"));
    Pausa();
}

void ProcurarFilme()
{
    Console.Clear();
    Console.Write("Título: ");
    var f = movieService.ProcurarPorTitulo(Console.ReadLine() ?? "");
    Console.WriteLine(f == null ? "❌ Não encontrado." :
        $"✅ [{f.ID}] {f.Titulo} ({f.Ano}) | {f.Lingua} | ⭐{f.Classificacao}/5");
    Pausa();
}

void RemoverFilme()
{
    Console.Clear();
    movieService.Listar().ForEach(f => Console.WriteLine($"[{f.ID}] {f.Titulo}"));
    Console.Write("ID: "); int.TryParse(Console.ReadLine(), out int id);
    try { movieService.Remover(id); Console.WriteLine("✅ Removido!"); }
    catch (Exception ex) { Console.WriteLine($"❌ {ex.Message}"); }
    Pausa();
}

void MenuCategorias()
{
    bool voltar = false;
    while (!voltar)
    {
        Console.Clear();
        Console.WriteLine("── Categorias ──");
        Console.WriteLine("1. Adicionar  2. Listar  3. Remover  0. Voltar");
        Console.Write("Escolha: ");
        switch (Console.ReadLine())
        {
            case "1":
                Console.Write("Nome: "); var nome = Console.ReadLine() ?? "";
                try { categoryService.Adicionar(0, nome); Console.WriteLine("✅ Adicionada!"); }
                catch (Exception ex) { Console.WriteLine($"❌ {ex.Message}"); }
                Pausa(); break;
            case "2":
                Console.Clear();
                categoryService.Listar().ForEach(c => Console.WriteLine($"[{c.ID}] {c.Nome}"));
                Pausa(); break;
            case "3":
                Console.Clear();
                categoryService.Listar().ForEach(c => Console.WriteLine($"[{c.ID}] {c.Nome}"));
                Console.Write("ID: "); int.TryParse(Console.ReadLine(), out int id);
                try { categoryService.Remover(id); Console.WriteLine("✅ Removida!"); }
                catch (Exception ex) { Console.WriteLine($"❌ {ex.Message}"); }
                Pausa(); break;
            case "0": voltar = true; break;
        }
    }
}

void MenuRealizadores()
{
    bool voltar = false;
    while (!voltar)
    {
        Console.Clear();
        Console.WriteLine("── Realizadores ──");
        Console.WriteLine("1. Adicionar  2. Listar  3. Remover  0. Voltar");
        Console.Write("Escolha: ");
        switch (Console.ReadLine())
        {
            case "1":
                Console.Write("Nome: "); var nome = Console.ReadLine() ?? "";
                Console.Write("País: "); var pais = Console.ReadLine() ?? "";
                try { directorService.Adicionar(0, nome, pais); Console.WriteLine("✅ Adicionado!"); }
                catch (Exception ex) { Console.WriteLine($"❌ {ex.Message}"); }
                Pausa(); break;
            case "2":
                Console.Clear();
                directorService.Listar().ForEach(d => Console.WriteLine($"[{d.ID}] {d.Nome} ({d.Pais})"));
                Pausa(); break;
            case "3":
                Console.Clear();
                directorService.Listar().ForEach(d => Console.WriteLine($"[{d.ID}] {d.Nome}"));
                Console.Write("ID: "); int.TryParse(Console.ReadLine(), out int id);
                try { directorService.Remover(id); Console.WriteLine("✅ Removido!"); }
                catch (Exception ex) { Console.WriteLine($"❌ {ex.Message}"); }
                Pausa(); break;
            case "0": voltar = true; break;
        }
    }
}

void Pausa()
{
    Console.WriteLine("\nENTER para continuar...");
    Console.ReadLine();
}
