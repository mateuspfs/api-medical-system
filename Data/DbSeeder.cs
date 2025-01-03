using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SistemaMedico.Models;

namespace SistemaMedico.Data
{
    public static class DbSeeder
    {
        public static void Seed(SistemaMedicoDBContex dbContext)
        {
            if (!dbContext.Especialidades.Any())
            {
                SeedEspecialidades(dbContext);    
            } 
            if (!dbContext.Doutores.Any())
            {
                SeedDoutores(dbContext);    
            } 
            if (!dbContext.DoutorEspecialidades.Any())
            {
                SeedDoutorEspecialidades(dbContext);    
            }
            if (!dbContext.Tratamentos.Any())
            {
                SeedTratamentos(dbContext);    
            }
            if (!dbContext.Pacientes.Any())
            {
                SeedPacientes(dbContext);    
            }
            if (!dbContext.Etapas.Any())
            {
                SeedEtapas(dbContext);    
            } 
            if(!dbContext.Admins.Any())
            {
                SeedAdmins(dbContext);    
            } if(!dbContext.TratamentosPacientes.Any())
            {
                SeedTratamentoPaciente(dbContext);    
            }
        }

        private static void SeedEspecialidades(SistemaMedicoDBContex dbContext)
        {
            string[] especialidades = {
                "Cardiologia",
                "Dermatologia",
                "Ginecologia",
                "Neurologia",
                "Ortopedia",
                "Pediatria",
                "Psiquiatria",
                "Urologia",
                "Oftalmologia",
                "Oncologia",
                "Endocrinologia",
                "Pneumologia",
                "Radiologia",
                "Gastroenterologia",
                "Hematologia",
                "Nefrologia",
                "Reumatologia",
                "Otorrinolaringologia",
                "Anestesiologia",
                "Cirurgia Geral"
            };

            for(int i = 1; i <= 20; i++)
            {
                dbContext.Especialidades.Add(new EspecialidadeModel { Codigo = "ESP000" + i, Nome = especialidades[i-1] });
            }
            
            dbContext.SaveChanges();
        }

