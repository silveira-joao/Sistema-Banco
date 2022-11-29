using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DigiBank.Classes
{
    public class Layout
    {
        private static List<Pessoa> pessoas = new List<Pessoa>();
        private static int opcao = 0;
        
        public static void TelaPrincipal()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Clear();

            Console.WriteLine("                                                                 ");
            Console.WriteLine("                    Digite a Opção Desejada:                     ");
            Console.WriteLine("                    ==========================                   ");
            Console.WriteLine("                    1 - Criar Conta                              ");
            Console.WriteLine("                    ==========================                   ");
            Console.WriteLine("                    2 - Entrar com CPF e Senha                   ");
            Console.WriteLine("                    ==========================                   ");

            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1: 
                    TelaCriarConta(); 
                    break;
                case 2: 
                    TelaLogin(); 
                    break;
                default:
                    Console.WriteLine("                    Opção Inválida!                                ");
                    Console.WriteLine("                    ============================                   ");
                    break;

            }
        }

        private static void TelaCriarConta()
        {
            Console.Clear();

            Console.WriteLine("                                                                 ");
            Console.Write("                    Digite seu nome: ");
            string nome = Console.ReadLine();
            Console.WriteLine("                    ===============================                   ");
            Console.Write("                    Digite seu CPF: ");
            string cpf = Console.ReadLine();
            Console.WriteLine("                    ===============================                   ");
            Console.Write("                    Digite sua Senha: ");
            string senha = Console.ReadLine();
            Console.WriteLine("                    ===============================                   ");

            Console.WriteLine("Nome: " + nome);
            Console.WriteLine("CPF: " + cpf);
            Console.WriteLine("Senha: " +senha);

            //Criar Conta
            ContaCorrente contaCorrente = new ContaCorrente();
            Pessoa pessoa = new Pessoa();

            pessoa.SetNome(nome);
            pessoa.SetCPF(cpf);
            pessoa.SetSenha(senha);
            pessoa.Conta = contaCorrente;

            pessoas.Add(pessoa);

            Console.Clear();
            Console.WriteLine("                    Conta criada com sucesso!                         ");
            Console.WriteLine("                    ===============================                   ");

            Thread.Sleep(1000);

            TelaLogada(pessoa);

        }

        private static void TelaLogin()
        {
            Console.Clear();

            Console.WriteLine("                                                                 ");
            Console.Write("                    Digite o CPF: ");
            string cpf = Console.ReadLine();
            Console.WriteLine("                    ============================                   ");
            Console.Write("                    Digite sua senha: ");
            string senha = Console.ReadLine();
            Console.WriteLine("                    ============================                   ");

            //Logar no sistema
            Pessoa pessoa = pessoas.FirstOrDefault(x => x.CPF == cpf && x.Senha == senha);
            if (pessoa != null)
            {
                TelaBoasVindas(pessoa);
                TelaLogada(pessoa);
            }
            else
            {
                Console.Clear();

                Console.WriteLine("                    Pessoa não cadastrada!                            ");
                Console.WriteLine("                    ===============================                   ");
                Console.WriteLine();
                Console.WriteLine();
            }
            
        }

        private static void TelaBoasVindas(Pessoa pessoa)
        {
            string msgTelaBemVindo =
                $"{pessoa.Nome} | Banco: {pessoa.Conta.GetCodigoDoBanco()} | Agencia: {pessoa.Conta.GetNumeroAgencia()} | Conta: {pessoa.Conta.GetNumeroDaConta()}";

            Console.WriteLine();
            Console.WriteLine($"                    Seja bem vindo, {msgTelaBemVindo} ");
            Console.WriteLine();
        }

        private static void TelaLogada(Pessoa pessoa)
        {
            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.WriteLine("                    Digite a Opção Desejada:                       ");
            Console.WriteLine("                    ============================                   ");
            Console.WriteLine("                    1 - Realizar Depósito                          ");
            Console.WriteLine("                    ============================                   ");
            Console.WriteLine("                    2 - Realizar um Saque                          ");
            Console.WriteLine("                    ============================                   ");
            Console.WriteLine("                    3 - Consultar Saldo                            ");
            Console.WriteLine("                    ============================                   ");
            Console.WriteLine("                    4 - Extrato                                    ");
            Console.WriteLine("                    ============================                   ");
            Console.WriteLine("                    5 - Sair                                       ");
            Console.WriteLine("                    ============================                   ");

            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1: TelaDeposito(pessoa);
                    break;
                case 2: TelaSaque(pessoa);
                    break;
                case 3: TelaSaldo(pessoa);
                    break;
                case 4: TelaExtrato(pessoa);
                    break;
                case 5: TelaPrincipal();
                    break;
                default: 
                    Console.Clear();
                    Console.WriteLine("                    Opção Inválida!                                ");
                    Console.WriteLine("                    =============================                 ");
                    break;
            } 
        }

        private static void TelaDeposito(Pessoa pessoa)
        {
            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.Write("                    Digite o valor do deposito: R$ ");
            double valor = double.Parse(Console.ReadLine());
            Console.WriteLine("                    ============================                   ");

            pessoa.Conta.Deposita(valor);

            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.WriteLine("                                                                   ");
            Console.WriteLine("                                                                   ");
            Console.WriteLine("                    Depósito Realizado com Sucesso!                ");
            Console.WriteLine("                    ================================               ");
            Console.WriteLine("                                                                   ");
            Console.WriteLine("                                                                   ");

            OpcaoVoltarLogado(pessoa);

        }
        private static void TelaSaque(Pessoa pessoa)
        {
            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.Write("                    Digite o valor do Saque: R$ ");
            double valor = double.Parse(Console.ReadLine());
            Console.WriteLine("                    =============================                 ");

            bool okSaque = pessoa.Conta.Sacar(valor);

            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.WriteLine("                                                                   ");
            Console.WriteLine("                                                                   ");
            
            if (okSaque)
            {
                Console.WriteLine("                    Saque Realizado com Sucesso!                   ");
                Console.WriteLine("                    =============================                 ");
            }
            else
            {
                Console.WriteLine("                    Saldo Insulficiente!                           ");
                Console.WriteLine("                    =============================                 ");
            }
            Console.WriteLine("                                                                   ");
            Console.WriteLine("                                                                   ");

            OpcaoVoltarLogado(pessoa);

        }
        private static void TelaSaldo(Pessoa pessoa)
        {
            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.WriteLine($"                    Seu Saldo é: {pessoa.Conta.ConsultaSaldo().ToString("C")} ");
            Console.WriteLine("                    =============================                 ");
            Console.WriteLine("                                                                   ");
            Console.WriteLine("                                                                   ");

            OpcaoVoltarLogado(pessoa);
        }

        private static void TelaExtrato(Pessoa pessoa)
        {
            Console.Clear();

            TelaBoasVindas(pessoa);

            if(pessoa.Conta.Extrato().Any())
            {
                double total = pessoa.Conta.Extrato().Sum(x => x.Valor);

                foreach(Extrato extrato in pessoa.Conta.Extrato())
                {
                    Console.WriteLine("                                                                  ");
                    Console.WriteLine($"                   Data: {extrato.Data.ToString("dd/MM/yyyy HH:mm:ss")}                          ");
                    Console.WriteLine($"                   Tipo de Movimentação: {extrato.Descricao}     ");
                    Console.WriteLine($"                   Valor: {extrato.Valor.ToString("C")}          ");
                    Console.WriteLine("                    =============================                 ");
                }

                Console.WriteLine("                                                                  ");
                Console.WriteLine("                                                                  ");
                Console.WriteLine($"                   SUB TOTAL: {total.ToString("C")}              ");
                Console.WriteLine("                    =============================                 ");
            }
            else
            {
                Console.WriteLine("                    Não há extrato a ser exibido!                 ");
                Console.WriteLine("                    =============================                 ");
            }

            OpcaoVoltarLogado(pessoa);
        }

        private static void OpcaoVoltarLogado(Pessoa pessoa)
        {
            Console.WriteLine("                    Entre com uma opção abaixo:                   ");
            Console.WriteLine("                    =============================                 ");
            Console.WriteLine("                    1 - Voltar para minha conta.                  ");
            Console.WriteLine("                    =============================                 ");
            Console.WriteLine("                    2 - Sair.                                     ");
            Console.WriteLine("                    =============================                 ");

            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:TelaLogada(pessoa);
                    break;
                case 2:TelaPrincipal();
                    break;
                default: Console.WriteLine("Opção Invalida!");
                    Thread.Sleep(1000);
                    Console.Clear(); 
                    OpcaoVoltarLogado(pessoa);
                    break;
            }
        }

        private static void OpcaoVoltarDeslogado()
        {
            Console.WriteLine("                    Entre com uma opção abaixo:                   ");
            Console.WriteLine("                    =================================             ");
            Console.WriteLine("                    1 - Voltar para o Menu Principal.             ");
            Console.WriteLine("                    =================================             ");
            Console.WriteLine("                    2 - Sair.                                     ");
            Console.WriteLine("                    =================================             ");

            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    TelaPrincipal();
                    break;
                case 2:
                    
                    break;
                default:
                    Console.WriteLine("Opção Invalida!");
                    Thread.Sleep(1000);
                    Console.Clear();
                    OpcaoVoltarDeslogado();
                    break;
            }
        }
    }


}
