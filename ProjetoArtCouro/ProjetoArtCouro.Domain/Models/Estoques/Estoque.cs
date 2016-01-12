﻿using System;
using ProjetoArtCouro.Domain.Models.Pessoas;
using ProjetoArtCouro.Domain.Models.Produtos;

namespace ProjetoArtCouro.Domain.Models.Estoques
{
    public class Estoque
    {
        public Guid EstoqueId { get; set; }
        public int EstoqueCodigo { get; set; }
        public DateTime DataUltimaEntrada { get; set; }
        public int Quantidade { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual Pessoa Fornecedor { get; set; }
    }
}