        private static void SeedDoutores(SistemaMedicoDBContex dbContext)
        {
            dbContext.Doutores.AddRange(new List<DoutorModel>
            {
                new DoutorModel
                {
                    Nome = "Dr. João Silva",
                    Email = "joao.silva@example.com",
                    Telefone = "11987654321",
                    Cpf = "12345678900",
                    Endereco = "Rua A, 123",
                    DocumentoNome = "doc-example.png"
                },
                new DoutorModel
                {
                    Nome = "Dra. Ana Santos",
                    Email = "ana.santos@example.com",
                    Telefone = "12987654321",
                    Cpf = "23456789011",
                    Endereco = "Av. B, 456",
                    DocumentoNome = "doc-example.png"
                },
                new DoutorModel
                {
                    Nome = "Dr. Pedro Costa",
                    Email = "pedro.costa@example.com",
                    Telefone = "13987654321",
                    Cpf = "34567890122",
                    Endereco = "Rua C, 789",
                    DocumentoNome = "doc-example.png"
                },
                new DoutorModel
                {
                    Nome = "Dra. Maria Oliveira",
                    Email = "maria.oliveira@example.com",
                    Telefone = "14987654321",
                    Cpf = "45678901233",
                    Endereco = "Av. D, 1011",
                    DocumentoNome = "doc-example.png"
                },
                new DoutorModel
                {
                    Nome = "Dr. Luiz Pereira",
                    Email = "luiz.pereira@example.com",
                    Telefone = "15987654321",
                    Cpf = "56789012344",
                    Endereco = "Rua E, 1213",
                    DocumentoNome = "doc-example.png"
                },
                new DoutorModel
                {
                    Nome = "Dra. Julia Fernandes",
                    Email = "julia.fernandes@example.com",
                    Telefone = "16987654321",
                    Cpf = "67890123455",
                    Endereco = "Av. F, 1415",
                    DocumentoNome = "doc-example.png"
                },
                new DoutorModel
                {
                    Nome = "Dr. Carlos Souza",
                    Email = "carlos.souza@example.com",
                    Telefone = "17987654321",
                    Cpf = "78901234566",
                    Endereco = "Rua G, 1617",
                    DocumentoNome = "doc-example.png"
                },
                new DoutorModel
                {
                    Nome = "Dra. Camila Lima",
                    Email = "camila.lima@example.com",
                    Telefone = "18987654321",
                    Cpf = "89012345677",
                    Endereco = "Av. H, 1819",
                    DocumentoNome = "doc-example.png"
                },
                new DoutorModel
                {
                    Nome = "Dr. André Almeida",
                    Email = "andre.almeida@example.com",
                    Telefone = "19987654321",
                    Cpf = "90123456788",
                    Endereco = "Rua I, 2021",
                    DocumentoNome = "doc-example.png"
                },
                  new DoutorModel
                {
                    Nome = "Dra. Fernanda Martins",
                    Email = "fernanda.martins@example.com",
                    Telefone = "20987654321",
                    Cpf = "01234567899",
                    Endereco = "Av. J, 2223",
                    DocumentoNome = "doc-example.png"
                },
                new DoutorModel
                {
                    Nome = "Dr. Ricardo Barbosa",
                    Email = "ricardo.barbosa@example.com",
                    Telefone = "21987654321",
                    Cpf = "12345678901",
                    Endereco = "Rua K, 2425",
                    DocumentoNome = "doc-example.png"
                },
                new DoutorModel
                {
                    Nome = "Dra. Patricia Correia",
                    Email = "patricia.correia@example.com",
                    Telefone = "22987654321",
                    Cpf = "23456789012",
                    Endereco = "Av. L, 2627",
                    DocumentoNome = "doc-example.png"
                },
                new DoutorModel
                {
                    Nome = "Dr. Gabriel Mendes",
                    Email = "gabriel.mendes@example.com",
                    Telefone = "23987654321",
                    Cpf = "34567890123",
                    Endereco = "Rua M, 2829",
                    DocumentoNome = "doc-example.png"
                },
                new DoutorModel
                {
                    Nome = "Dra. Mariana Carvalho",
                    Email = "mariana.carvalho@example.com",
                    Telefone = "24987654321",
                    Cpf = "45678901234",
                    Endereco = "Av. N, 3031",
                    DocumentoNome = "doc-example.png"
                },
                new DoutorModel
                {
                    Nome = "Dr. Rafael Santos",
                    Email = "rafael.santos@example.com",
                    Telefone = "25987654321",
                    Cpf = "56789012345",
                    Endereco = "Rua O, 3233",
                    DocumentoNome = "doc-example.png"
                },
                new DoutorModel
                {
                    Nome = "Dra. Amanda Rodrigues",
                    Email = "amanda.rodrigues@example.com",
                    Telefone = "26987654321",
                    Cpf = "67890123456",
                    Endereco = "Av. P, 3435",
                    DocumentoNome = "doc-example.png"
                },
                new DoutorModel
                {
                    Nome = "Dr. Lucas Oliveira",
                    Email = "lucas.oliveira@example.com",
                    Telefone = "27987654321",
                    Cpf = "78901234567",
                    Endereco = "Rua Q, 3637",
                    DocumentoNome = "doc-example.png"
                },
                new DoutorModel
                {
                    Nome = "Dra. Isabela Pereira",
                    Email = "isabela.pereira@example.com",
                    Telefone = "28987654321",
                    Cpf = "89012345678",
                    Endereco = "Av. R, 3839",
                    DocumentoNome = "doc-example.png"
                },
                new DoutorModel
                {
                    Nome = "Dr. Vinicius Marinho",
                    Email = "vm_espindola@hotmail.com",
                    Telefone = "30987654321",
                    Cpf = "01234567896",
                    Endereco = "Av. U, 4243",
                    DocumentoNome = "doc-example.png"
                },
                new DoutorModel
                {
                    Nome = "Dr. Mateus Sampaio",
                    Email = "mateuspfs3@gmail.com",
                    Telefone = "30987654321",
                    Cpf = "01234567836",
                    Endereco = "Av. V, 4243",
                    DocumentoNome = "doc-example.png"
                }
            });

            dbContext.SaveChanges();
        }

        private static void SeedDoutorEspecialidades(SistemaMedicoDBContex dbContext)
        {
            int[] valores = {
                    1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                    11, 12, 13, 14, 15, 16, 17, 18, 19, 20
                };

            for (int i = 0; i < valores.Length; i++)
            {
                dbContext.DoutorEspecialidades.Add(new DoutorEspecialidadeModel
                {
                    DoutorId = valores[i],
                    EspecialidadeId = valores[i]
                });
            }

            dbContext.SaveChanges();
        }

