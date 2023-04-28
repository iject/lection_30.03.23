using MySqlConnector;

// Подключение к БД lection_bd30_03_23
var connStr = "Server=localhost;DataBase=lection_bd30_03_23;port=3306;User Id=root;password=";

// Если мы хотим создать БД, то пишем верхнюю строчку без DataBase,
// после создаём cmd CREATE SCHEMA `name`, а в CREATE TABLE помимо названия
// таблицы пишем название созданной нами БД:
// CREATE TABLE `name`.`department`

MySqlConnection conn = new MySqlConnection(connStr);
conn.Open();
MySqlCommand cmd = conn.CreateCommand();
cmd.CommandText = "CREATE TABLE IF NOT EXISTS`department` (" +
                  "id int not null auto_increment primary key, " +
                  "name varchar(200) not null default 'Рога и копыта', " +
                  "location varchar(200) not null)";
cmd.ExecuteNonQuery();

var data = new List<Department>();
data.Add(new Department() { Name = "Департамент 1", Location = "Москва" });
data.Add(new Department() { Location = "Бобруйск" });
foreach (var department in data)
{
    var sqlCmd = $"INSERT INTO `department` ({(department.Name != null ? "name, " : "")}location)" +
                 $"VALUES({(department.Name != null ? "'" + department.Name + "', ": "")}'{department.Location}')";
    cmd.CommandText = sqlCmd;
    cmd.ExecuteNonQuery();
}
conn.Close();

class Department
{
    public int Id { get; set; }
    public string? Name { get; set; } = null;
    public string? Location { get; set; } = null;
}