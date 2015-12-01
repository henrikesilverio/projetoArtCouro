﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Newtonsoft.Json.Linq;
using ProjetoArtCouro.Api.Helpers;
using ProjetoArtCouro.Domain.Contracts.IService.IPagamento;
using ProjetoArtCouro.Domain.Models.Pagamentos;
using ProjetoArtCouro.Model.Models.CondicaoPagamento;

namespace ProjetoArtCouro.Api.Controllers.Pagamentos
{
    [RoutePrefix("api/CondicaoPagamento")]
    public class CondicaoPagamentoController : BaseApiController
    {
        private readonly ICondicaoPagamentoService _condicaoPagamentoService;
        public CondicaoPagamentoController(ICondicaoPagamentoService condicaoPagamentoService)
        {
            _condicaoPagamentoService = condicaoPagamentoService;
        }

        [Route("ObterListaCondicaoPagamento")]
        [Authorize(Roles = "PesquisaCondicaoPagamento")]
        [HttpGet]
        public Task<HttpResponseMessage> ObterListaCondicaoPagamento()
        {
            HttpResponseMessage response;
            try
            {
                var listaCondicaoPagamento = _condicaoPagamentoService.ObterListaCondicaoPagamento();
                response = ReturnSuccess(Mapper.Map<List<CondicaoPagamentoModel>>(listaCondicaoPagamento));
            }
            catch (Exception ex)
            {
                response = ReturnError(ex);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [Route("CriarCondicaoPagamento")]
        [Authorize(Roles = "NovaCondicaoPagamento")]
        [HttpPost]
        public Task<HttpResponseMessage> CriarCondicaoPagamento(CondicaoPagamentoModel model)
        {
            HttpResponseMessage response;
            try
            {
                var condicaoPagamento = Mapper.Map<CondicaoPagamento>(model);
                var condicaoPagamentoRetorno = _condicaoPagamentoService.CriarCondicaoPagamento(condicaoPagamento);
                response = ReturnSuccess(Mapper.Map<CondicaoPagamentoModel>(condicaoPagamentoRetorno));
            }
            catch (Exception ex)
            {
                response = ReturnError(ex);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [Route("EditarCondicaoPagamento")]
        [Authorize(Roles = "EditarCondicaoPagamento")]
        [HttpPut]
        public Task<HttpResponseMessage> EditarCondicaoPagamento(CondicaoPagamentoModel model)
        {
            HttpResponseMessage response;
            try
            {
                var condicaoPagamento = Mapper.Map<CondicaoPagamento>(model);
                var condicaoPagamentoRetorno = _condicaoPagamentoService.AtualizarCondicaoPagamento(condicaoPagamento);
                response = ReturnSuccess(Mapper.Map<CondicaoPagamentoModel>(condicaoPagamentoRetorno));
            }
            catch (Exception ex)
            {
                response = ReturnError(ex);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [Route("ExcluirCondicaoPagamento")]
        [Authorize(Roles = "ExcluirCondicaoPagamento")]
        [HttpDelete]
        public Task<HttpResponseMessage> ExcluirCondicaoPagamento([FromBody]JObject jObject)
        {
            var codigoCondicaoPagamento = jObject["codigoCondicaoPagamento"].ToObject<int>();
            HttpResponseMessage response;
            try
            {
                _condicaoPagamentoService.ExcluirCondicaoPagamento(codigoCondicaoPagamento);
                response = ReturnSuccess();
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
            _condicaoPagamentoService.Dispose();
        }
    }
}