        private static void SeedPacientes(SistemaMedicoDBContex dbContext)
        {
            string[] codigos = {
                    "PAC001",
                    "PAC002",
                    "PAC003",
                    "PAC004",
                    "PAC005",
                    "PAC006",
                    "PAC007",
                    "PAC008",
                    "PAC009",
                    "PAC010",
                    "PAC011",
                    "PAC012",
                    "PAC013",
                    "PAC014",
                    "PAC015",
                    "PAC016",
                    "PAC017",
                    "PAC018",
                    "PAC019",
                    "PAC020"
                };

            string[] nomes = {
                    "Mateus Sampaio",
                    "José Silva",
                    "Ana Santos",
                    "Carlos Souza",
                    "Juliana Lima",
                    "Fernando Martins",
                    "Camila Oliveira",
                    "Rafaela Pereira",
                    "Lucas Rodrigues",
                    "Mariana Costa",
                    "Pedro Almeida",
                    "Carolina Santos",
                    "Gabriel Oliveira",
                    "Amanda Fernandes",
                    "Gustavo Silva",
                    "Isabela Pereira",
                    "Tiago Martins",
                    "Laura Souza",
                    "Diego Santos",
                    "Natália Lima"
                };

            string[] emails = {
                    "mateupfs123@gmail.com",
                    "jose.silva@example.com",
                    "ana.santos@example.com",
                    "carlos.souza@example.com",
                    "juliana.lima@example.com",
                    "fernando.martins@example.com",
                    "camila.oliveira@example.com",
                    "rafaela.pereira@example.com",
                    "lucas.rodrigues@example.com",
                    "mariana.costa@example.com",
                    "pedro.almeida@example.com",
                    "carolina.santos@example.com",
                    "gabriel.oliveira@example.com",
                    "amanda.fernandes@example.com",
                    "gustavo.silva@example.com",
                    "isabela.pereira@example.com",
                    "tiago.martins@example.com",
                    "laura.souza@example.com",
                    "diego.santos@example.com",
                    "natalia.lima@example.com"
                };

            string[] telefones = {
                    "11987654321",
                    "12987654321",
                    "13987654321",
                    "14987654321",
                    "15987654321",
                    "16987654321",
                    "17987654321",
                    "18987654321",
                    "19987654321",
                    "20987654321",
                    "21987654321",
                    "22987654321",
                    "23987654321",
                    "24987654321",
                    "25987654321",
                    "26987654321",
                    "27987654321",
                    "28987654321",
                    "29987654321",
                    "30987654321"
                };

            string[] cpfs = {
                    "39491150804",
                    "87654321011",
                    "76543210922",
                    "65432109833",
                    "54321098744",
                    "43210987655",
                    "32109876566",
                    "21098765477",
                    "10987654388",
                    "09876543299",
                    "98765432101",
                    "87654321012",
                    "76543210923",
                    "65432109834",
                    "54321098745",
                    "43210987656",
                    "32109876567",
                    "21098765478",
                    "10987654389",
                    "09876543290"
                };

            string[] enderecos = {
                    "Rua A, 123",
                    "Av. B, 456",
                    "Rua C, 789",
                    "Av. D, 1011",
                    "Rua E, 1213",
                    "Av. F, 1415",
                    "Rua G, 1617",
                    "Av. H, 1819",
                    "Rua I, 2021",
                    "Av. J, 2223",
                    "Rua K, 2425",
                    "Av. L, 2627",
                    "Rua M, 2829",
                    "Av. N, 3031",
                    "Rua O, 3233",
                    "Av. P, 3435",
                    "Rua Q, 3637",
                    "Av. R, 3839",
                    "Rua S, 4041",
                    "Av. T, 4243"
                };

            for (int i = 0; i < codigos.Length; i++)
            {
                dbContext.Pacientes.Add(new PacienteModel
                {
                    Codigo = codigos[i],
                    Nome = nomes[i],
                    Email = emails[i],
                    Telefone = telefones[i],
                    Cpf = cpfs[i],
                    Endereco = enderecos[i]
                });
            }

            dbContext.SaveChanges();
        }

