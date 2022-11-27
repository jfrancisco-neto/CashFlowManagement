namespace Account.Migrator.Options;

public class MigratorOptions
{
    public string Login { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public ICollection<Tuple<string, string>> Claims { get; set; }
}
