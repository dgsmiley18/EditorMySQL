using System;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using MySQLcrud;

namespace MySQLcrud
{
    public class Program
    {
        // Cria um ENUM para seleção de opção do MENU
        enum options
        {
            Insert = 1,
            Delete = 2,
            Update = 3,
            Select = 4,
            Exit = 5
        }
        public static void Main(string[] args)
        {
            // Instancia a config para poder ler as configurações do banco de dados.
            StreamReader config = new StreamReader("config.json");
            string jsondata = config.ReadToEnd();

            // Mescla as configurações do servidor para conexão do servidor MySQL
            var dado = JsonConvert.DeserializeObject<Banco>(jsondata);
            var dados = $"Server={dado.Server};Port={dado.Port};Database={dado.Database};Uid={dado.Uid};Pwd={dado.Pwd}";
            MySqlConnection conexao = new MySqlConnection(dados);
            var comando = conexao.CreateCommand();

            // Cria um MENU a partir de um loop.
            bool executando = false;
            while (!executando)
            {
                Console.WriteLine("Digite a opção desejada:");
                Console.WriteLine("1 - Inserir");
                Console.WriteLine("2 - Deletar");
                Console.WriteLine("3 - Atualizar");
                Console.WriteLine("4 - Selecionar");
                Console.WriteLine("5 - Sair");
                var opcao = int.Parse(Console.ReadLine());
                switch (opcao)
                {
                    case (int)options.Insert:
                        Insert();
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case (int)options.Delete:
                        Delete();
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case (int)options.Update:
                        Update();
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case (int)options.Select:
                        Select();
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case (int)options.Exit:
                        executando = true;
                        break;
                    default:
                        Console.WriteLine("Opção inválida");
                        break;
                }
            }

            void Insert()
            {
                Console.Write("Digite o nome: ");
                var nome = Console.ReadLine();
                Console.Write("Digite o Email: ");
                var email = Console.ReadLine();

                try
                {
                    conexao.Open();
                    comando.CommandText = $"INSERT INTO usuarios (nome, email) VALUES ('{nome}', '{email}')";
                    comando.ExecuteNonQuery();
                    Console.WriteLine("Usuário inserido com sucesso!");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    conexao.Close();
                }
            }

            void Delete()
            {
                Console.Write("Digite o ID do usuario para deletar: ");
                int ID = int.Parse(Console.ReadLine());

                try
                {
                    conexao.Open();
                    comando.CommandText = $"DELETE FROM usuarios WHERE id_usuario = {ID}";
                    comando.ExecuteNonQuery();
                    Console.WriteLine("Usuário deletado com sucesso!");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    conexao.Close();
                }
                
            }

            void Update()
            {
                Console.Write("Digite o ID do usuario que deseja alterar: ");
                var atualizar_id = Console.ReadLine();
                Console.Write("Digite o novo nome do usuario: ");
                var novo_nome = Console.ReadLine();
                Console.Write("Digite o novo Email: ");
                var novo_email = Console.ReadLine();

                try
                {
                    conexao.Open();
                    comando.CommandText = $"UPDATE usuarios SET nome = '{novo_nome}', email = '{novo_email}' WHERE id_usuario = {atualizar_id}";
                    comando.ExecuteNonQuery();
                    Console.WriteLine("Usuário atualizado com sucesso!");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
                finally
                {
                    conexao.Close();
                }
                
            }
            void Select()
            {
                try
                {
                    conexao.Open();
                    comando.CommandText = @$"
                    SELECT * FROM usuarios";
                    MySqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader[0]} Nome: {reader[1]} Email: {reader[2]}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    conexao.Close();
                }
                
            }
        }
    }
}
