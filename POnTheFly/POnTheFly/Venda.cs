using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POnTheFly;

namespace Projeto_OnTheFly
{
    internal class Venda
    {
        //Propriedades
        public int Id { get; set; }
        public DateTime DataVenda { get; set; }
        public string Passageiro { get; set; }
        public double ValorTotal { get; set; }
        //Metodos
        public Venda() { }
        public Venda(int id, DateTime dataVenda, string passageiro, double valorTotal)
        {
            Id = id;
            DataVenda = dataVenda;
            Passageiro = passageiro;
            ValorTotal = valorTotal;
        }
        public Venda CadastrarVenda(List<Venda> listaDeVendas, List<ItemVenda> listaDeItemVenda)
        {

            double valorUnitarioPassagem = 1000;
            int tamanhoListaDeVendas = listaDeVendas.Count;
            int qtdPassagens = 0;
            string cpf;

            Console.Clear();

            cpf = Program.ReadCPF("Digite o CPF do portador sem pontos ou traço: ");

            if (false) //inserir a funcao de buscar cpf na lista de restritos aqui
            {
                Console.WriteLine("CPF Restrito, impossivel realizar a venda");
                return null;
            }

            if (false) //inserir a funcao de buscar cpf na lista de restritos aqui
            {
                Console.WriteLine("O passageiro tem menos de 18 anos, impossivel realizar a venda");
                return null;
            }

            if (false) // verificar aqui se a quantidade de passagens livres é maior que qtdPassagens
            {
                //msg = "Nao foi possivel vender as passagens! Verifique a quantidade de passagens disponiveis";
            }
            //Trocar o status da passagem para Paga

            do
            {
                qtdPassagens = Program.ReadInt("Digite a quantidade de passagens que deseja comprar" +
                    " (maximo 4 por venda): ");
                if (qtdPassagens < 1 || qtdPassagens > 4) Console.WriteLine("Quantidade invalida!");
            } while (qtdPassagens < 1 || qtdPassagens > 4);

            Venda venda = new Venda
            (
                tamanhoListaDeVendas + 1,
                DateTime.Today,
                cpf,
                qtdPassagens * valorUnitarioPassagem
            );

            for (int i = 0; i < qtdPassagens; i++)
            {
                ItemVenda itemVenda = new ItemVenda(venda.Id, 5555, valorUnitarioPassagem);
                listaDeItemVenda.Add(itemVenda);
            }
            //msg = "Venda Cadastrada com sucesso!";
            return venda;

        }
        public static void ImprimirVendas(List<Venda> listaDeVendas)
        {
            string op = "-1";
            string msg = "";
            string[] options = new string[] { "1", "2", "3", "4", "0" };
            int i = 0;

            while (op != "0")
            {
                Console.Clear();
                Console.WriteLine("*********REGISTROS DE VENDA*********");
                Console.WriteLine(listaDeVendas[i]);
                Console.WriteLine("\n>>> Qtde total de registros: {0}    /    Registro atual: {1}", listaDeVendas.Count, i + 1);
                Console.WriteLine("\nInforme a operacao desejada: ");
                Console.WriteLine("1. Ir para o inicio da lista");
                Console.WriteLine("2. Voltar para o registro anterior");
                Console.WriteLine("3. Avancar para o proximo registro");
                Console.WriteLine("4. Ir para o ultimo registro");
                Console.WriteLine("0. Voltar");
                Console.Write("\n{0}{1}{2}", msg == "" ? "" : ">>> ", msg, msg == "" ? "" : "\n");
                op = Program.ReadString("Opcao: ");
                if (!Program.BuscarNoArray(op, options))
                {
                    msg = "Opcao invalida! Digite novamente...";
                    continue;
                }
                switch (op)
                {
                    case "1":
                        i = 0;
                        break;
                    case "2":
                        if (i != 0) i--;
                        break;
                    case "3":
                        if (i < listaDeVendas.Count - 1) i++;
                        break;
                    case "4":
                        i = listaDeVendas.Count - 1;
                        break;
                    case "0":
                        return;
                }
            }
        }
        public static void LocalizarVenda(List<Venda> listaDeVendas)
        {
            string op = "-1";
            string msg = "";
            int inputId;
            string[] options = new string[] { "1", "0" };
            bool b;

            while (op != "0")
            {
                Console.Clear();
                Console.WriteLine("*********LOCALIZAR VENDA*********");
                Console.WriteLine("\nInforme a operacao desejada: ");
                Console.WriteLine("1. Localizar nova venda");
                Console.WriteLine("0. Voltar");
                Console.Write("\n{0}{1}{2}", msg == "" ? "" : ">>> ", msg, msg == "" ? "" : "\n");
                op = Program.ReadString("Opcao: ");
                if (!Program.BuscarNoArray(op, options))
                {
                    msg = "Opcao invalida! Digite novamente...";
                    continue;
                }
                switch (op)
                {
                    case "1":
                        b = false;
                        inputId = Program.ReadInt("Digite o ID da Venda (apenas numeros): V");

                        foreach (Venda venda in listaDeVendas)
                        {
                            if (venda.Id == inputId)
                            {
                                Console.WriteLine("");
                                Console.WriteLine(venda);
                                Program.ReadString("\nDigite qualquer tecla para continuar...");
                                b = true;
                            };
                        }
                        if (!b) Program.ReadString("\nVenda nao encontrada!\nDigite qualquer tecla para continuar...");
                        break;
                    case "0":
                        return;
                }
            }
        }
        public void AcessarVenda(List<Venda> listaDeVendas, List<ItemVenda> listaDeItemVenda)
        {
            int opcao = 0;
            bool condicaoDeParada = false;
            Venda venda = new();

            do
            {
                Console.Clear();

                Console.WriteLine("OPÇÃO: ACESSAR VENDAS\n");

                Console.WriteLine("1 - Cadastrar Venda");
                Console.WriteLine("2 - Localizar Venda");
                Console.WriteLine("3 - Imprimir Venda");
                Console.WriteLine("\n9 - Voltar ao menu anterior");
                Console.Write("\nOpção: ");

                try
                {
                    opcao = int.Parse(Console.ReadLine());
                    condicaoDeParada = false;
                }

                catch (Exception)
                {
                    Console.WriteLine("Parametro de entrada inválido!");
                    Console.WriteLine("Pressione enter para escolher novamente!");
                    Console.ReadKey();
                    condicaoDeParada = true;
                }

                if (opcao < 1 || opcao > 3 && opcao != 9)
                {
                    if (!condicaoDeParada)
                    {
                        Console.WriteLine("Escolha uma das opções disponiveis!!");
                        Console.WriteLine("Pressione enter para escolher novamente!");
                        Console.ReadKey();
                        condicaoDeParada = true;
                    }
                }

                switch (opcao)
                {
                    case 1:
                        listaDeVendas.Add(venda.CadastrarVenda(listaDeVendas, listaDeItemVenda));
                        Console.ReadKey();
                        break;

                    case 2:
                        if (listaDeVendas.Count != 0) Venda.LocalizarVenda(listaDeVendas);
                        Console.ReadKey();
                        break;

                    case 3:
                        if (listaDeVendas.Count != 0) Venda.ImprimirVendas(listaDeVendas);
                        Console.ReadKey();
                        break;

                    case 9:
                        Console.WriteLine("Até");
                        break;
                }

            } while (opcao != 9);
        }
        public override string ToString()
        {
            string cpfFormat = String.Format("{0}.{1}.{2}-{3}",
                Passageiro.Substring(0, 3), Passageiro.Substring(3, 3), Passageiro.Substring(6, 3), Passageiro.Substring(9, 2));
            return String.Format("Id:\t\t\tV{0:0000}\n" +
                "Data da Venda:\t\t{1}\n" +
                "CPF do Passageiro:\t{2}\n" +
                "Valor Total da Venda:\tR${3:0.00}",
                Id, DataVenda.ToShortDateString(), cpfFormat, ValorTotal);
        }
    }
}