        private static void SeedTratamentos(SistemaMedicoDBContex dbContext)
        {
            string[] nomes = {
                    "Tratamento Cardíaco",
                    "Tratamento Dermatológico",
                    "Tratamento Ginecológico",
                    "Tratamento Neurológico",
                    "Tratamento Ortopédico",
                    "Tratamento Pediátrico",
                    "Tratamento Psiquiátrico",
                    "Tratamento Urológico",
                    "Tratamento Oftalmológico",
                    "Tratamento Oncológico",
                    "Tratamento Endocrinológico",
                    "Tratamento Pneumológico",
                    "Tratamento Radiológico",
                    "Tratamento Gastroenterológico",
                    "Tratamento Hematológico",
                    "Tratamento Nefrológico",
                    "Tratamento Reumatológico",
                    "Tratamento Otorrinolaringológico",
                    "Tratamento Anestésico",
                    "Tratamento Cirúrgico Geral"
                };

            int[] tempos = {
                10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65,
                70, 75, 80, 85, 90, 95, 100, 105
                };

            int[] especialidadesIds = {
                    1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15,
                    16, 17, 18, 19, 20
                };

            for (int i = 0; i < nomes.Length; i++)
            {
                dbContext.Tratamentos.Add(new TratamentoModel
                {
                    Nome = nomes[i],
                    Tempo = tempos[i],
                    EspecialidadeId = especialidadesIds[i]
                });
            }
            dbContext.SaveChanges();
        }

        private static void SeedEtapas(SistemaMedicoDBContex dbContext)
        {
            int[] tratamentosIds = {
                    1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                    11, 12, 13, 14, 15, 16, 17, 18, 19, 20
                };

            for (int i = 0; i < tratamentosIds.Length; i++)
            {
                for (int j = 1; j <= 5; j++)
                {
                    dbContext.Etapas.Add(new EtapaModel
                    {
                        Titulo = $"Etapa {j} do Tratamento {tratamentosIds[i]}",
                        Descricao = $"Esta é a descrição da etapa {j} do tratamento {tratamentosIds[i]}.",
                        Numero = j,
                        TratamentoId = tratamentosIds[i]
                    });
                }
            }

            dbContext.SaveChanges();
        }

        private static void SeedAdmins(SistemaMedicoDBContex dbContext)
        {
            string[] nomes = {
                    "Admin1",
                    "Admin2",
                    "Admin3",
                    "Admin4",
                    "Admin5",
                    "Admin6",
                    "Admin7",
                    "Admin8",
                    "Admin9",
                    "Admin10",
                    "Admin11",
                    "Admin12",
                    "Admin13",
                    "Admin14",
                    "Admin15",
                    "Admin16",
                    "Admin17",
                    "Admin18",
                    "Admin19",
                    "Admin20",
                    "Vinicius M. Espíndola",
                    "Mateus Sampaio"
                };

            string[] emails = {
                    "admin1@example.com",
                    "admin2@example.com",
                    "admin3@example.com",
                    "admin4@example.com",
                    "admin5@example.com",
                    "admin6@example.com",
                    "admin7@example.com",
                    "admin8@example.com",
                    "admin9@example.com",
                    "admin10@example.com",
                    "admin11@example.com",
                    "admin12@example.com",
                    "admin13@example.com",
                    "admin14@example.com",
                    "admin15@example.com",
                    "admin16@example.com",
                    "admin17@example.com",
                    "admin18@example.com",
                    "admin19@example.com",
                    "admin20@example.com",
                    "viniciusmarinhoespindola@gmail.com",
                    "devmateuspfs@gmail.com"
                };

            // Adicionando 20 admins
            for (int i = 0; i < nomes.Length; i++)
            {
                dbContext.Admins.Add(new AdminModel
                {
                    Name = nomes[i],
                    Email = emails[i]
                });
            }

            dbContext.SaveChanges();
        }

        public static void SeedTratamentoPaciente(SistemaMedicoDBContex dbContext)
        {
            List<PacienteModel> pacientes = dbContext.Pacientes.ToList();
            List<EtapaModel> etapas = dbContext.Etapas.ToList();

            Random rand = new Random();

            foreach (var paciente in pacientes)
            {
                int numTratamentos = rand.Next(1, 4);

                for (int i = 0; i < numTratamentos; i++)
                {
                    EtapaModel etapa = etapas[rand.Next(etapas.Count)];


                    TratamentoPacienteModel tratamento = new()
                    {
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                        Status = false,
                        PacienteId = paciente.Id,
                        EtapaId = etapa.Id
                    };

                    dbContext.TratamentosPacientes.Add(tratamento);
                    dbContext.SaveChanges();

                    PagamentoModel pagamento = new()
                    {
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                        TratamentoPacienteId = tratamento.Id
                    };

                    dbContext.Pagamentos.Add(pagamento);
                    dbContext.SaveChanges();
                }
            }
        }

        private static string GenerateRandomCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 6)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
