﻿using System.ComponentModel.DataAnnotations;
using ProjetoArtCouro.Resources.Resources;

namespace ProjetoArtCouro.Model.Models.Fornecedor
{
    public class PesquisaFornecedorModel
    {
        [Display(Name = "Code", ResourceType = typeof(Mensagens))]
        public int? CodigoFornecedor { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Mensagens))]
        public string Nome { get; set; }

        [Display(Name = "CPFCNPJ", ResourceType = typeof(Mensagens))]
        public string CPFCNPJ { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Mensagens))]
        public string Email { get; set; }

        [Display(Name = "TypeOfPerson", ResourceType = typeof(Mensagens))]
        public bool EPessoaFisica { get; set; }
    }
}
