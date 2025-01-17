﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using ProjetoArtCouro.Api.Helpers;
using ProjetoArtCouro.Domain.Contracts.IService.IPessoa;
using ProjetoArtCouro.Model.Models.Common;
using WebApi.OutputCache.V2;

namespace ProjetoArtCouro.Api.Controllers.Pessoas
{
    [RoutePrefix("api/Pessoa")]
    public class PessoaController : BaseApiController
    {
        private readonly IPessoaService _pessoaService;
        public PessoaController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [Route("ObterListaEstado")]
        [Authorize(Roles = "NovoCliente, EditarCliente, NovoFornecedor, EditarFornecedor, NovoFuncionario, EditarFuncionario")]
        [CacheOutput(ServerTimeSpan = 10000)]
        [HttpGet]
        public Task<HttpResponseMessage> ObterListaEstado()
        {
            HttpResponseMessage response;
            try
            {
                var listaEstado = _pessoaService.ObterEstados();
                response = ReturnSuccess(Mapper.Map<List<LookupModel>>(listaEstado));
            }
            catch (Exception ex)
            {
                response = ReturnError(ex);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [Route("ObterListaEstadoCivil")]
        [Authorize(Roles = "NovoCliente, EditarCliente, NovoFornecedor, EditarFornecedor, NovoFuncionario, EditarFuncionario")]
        [CacheOutput(ServerTimeSpan = 10000)]
        [HttpGet]
        public Task<HttpResponseMessage> ObterListaEstadoCivil()
        {
            HttpResponseMessage response;
            try
            {
                var listaEstadoCivil = _pessoaService.ObterEstadosCivis();
                response = ReturnSuccess(Mapper.Map<List<LookupModel>>(listaEstadoCivil));
            }
            catch (Exception ex)
            {
                response = ReturnError(ex);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [Route("ObterListaPessoa")]
        [Authorize(Roles = "NovaVenda")]
        [CacheOutput(ServerTimeSpan = 10000)]
        [HttpGet]
        public Task<HttpResponseMessage> ObterListaPessoa()
        {
            HttpResponseMessage response;
            try
            {
                var listaPessoa = _pessoaService.ObterListaPessoa();
                response = ReturnSuccess(Mapper.Map<List<PessoaModel>>(listaPessoa));
            }
            catch (Exception ex)
            {
                response = ReturnError(ex);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        protected override void Dispose(bool disposing)
        {
            _pessoaService.Dispose();
        }
    }
}
