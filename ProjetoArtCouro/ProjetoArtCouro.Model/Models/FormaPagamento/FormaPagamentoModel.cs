﻿using System.ComponentModel.DataAnnotations;
using ProjetoArtCouro.Resource.Resources;

namespace ProjetoArtCouro.Model.Models.FormaPagamento
{
    public class FormaPagamentoModel
    {
        [Display(Name = "Description", ResourceType = typeof(Mensagens))]
        public string Descricao { get; set; }

        [Display(Name = "Active", ResourceType = typeof(Mensagens))]
        public bool Ativo { get; set; }
    }
}
